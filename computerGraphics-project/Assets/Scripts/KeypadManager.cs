using UnityEngine;
using UnityEngine.SceneManagement;
using NavKeypad;
using System.Collections;

public class KeypadManager : MonoBehaviour
{
    private Keypad keypad;

    [Header("정답 맞췄을 때 이동할 씬 이름")]
    public string endingSceneName = "Ending Scene"; // Inspector에서 원하는 씬 이름 입력

    void Start()
    {
        GameObject keypadObject = GameObject.Find("Keypad");
        if (keypadObject == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' 오브젝트를 찾지 못했습니다!");
            return;
        }
        keypad = keypadObject.GetComponent<Keypad>();
        if (keypad == null)
        {
            Debug.LogError("[KeypadManager] 'Keypad' 오브젝트에 Keypad 스크립트가 없습니다!");
            return;
        }

        // 정답 맞췄을 때 엔딩씬 이동 함수 연결!
        keypad.OnAccessGranted.AddListener(OnCorrectAnswer);

        // 코루틴으로 한 프레임 대기 후 값 세팅 (실행 순서 꼬임 방지)
        StartCoroutine(SetComboLate());
    }

    IEnumerator SetComboLate()
    {
        yield return null; // 한 프레임 대기
        int totalPeopleNumber = GameManager.Instance.GetTotalPeopleCount();
        keypad.SetKeypadCombo(totalPeopleNumber); // Keypad가 int를 받는다고 가정
        Debug.Log($"[KeypadManager] 정답 세팅: {totalPeopleNumber}");
    }

    // 정답 맞췄을 때 자동 호출
    void OnCorrectAnswer()
    {
        Debug.Log("[KeypadManager] 정답! 엔딩씬 이동!");
        SceneManager.LoadScene("Ending Scene");
    }
}
