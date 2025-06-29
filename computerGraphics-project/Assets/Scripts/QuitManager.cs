using UnityEngine;

public class QuitManager : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting"); // 에디터에서는 종료 안 되므로 로그로 확인
    }
}
