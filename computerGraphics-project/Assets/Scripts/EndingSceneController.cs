using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingSceneController : MonoBehaviour
{
    public void GoToMainMenu()
    {
        GameManager.Instance?.ResetPeopleCount();
        SceneManager.LoadScene("Main Menu");
    }
}
