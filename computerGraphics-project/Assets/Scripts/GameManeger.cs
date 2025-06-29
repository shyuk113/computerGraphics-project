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

    // 소환 인원 추가 (사람을 소환할 때마다 호출)
    public void AddSummonedPerson()
    {
        if (!summonedPeoplePerFloor.ContainsKey(currentFloor))
            summonedPeoplePerFloor[currentFloor] = 0;
        summonedPeoplePerFloor[currentFloor]++;
        totalPeopleCount++;

        // 전체 인원수 디버그 로그
        Debug.Log($"[GameManager] 전체 소환 인원: {totalPeopleCount}");
    }

    public int GetTotalPeopleCount()
    {
        // 필요할 때 호출해도 디버그 로그 출력
        Debug.Log($"[GameManager] 전체 소환 인원(조회): {totalPeopleCount}");
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
        Debug.Log("[GameManager] 소환 인원 초기화됨 (전체 0명)");
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
