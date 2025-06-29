using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public enum CursorMode
    {
        LockedAndHidden,
        UnlockedAndVisible
    }

    public CursorMode mode = CursorMode.LockedAndHidden;

    void Start()
    {
        ApplyCursorMode();
    }

    public void ApplyCursorMode()
    {
        if (mode == CursorMode.LockedAndHidden)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
