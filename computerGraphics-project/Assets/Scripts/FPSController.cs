using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class FPSController : MonoBehaviour
{
    [Header("�̵�/ī�޶� ����")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;

    [Header("�߷� ����")]
    public float gravity = -9.81f;

    [Header("ī�޶� ��鸲 ����")]
    private float bobbingSpeed = 10f;
    private float bobbingAmount = 0.1f;

    [Header("���� ����")]
    public float jumpForce = 1f; // ���� ��


    // ���� ����
    private CharacterController controller;
    private float rotationX = 0f;
    private float verticalVelocity = 0f;
    private float bobbingTimer = 0f;
    private Vector3 cameraInitialPos;
    private bool camLock = false;
    public void setCamLock(bool lockVal) { camLock = lockVal; }

    void Awake()
    {
        // �ߺ� �÷��̾� ���� �� �� ��ȯ �� ����
        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraInitialPos = cameraTransform.localPosition;
    }

    void Update()
    {
        if (camLock) { return; }
        HandleMovement();
        HandleMouseLook();
        HandleCameraBobbing();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; // ���� ���̱�

            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }

        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        move.y = verticalVelocity;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleCameraBobbing()
    {
        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            bobbingTimer += Time.deltaTime * bobbingSpeed;
            float wave = Mathf.Sin(bobbingTimer) * bobbingAmount;
            cameraTransform.localPosition = cameraInitialPos + new Vector3(0f, wave, 0f);
        }
        else
        {
            bobbingTimer = 0f;
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition, cameraInitialPos, Time.deltaTime * 5f);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. SpawnPoint ã��
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            if (controller == null)
                controller = GetComponent<CharacterController>();

            // 2. CharacterController ��Ȱ��ȭ �� ��ġ �̵� �� �ٽ� Ȱ��ȭ
            controller.enabled = false;
            transform.position = spawnPoint.transform.position;
            controller.enabled = true;
        }

        // 3. ī�޶� ȸ�� �� ��鸲 �ʱ�ȭ
        rotationX = 0f;
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.rotation = Quaternion.identity;
        cameraInitialPos = cameraTransform.localPosition;
    }
}
