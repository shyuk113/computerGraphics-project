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

    // PeopleSpawner���� ȣ��!
    public void SetKeypadCombo(int answer)
    {
        keypad.SetKeypadCombo(answer);
        Debug.Log("������ " + answer);
    }

    public void CorrectAnswer()
    {
        Debug.Log("����!");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }
}
