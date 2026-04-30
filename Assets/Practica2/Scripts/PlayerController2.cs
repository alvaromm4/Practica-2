using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float crouchSpeed = 2.5f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Cámara y Zoom")]
    [SerializeField] private Camera cam;
    [SerializeField] private float mouseSensitivity = 200f;
    [SerializeField] private float zoomFOV = 40f; 
    private float normalFOV;

    [Header("Sistema de Apuntado (Aim)")]
    [SerializeField] private Transform weaponHolder; 
    [SerializeField] private Vector3 hipPosition; 
    [SerializeField] private Vector3 aimPosition; 
    [SerializeField] private float aimSpeed = 10f;

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

        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        ManejarCamara();
        ManejarMovimiento();
        ManejarAgachado();
        ManejarZoomYAim();
    }

    void ManejarCamara()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotación horizontal (Cuerpo)
        transform.Rotate(Vector3.up * mouseX);

        // Rotación vertical (Cámara) con límite
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void ManejarMovimiento()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;

        float currentSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed *= sprintMultiplier;
        if (Input.GetKey(KeyCode.C)) currentSpeed = crouchSpeed;

        rb.linearVelocity = new Vector3(moveDir.x * currentSpeed, rb.linearVelocity.y, moveDir.z * currentSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && suelo)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            suelo = false;
        }
    }

    void ManejarAgachado()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            boxCol.size = crouchSize;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            boxCol.size = normalSize;
        }
    }

    void ManejarZoomYAim()
    {
        bool isAiming = Input.GetMouseButton(1);

        float targetFOV = isAiming ? zoomFOV : normalFOV;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * aimSpeed);

        if (weaponHolder != null)
        {
            Vector3 targetPos = isAiming ? aimPosition : hipPosition;
            weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, targetPos, Time.deltaTime * aimSpeed);
        }
    }

    private void OnCollisionStay(Collision c) { if (c.gameObject.CompareTag("Floor")) suelo = true; }
    private void OnCollisionExit(Collision c) { if (c.gameObject.CompareTag("Floor")) suelo = false; }
}