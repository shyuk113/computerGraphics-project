using UnityEngine;

public class TutorialPanelTrigger : MonoBehaviour
{
    public GameObject panel; // Inspector���� �г� ������Ʈ ����

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true); // �г� ���̱�
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false); // Ʈ���ſ��� ����� �г� �����
        }
    }
}
