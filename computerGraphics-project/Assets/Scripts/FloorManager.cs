using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class FloorManager : MonoBehaviour
{
    public static FloorManager Instance { get; private set; }

    public List<string> randomFloors = new List<string> { "floorA", "floorB", "floorC" };
    public string escapeScene = "final";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
            SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        }
        else
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }



    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    // 씬이 로드될 때마다 호출됨
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 예시: 엔딩/탈출씬에서는 커서 보이기, 그 외에는 숨기기
        if (scene.name == escapeScene)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void LoadNextFloor()
    {
        var data = GameManager.Instance;
        if (data == null)
        {
            Debug.LogWarning("[FloorManager] GameManager를 찾을 수 없습니다!");
            return;
        }

        if (data.currentFloor < data.totalFloors)
        {
            data.NextFloor();
            int idx = Random.Range(0, randomFloors.Count);
            string nextFloor = randomFloors[idx];
            Debug.Log($"[FloorManager] {data.currentFloor}층으로 이동 ({nextFloor})");
            SceneManager.LoadScene(nextFloor);
        }
        else
        {
            Debug.Log("[FloorManager] 탈출씬으로 이동");
            SceneManager.LoadScene(escapeScene);
        }
    }
    public void ResetAll()
    {

        Debug.Log("[FloorManager] 상태가 초기화되었습니다!");
    }

    public int GetCurrentFloor()
    {
        return GameManager.Instance != null ? GameManager.Instance.currentFloor : 1;
    }
}

