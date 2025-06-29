using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    void Start()
    {
        // ���θ޴������� �׻� ���콺�� ���̰�!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f; // Ȥ�� ���� ������ �Ͻ������� ��� ����
    }

    // Tutorial ��ư Ŭ�� �� ȣ��Ǵ� �Լ�
    public void OnTutorialButtonClicked()
    {
        SceneManager.LoadScene("Tutorialscene");
    }
}
