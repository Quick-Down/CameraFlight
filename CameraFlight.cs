using UnityEngine;

public class CameraFlight: MonoBehaviour
{
    public float Sensitivity = 3f;  // Mouse sensitivity 
    public float speed = 5f;  
    public float minX = -360f;      // The rotation angle limitation, can be changed if necessary. 
    public float maxX = 360f;
    public float minY = -60f;
    public float maxY = 60f;
    float rotationX = 0f;
    float rotationY = 0f;
    Quaternion originalRotation;
    Vector3 startCam;
    Vector3 transfer;

    void Start()
    { originalRotation = transform.rotation; }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))                       // Deceleration by pressing the left Shift
            speed = 20f;

        rotationX += Input.GetAxis("Mouse X") * Sensitivity;       // Mouse Movements -> Camera Rotation
        rotationY += Input.GetAxis("Mouse Y") * Sensitivity;
        rotationX = ClampAngle(rotationX, minX, maxX);
        rotationY = ClampAngle(rotationY, minY, maxY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, Vector3.left);
        transform.rotation = originalRotation * xQuaternion * yQuaternion;

        Vector3 newPos = new Vector3(0, 1, 0);
        if (Input.GetKey(KeyCode.E))                               // Raising and lowering the camera
            transform.position += newPos * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.Q))
            transform.position -= newPos * speed * Time.deltaTime;

        transfer = transform.forward * Input.GetAxis("Vertical");  // Moving the camera 
        transfer += transform.right * Input.GetAxis("Horizontal");
        transform.position += transfer * speed * Time.deltaTime;

        if (Input.GetKeyDown("r"))                                 // Disabling camera movement
        {
            if (speed > 0f) {
                Sensitivity = 0f;
                speed = 0f;
            }
            else if (speed == 0f) {
                Sensitivity = 3f;
                speed = 5f;
            }
        }
        if (transform.position.y > 2.129774 || transform.position.y < 0.03639568) // Limiting coordinates for the camera 
        {
            startCam.x = -0.0554f;                                                // You can specify any coordinate constraints you want. 
            startCam.y = 1.44f; 
            startCam.z = 0.6766f;
            //transform.position = startCam;                                      // Remove this bracket to limit camera flight. 
        }
    }

    public static float ClampAngle(float angle, float min, float max)             // Rotation restriction 
    {
        if (angle < -360F) angle += 360F;
        if (angle > 360F) angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

}