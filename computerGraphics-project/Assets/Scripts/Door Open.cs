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

    public Transform player; // Inspector���� �÷��̾� Transform ����

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform; // �÷��̾ "Player" �±� �ο� �ʿ�
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
            // �÷��̾ ���� ��� �ʿ� �ִ��� ���
            Vector3 doorToPlayer = player.position - transform.position;
            float dot = Vector3.Dot(transform.forward, doorToPlayer);

            // ���� ����(dot > 0) �� openAngle, ����(dot < 0) �� -openAngle
            float angle = (dot < 0) ? openAngle : -openAngle;

            // openRotation�� ��� ����
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
