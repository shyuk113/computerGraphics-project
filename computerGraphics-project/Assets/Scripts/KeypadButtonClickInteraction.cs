using UnityEngine;
using NavKeypad; // 키패드 애셋 네임스페이스 사용

// DoorClickInteraction 코드랑 같음
public class KeypadButtonClickInteraction : MonoBehaviour
{
    public float interactDistance = 5f; // 상호작용 거리

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                KeypadButton keypadButton = hit.collider.GetComponent<KeypadButton>();
                if (keypadButton != null)
                {
                    keypadButton.PressButton();
                }
            }
        }

    }
}