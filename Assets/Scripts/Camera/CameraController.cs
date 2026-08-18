using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{ 

    public float moveSpeed = 10;
    public float zoomSpeed = 20;
    public float rotationSpeed = 10;
    private float yaw = 0;
    private float pitch = 45;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardFlat = new Vector3(transform.forward.x, 0, transform.forward.z);
        Vector3 rightFlat = new Vector3(transform.right.x, 0, transform.right.z);

        if (Keyboard.current.wKey.isPressed)
        {
            transform.position += forwardFlat * moveSpeed * Time.deltaTime;  
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.position += -forwardFlat * moveSpeed * Time.deltaTime;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            transform.position += -rightFlat * moveSpeed * Time.deltaTime;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            transform.position += rightFlat * moveSpeed * Time.deltaTime;
        }

        float scrollValue = Mouse.current.scroll.ReadValue().y;
        transform.position += transform.forward * scrollValue * zoomSpeed * Time.deltaTime;

        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 readValue = Mouse.current.delta.ReadValue();
            yaw += readValue.x * rotationSpeed;
            pitch -= readValue.y * rotationSpeed;
            pitch = Mathf.Clamp(pitch, 10f, 80f);
            transform.eulerAngles = new Vector3(pitch, yaw, 0); 
        }

    }
}
