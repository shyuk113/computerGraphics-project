using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // 메인메뉴에서는 항상 마우스가 보이게!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f; // 혹시 이전 씬에서 일시정지된 경우 복구
    }

    // Start 버튼 클릭 시 호출되는 함수
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("floorB");
    }
    void Awake()
    {
        GameObject menuCameraObject = GameObject.FindWithTag("MainMenuCamera");
        AudioListener listener = null;
        if (menuCameraObject != null)
        {
            listener = menuCameraObject.GetComponent<AudioListener>();
        }

        if (listener != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            listener.enabled = player == null;
        }
    }
}
