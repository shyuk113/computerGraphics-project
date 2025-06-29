using UnityEngine;
using UnityEngine.SceneManagement;
using NavKeypad;
using System.Collections;

public class KeypadManager : MonoBehaviour
{
    private Keypad keypad;

    [Header("���� ������ �� �̵��� �� �̸�")]
    public string endingSceneName = "Ending Scene"; // Inspector���� ���ϴ� �� �̸� �Է�

    void Start()
    {
        GameObject keypadObject = GameObject.Find("Keypad");
        if (keypadObject == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' ������Ʈ�� ã�� ���߽��ϴ�!");
            return;
        }
        keypad = keypadObject.GetComponent<Keypad>();
        if (keypad == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' ������Ʈ�� Keypad ��ũ��Ʈ�� �����ϴ�!");
            return;
        }

        // ���� ������ �� ������ �̵� �Լ� ����!
        keypad.OnAccessGranted.AddListener(OnCorrectAnswer);

        // �ڷ�ƾ���� �� ������ ��� �� �� ���� (���� ���� ���� ����)
        StartCoroutine(SetComboLate());
    }

    IEnumerator SetComboLate()
    {
        yield return null; // �� ������ ���
        int totalPeopleNumber = GameManager.Instance.GetTotalPeopleCount();
        keypad.SetKeypadCombo(totalPeopleNumber); // Keypad�� int�� �޴´ٰ� ����
        Debug.Log($"[KeypadManager] ���� ����: {totalPeopleNumber}");
    }

    // ���� ������ �� �ڵ� ȣ��
    void OnCorrectAnswer()
    {
        Debug.Log("[KeypadManager] ����! ������ �̵�!");
        SceneManager.LoadScene("Ending Scene");
    }
}
