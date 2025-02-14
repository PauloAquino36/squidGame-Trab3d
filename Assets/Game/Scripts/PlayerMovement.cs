using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 5f;
    public Transform cameraTransform;
    public float rotationSpeed = 720f;
    public float cameraHeight = 2f;
    public float cameraDistance = 4f; // Distância fixa da câmera
    private float cameraPitch = 0f;
    private float cameraYaw = 0f;
    private Rigidbody rb;
    private float runSpeed;
    private bool death = false;
    private Animator animator;

    void Start()
    {
        runSpeed = walkSpeed * 2f; // Ajuste para corrida
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleCameraRotation();
        if(!death){
            HandleMovement();
        }

        if(animator.GetBool("Death")){
            death = true;
        }
        else{
            death = false;
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        cameraYaw += mouseX * rotationSpeed * Time.deltaTime;
        cameraPitch -= mouseY * rotationSpeed * Time.deltaTime;
        cameraPitch = Mathf.Clamp(cameraPitch, -30f, 60f); // Limitando ângulo da câmera

        Quaternion rotation = Quaternion.Euler(cameraPitch, cameraYaw, 0f);
        Vector3 cameraOffset = new Vector3(0, cameraHeight, -cameraDistance);
        Vector3 targetPosition = transform.position + rotation * cameraOffset;

        cameraTransform.position = targetPosition;
        cameraTransform.LookAt(transform.position + Vector3.up * cameraHeight);

        //////////////////Teste de Morte APAGAR DEPOIS //////////////////////////////////////////////////////
        if(Input.GetKeyDown(KeyCode.K)){
            death = true;
            animator.SetBool("Death", true);
        }
        if(Input.GetKeyDown(KeyCode.L)){
            death = false;
            animator.SetBool("Death", false);
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude > 0.1f)
        {
            Move(direction);
        }
        else{
            animator.SetFloat("Speed", 0f);
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * direction.z + right * direction.x).normalized;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        rb.linearVelocity = moveDirection * speed;

        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        animator.SetFloat("Speed", isRunning ? 100 : 50);
    }
}