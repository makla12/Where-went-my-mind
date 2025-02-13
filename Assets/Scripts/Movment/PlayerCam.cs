using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX ;
    public float sensY;
    
    [SerializeField] private Transform orientation;

    float xRotation;
    float yRotation;

    private void Start() 
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void Update() 
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX *1.15f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY *1.15f;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
