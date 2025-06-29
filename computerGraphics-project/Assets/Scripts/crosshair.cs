using UnityEngine;

public class CrosshairSpawner : MonoBehaviour
{
    public GameObject crosshairPrefab;
    private static GameObject crosshairInstance;

    void Awake()
    {
        if (crosshairInstance == null)
        {
            crosshairInstance = Instantiate(crosshairPrefab);
            DontDestroyOnLoad(crosshairInstance);
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        FPSController fpsController = null;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            return;
        fpsController = player.GetComponent<FPSController>();
        if (fpsController != null) { fpsController.setCamLock(false); }

    }
}
