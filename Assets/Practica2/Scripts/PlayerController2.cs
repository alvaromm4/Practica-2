using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Cámara y Zoom")]
    [SerializeField] private Camera cam;
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float zoomFOV = 40f; 
    private float normalFOV;

    [Header("Agachado (Box Collider)")]
    [SerializeField] private float crouchHeightY = 1f;
    private Vector3 normalSize;
    private Vector3 crouchSize;

    private Rigidbody rb;
    private BoxCollider boxCol;
    private bool suelo;
    private float xRotation = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCol = GetComponent<BoxCollider>();

        if (cam != null) normalFOV = cam.fieldOfView;
        normalSize = boxCol.size;
        crouchSize = new Vector3(normalSize.x, crouchHeightY, normalSize.z);

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ManejarCamara();
        ManejarMovimiento();
        ManejarZoom();
    }

    void ManejarZoom()
    {
        // Si mantienes click derecho, el objetivo es zoomFOV, si no, el normal
        float target = Input.GetMouseButton(1) ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, target, Time.deltaTime * 10f);
    }

    void ManejarAgachado()
    {
        // Si pulsas C, el BoxCollider se encoge
        if (Input.GetKeyDown(KeyCode.C))
        {
            boxCol.size = crouchSize;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            boxCol.size = normalSize;
        }
    }

    void ManejarCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void ManejarMovimiento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;
        float currentSpeed = Input.GetKey(KeyCode.C) ? crouchSpeed : speed;

        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && suelo)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            suelo = false;
        }
    }

    private void OnCollisionStay(Collision c) { if (c.gameObject.CompareTag("Floor")) suelo = true; }
    private void OnCollisionExit(Collision c) { if (c.gameObject.CompareTag("Floor")) suelo = false; }
}