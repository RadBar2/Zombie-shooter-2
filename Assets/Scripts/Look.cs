using UnityEngine;

public class Look : MonoBehaviour
{
    public Transform cam;
    
        private float xRotation;
    
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    
       
        void Update()
        {
            var mouseX = Input.GetAxis("Mouse X");
            var mouseY = Input.GetAxis("Mouse Y");
    
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90);
    
            cam.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(0, mouseX, 0);
        }
}
