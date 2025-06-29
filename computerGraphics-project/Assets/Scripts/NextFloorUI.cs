using UnityEngine;

public class NextFloorUI : MonoBehaviour
{
    public GameObject NextFloorPanel; // Inspector에서 연결
    public MonoBehaviour[] controlScripts; // 플레이어 조작 스크립트 연결

    // OK 버튼에서 호출
    public void OnOK()
    {
        Resume();
        // 다음 층으로 이동
        var FloorManager = FindObjectOfType<FloorManager>();
        if (FloorManager != null)
        {
            FloorManager.LoadNextFloor();
        }
        else
        {
            Debug.LogWarning("[NextFloorUI] FloorManager를 찾을 수 없습니다!");
        }
    }

    // NO 버튼에서 호출
    public void OnNo()
    {
        Resume();
        // 추가 행동이 필요하다면 여기에 작성
    }

    // 일시정지 해제(공통)
    private void Resume()
    {
        if (NextFloorPanel != null)
            NextFloorPanel.SetActive(false);

        // 플레이어 컨트롤러 복구
        if (controlScripts != null)
        {
            foreach (var script in controlScripts)
                if (script != null) script.enabled = true;
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
