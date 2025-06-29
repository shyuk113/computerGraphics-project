using UnityEngine;

public class DoorClickInteractor : MonoBehaviour
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
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    door.ToggleDoor();
                }
            }
        }

    }
}
