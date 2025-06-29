using UnityEngine;
using NavKeypad;

public class KeypadManageTutorial : MonoBehaviour
{
    private Keypad keypad;

    void Start()
    {
        GameObject keypadObject = GameObject.Find("Keypad");
        keypad = keypadObject.GetComponent<Keypad>();
    }

    // PeopleSpawner에서 호출!
    public void SetKeypadCombo(int answer)
    {
        keypad.SetKeypadCombo(answer);
        Debug.Log("정답은 " + answer);
    }

    public void CorrectAnswer()
    {
        Debug.Log("정답!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
