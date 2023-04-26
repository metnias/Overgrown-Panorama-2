using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Range(0f, 180f)]
    private float rotHSpeed = 100f;
    [SerializeField, Range(0f, 180f)]
    private float rotVSpeed = 100f;

    [SerializeField]
    private Camera cam = null;

    private float rotYaw = 0f;
    private float rotPitch = 0f;

    private void Update()
    {
        if (Input.GetMouseButton(1))
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

        cam.transform.rotation = Quaternion.Euler(rotPitch, rotYaw, 0f);
    }
}
