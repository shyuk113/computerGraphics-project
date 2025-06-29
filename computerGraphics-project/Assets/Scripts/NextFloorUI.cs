using UnityEngine;

public class NextFloorUI : MonoBehaviour
{
    public GameObject NextFloorPanel; // Inspector���� ����
    public MonoBehaviour[] controlScripts; // �÷��̾� ���� ��ũ��Ʈ ����

    // OK ��ư���� ȣ��
    public void OnOK()
    {
        Resume();
        // ���� ������ �̵�
        var FloorManager = FindObjectOfType<FloorManager>();
        if (FloorManager != null)
        {
            FloorManager.LoadNextFloor();
        }
        else
        {
            Debug.LogWarning("[NextFloorUI] FloorManager�� ã�� �� �����ϴ�!");
        }
    }

    // NO ��ư���� ȣ��
    public void OnNo()
    {
        Resume();
        // �߰� �ൿ�� �ʿ��ϴٸ� ���⿡ �ۼ�
    }

    // �Ͻ����� ����(����)
    private void Resume()
    {
        if (NextFloorPanel != null)
            NextFloorPanel.SetActive(false);

        // �÷��̾� ��Ʈ�ѷ� ����
        if (controlScripts != null)
        {
            foreach (var script in controlScripts)
                if (script != null) script.enabled = true;
        }

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
