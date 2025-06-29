using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        // ���θ޴������� �׻� ���콺�� ���̰�!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f; // Ȥ�� ���� ������ �Ͻ������� ��� ����
    }

    // Start ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
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
