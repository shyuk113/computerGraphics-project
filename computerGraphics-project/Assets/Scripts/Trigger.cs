
using UnityEngine;

public class NextFloorTrigger : MonoBehaviour
{
    public GameObject NextFloorPanel; // Inspector에서 연결
    public MonoBehaviour playerController; // 플레이어 이동 스크립트 연결

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
                Debug.LogWarning("[Trigger] NextFloorPanel이 null(혹은 파괴됨) 상태입니다. SetActive를 시도하지 않습니다.");
            }

            // 플레이어 조작 잠금 (예: FPSController, PlayerMovement 등)
            if (playerController != null)
                playerController.enabled = false;

            // 마우스 커서 보이기
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}