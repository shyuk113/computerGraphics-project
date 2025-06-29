using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    private bool isOpen = false;
    private Quaternion closedRotation;
    private Quaternion openRotation;

    public AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;

    public Transform player; // Inspector에서 플레이어 Transform 연결

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform; // 플레이어에 "Player" 태그 부여 필요
    }

    void Update()
    {
        if (isOpen)
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, closedRotation, Time.deltaTime * openSpeed);
    }

    public void ToggleDoor()
    {
        if (!isOpen)
        {
            // 플레이어가 문의 어느 쪽에 있는지 계산
            Vector3 doorToPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.forward, doorToPlayer);

            // 문의 앞쪽(dot > 0) → openAngle, 뒤쪽(dot < 0) → -openAngle
            float angle = (dot < 0) ? openAngle : -openAngle;

            // openRotation을 즉시 갱신
            closedRotation = transform.rotation;
            openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, angle, 0));
        }

        isOpen = !isOpen;

        if (audioSource != null)
        {
            if (isOpen && openSound != null)
                audioSource.PlayOneShot(openSound);
            else if (!isOpen && closeSound != null)
                audioSource.PlayOneShot(closeSound);
        }
    }
}
