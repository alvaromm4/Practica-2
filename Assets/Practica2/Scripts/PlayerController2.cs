using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Cámara y Zoom")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform cameraPivot; 
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float zoomFOV = 40f; 
    private float normalFOV;

    private Rigidbody rb;
    private bool suelo;
    private float xRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cam != null) normalFOV = cam.fieldOfView;

        rb.freezeRotation = true; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ManejarCamara();
        ManejarZoom();
    }

    private void FixedUpdate()
    {
        ManejarMovimiento();
    }

    void ManejarCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        
        if(cameraPivot != null)
            cameraPivot.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        else
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void ManejarMovimiento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;

        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed *= sprintMultiplier;

        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);

        if (Input.GetKey(KeyCode.Space) && suelo)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            suelo = false;
        }
    }

    void ManejarZoom()
    {
        float target = Input.GetMouseButton(1) ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, target, Time.deltaTime * 10f);
    }

    private void OnCollisionStay(Collision c) 
    { 
        if (c.gameObject.CompareTag("Floor")) suelo = true; 
    }

    private void OnCollisionExit(Collision c) 
    { 
        if (c.gameObject.CompareTag("Floor")) suelo = false; 
    }
}