using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab; // Inspector���� �÷��̾� ������ ����

    void Awake()
    {
        // "Player" �±װ� ���� ������Ʈ�� ������ �ڵ� ����
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
