using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0f, 20f)]
    private float maxSpeed = 5f;


    [SerializeField]
    private float speed = 0f;

    private CharacterSkinController skinCtrler;
    private Animator anim = null;
    private Rigidbody[] rBodys = null;
    private CharacterController charCtrler = null;

    private void Awake()
    {
        skinCtrler = GetComponentInChildren<CharacterSkinController>();
        anim = GetComponentInChildren<Animator>();
        rBodys = GetComponentsInChildren<Rigidbody>();

        charCtrler = GetComponent<CharacterController>();
        speed = 0f;
    }

    private void Update()
    {

        #region Speed

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            speed = Mathf.Lerp(speed, maxSpeed, Time.deltaTime * 4f);
            if (maxSpeed - speed < 0.1f) speed = maxSpeed;
        }
        else
        {
            speed = Mathf.Lerp(speed, 0f, Time.deltaTime * 10f);
            if (speed < 0.1f) speed = 0f;
        }

        skinCtrler.eyeState =
                speed > 1f ? skinCtrler.eyeState = CharacterSkinController.EyePosition.happy
                : skinCtrler.eyeState = CharacterSkinController.EyePosition.normal;

        float spdNormal = Mathf.Clamp(speed / maxSpeed, -1f, 1f);
        anim.SetFloat("MoveX", Input.GetAxis("Horizontal") * spdNormal);
        anim.SetFloat("MoveY", Input.GetAxis("Vertical") * spdNormal);

        #endregion

#if RAGDOLL
        if (Input.GetKey(KeyCode.R))
            ToggleRagdoll(true);

        if (Input.GetKeyDown(KeyCode.T))
            RandomHit();
#endif
    }

    private void FixedUpdate()
    {
        //FirstPersonMoving();
        ThirdPersonMoving();
    }

    /*
    private float velY;

    private void FirstPersonMoving()
    {
        var camForward = Camera.main.transform.forward;
        camForward.y = 0f; camForward.Normalize();

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        var inputVec = new Vector2(h, v);
        inputVec = Vector2.ClampMagnitude(inputVec, 1f);
        inputVec *= speed * 5f * Time.fixedDeltaTime;

        var vec = Vector3.zero;
        vec.x = camForward.x * inputVec.y + camForward.z * inputVec.x;
        vec.z = camForward.z * inputVec.y - camForward.x * inputVec.x;
        velY = charCtrler.isGrounded ? 0f : (velY + Physics.gravity.y * Time.fixedDeltaTime);
        vec.y = velY;
        charCtrler.Move(vec);
    }
    */

    private void ThirdPersonMoving()
    {
        Vector3 dir = new(Input.GetAxisRaw("Horizontal"),
                    charCtrler.isGrounded ? 0f : Physics.gravity.y,
                    Input.GetAxisRaw("Vertical"));
        dir = Vector3.ClampMagnitude(dir, 1f);
        dir *= speed * 5f * Time.fixedDeltaTime;
        charCtrler.Move(dir);
    }

#if RAGDOLL
    private void ToggleRagdoll(bool enable)
    {
        anim.enabled = !enable;
        ToggleKinematics(!enable);

        void ToggleKinematics(bool enable)
        {
            if (rBodys == null) return;
            foreach (var rBody in rBodys)
                rBody.isKinematic = enable;
        }
    }

    private void RandomHit()
    {
        const float POW = 100f;
        Vector3 force = new(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f);
        force = force.normalized * POW;

        var rBody = rBodys[Random.Range(0, rBodys.Length)];
        rBody.AddForce(force, ForceMode.Impulse);
    }
#endif
}
