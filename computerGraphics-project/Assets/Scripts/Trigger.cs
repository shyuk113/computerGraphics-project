
using UnityEngine;

public class NextFloorTrigger : MonoBehaviour
{
    public GameObject NextFloorPanel; // Inspector���� ����
    public MonoBehaviour playerController; // �÷��̾� �̵� ��ũ��Ʈ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (NextFloorPanel != null)
            {
                NextFloorPanel.SetActive(true);
            }
            else
            {
                Debug.LogWarning("[Trigger] NextFloorPanel�� null(Ȥ�� �ı���) �����Դϴ�. SetActive�� �õ����� �ʽ��ϴ�.");
            }

            // �÷��̾� ���� ��� (��: FPSController, PlayerMovement ��)
            if (playerController != null)
                playerController.enabled = false;

            // ���콺 Ŀ�� ���̱�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}