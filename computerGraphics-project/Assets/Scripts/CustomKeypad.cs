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
        // CustomKeypad 오브젝트 찾기
        GameObject keypadObject = GameObject.Find("Keypad");
        if (keypadObject == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' 오브젝트를 찾지 못했습니다!");
            return;
        }
        keypad = keypadObject.GetComponent<CustomKeypad>();
        if (keypad == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' 오브젝트에 CustomKeypad 스크립트가 없습니다!");
            return;
        }

        // GameManager에서 정답(총 소환 인원) 가져오기
        totalPeopleNumber = GameManager.Instance.GetTotalPeopleCount();

        // CustomKeypad에 정답 세팅
        keypad.SetKeypadCombo(totalPeopleNumber.ToString());

        Debug.Log("정답은 " + totalPeopleNumber);
    }

    public void CorrectAnswer()
    {
        Debug.Log("정답!");
        // 엔딩 연출/씬 이동 등 추가
    }
    public void SetKeypadCombo(string combo)
    {
        correctCode = combo;
    }

}
