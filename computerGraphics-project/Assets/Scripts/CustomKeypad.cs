using UnityEngine;

public class CustomKeypad : MonoBehaviour
{
    public string correctCode = "1234";
    private int totalPeopleNumber;
    private CustomKeypad keypad;
    public UnityEngine.UI.Text inputText;
    private string currentInput = "";

    void Start()
    {
        // CustomKeypad ������Ʈ ã��
        GameObject keypadObject = GameObject.Find("Keypad");
        if (keypadObject == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' ������Ʈ�� ã�� ���߽��ϴ�!");
            return;
        }
        keypad = keypadObject.GetComponent<CustomKeypad>();
        if (keypad == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' ������Ʈ�� CustomKeypad ��ũ��Ʈ�� �����ϴ�!");
            return;
        }

        // GameManager���� ����(�� ��ȯ �ο�) ��������
        totalPeopleNumber = GameManager.Instance.GetTotalPeopleCount();

        // CustomKeypad�� ���� ����
        keypad.SetKeypadCombo(totalPeopleNumber.ToString());

        Debug.Log("������ " + totalPeopleNumber);
    }

    public void CorrectAnswer()
    {
        Debug.Log("����!");
        // ���� ����/�� �̵� �� �߰�
    }
    public void SetKeypadCombo(string combo)
    {
        correctCode = combo;
    }

}
