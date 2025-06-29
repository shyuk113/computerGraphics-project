using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel;

    void Update()
    {
        // 메인메뉴가 아닐 때만 ESC로 세팅 패널 열기/닫기
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!settingsPanel.activeSelf)
                    OpenSettings();
                else
                    CloseSettings();
            }
        }
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
