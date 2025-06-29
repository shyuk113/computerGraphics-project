using UnityEngine;
using NavKeypad; // Ű�е� �ּ� ���ӽ����̽� ���

// DoorClickInteraction �ڵ�� ����
public class KeypadButtonClickInteraction : MonoBehaviour
{
    public float interactDistance = 5f; // ��ȣ�ۿ� �Ÿ�

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