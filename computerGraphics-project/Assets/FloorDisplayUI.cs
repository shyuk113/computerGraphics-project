using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FloorDisplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text floorText;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (floorText == null || FloorManager.Instance == null)
            return;

        int floor = 11 - FloorManager.Instance.GetCurrentFloor();
        floorText.text = $"{floor}F";
    }
}
