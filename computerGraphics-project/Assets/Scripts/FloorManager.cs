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
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� ����
            SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
        }
        else
        {
            Destroy(gameObject); // �ߺ� ���� ����
        }
    }



    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }

    // ���� �ε�� ������ ȣ���
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ����: ����/Ż��������� Ŀ�� ���̱�, �� �ܿ��� �����
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
            Debug.LogWarning("[FloorManager] GameManager�� ã�� �� �����ϴ�!");
            return;
        }

        if (data.currentFloor < data.totalFloors)
        {
            data.NextFloor();
            int idx = Random.Range(0, randomFloors.Count);
            string nextFloor = randomFloors[idx];
            Debug.Log($"[FloorManager] {data.currentFloor}������ �̵� ({nextFloor})");
            SceneManager.LoadScene(nextFloor);
        }
        else
        {
            Debug.Log("[FloorManager] Ż������� �̵�");
            SceneManager.LoadScene(escapeScene);
        }
    }
    public void ResetAll()
    {

        Debug.Log("[FloorManager] ���°� �ʱ�ȭ�Ǿ����ϴ�!");
    }

    public int GetCurrentFloor()
    {
        return GameManager.Instance != null ? GameManager.Instance.currentFloor : 1;
    }
}

