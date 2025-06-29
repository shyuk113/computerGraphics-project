using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab; // Inspector에서 플레이어 프리팹 연결

    void Awake()
    {
        // "Player" 태그가 붙은 오브젝트가 없으면 자동 생성
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
