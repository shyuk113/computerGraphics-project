using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip walkClip;
    public AudioClip runClip;
    public float walkStepInterval = 0.5f; // 걷기 발소리 간격
    public float runStepInterval = 0.3f;  // 뛰기 발소리 간격

    private float stepTimer = 0f;
    private CharacterController controller; // 또는 Rigidbody 등

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool isMoving = controller.velocity.magnitude > 0.1f && controller.isGrounded;
        bool isRunning = Input.GetKey(KeyCode.LeftShift); // 달리기 키

        if (isMoving)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                audioSource.clip = isRunning ? runClip : walkClip;
                audioSource.PlayOneShot(audioSource.clip);

                stepTimer = isRunning ? runStepInterval : walkStepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
}
