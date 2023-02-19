using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sensitivity = 5.0f;

    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;

    void Update()
    {
        // Movement controls
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float updown = 0f;
        if (Input.GetKey(KeyCode.Space)) updown = 1f;
        if (Input.GetKey(KeyCode.LeftShift)) updown = -1f;
        Vector3 movement = new Vector3(horizontal, updown, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Mouse look controls
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;
        _rotationX += mouseY;
        _rotationY += mouseX;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(-_rotationX, _rotationY, 0f);
    }
}
