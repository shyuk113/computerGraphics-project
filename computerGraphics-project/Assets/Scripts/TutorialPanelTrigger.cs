using UnityEngine;

public class TutorialPanelTrigger : MonoBehaviour
{
    public GameObject panel; // Inspector에서 패널 오브젝트 연결

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true); // 패널 보이기
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(false); // 트리거에서 벗어나면 패널 숨기기
        }
    }
}
