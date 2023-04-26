using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 360f)]
    private float rotHSpeed = 100f;
    [SerializeField, Range(0f, 360f)]
    private float rotVSpeed = 100f;

    [SerializeField]
    private Camera cam = null;

    private float rotYaw = 0f;
    private float rotPitch = 0f;
    private float posY = 0f;
    private float posRad = 0f;


    private void Awake()
    {
        posY = cam.transform.localPosition.y;
        posRad = new Vector2(cam.transform.localPosition.x, cam.transform.localPosition.z).magnitude;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float yaw = Input.GetAxisRaw("Mouse X");
        float pitch = Input.GetAxisRaw("Mouse Y");
        yaw *= rotHSpeed; pitch *= -rotVSpeed;
        rotYaw += yaw * Time.deltaTime; rotPitch += pitch * Time.deltaTime;
        rotYaw %= 360f;
        rotPitch = Mathf.Clamp(rotPitch, -80f, 80f);

        Vector3 pos = Vector3.zero;
        float tan = Mathf.Tan(rotPitch * Mathf.Deg2Rad);
        float rad = posRad;
        if (tan > 1f) rad /= tan;
        else if (tan < -0.3f) rad /= -tan + 0.7f;
        pos.y = Mathf.Clamp(tan, -0.3f, 1f) * posRad + posY;
        
        pos.x = -Mathf.Sin(rotYaw * Mathf.Deg2Rad) * rad;
        pos.z = -Mathf.Cos(rotYaw * Mathf.Deg2Rad) * rad;

        cam.transform.localPosition = pos;
        cam.transform.rotation = Quaternion.Euler(rotPitch, rotYaw, 0f);
    }
}
