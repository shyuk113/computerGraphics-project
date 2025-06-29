using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int currentFloor = 1;
    public int totalFloors = 10;

    private int totalPeopleCount = 0;
    private Dictionary<int, int> summonedPeoplePerFloor = new Dictionary<int, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ��ȯ �ο� �߰� (����� ��ȯ�� ������ ȣ��)
    public void AddSummonedPerson()
    {
        if (!summonedPeoplePerFloor.ContainsKey(currentFloor))
            summonedPeoplePerFloor[currentFloor] = 0;
        summonedPeoplePerFloor[currentFloor]++;
        totalPeopleCount++;

        // ��ü �ο��� ����� �α�
        Debug.Log($"[GameManager] ��ü ��ȯ �ο�: {totalPeopleCount}");
    }

    public int GetTotalPeopleCount()
    {
        // �ʿ��� �� ȣ���ص� ����� �α� ���
        Debug.Log($"[GameManager] ��ü ��ȯ �ο�(��ȸ): {totalPeopleCount}");
        return totalPeopleCount;
    }

    public int GetCurrentFloorPeopleCount()
    {
        if (summonedPeoplePerFloor.ContainsKey(currentFloor))
            return summonedPeoplePerFloor[currentFloor];
        return 0;
    }

    public void ResetPeopleCount()
    {
        totalPeopleCount = 0;
        summonedPeoplePerFloor.Clear();
        Debug.Log("[GameManager] ��ȯ �ο� �ʱ�ȭ�� (��ü 0��)");
    }

    // GameManager.cs
    public void ResetAll()
    {
        currentFloor = 1;
        totalPeopleCount = 0;
        summonedPeoplePerFloor.Clear();
    }


    public void NextFloor()
    {
        currentFloor++;
    }
}
