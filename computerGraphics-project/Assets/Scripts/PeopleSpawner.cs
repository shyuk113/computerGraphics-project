using UnityEngine;
using System.Collections.Generic;

public class PeopleSpawner : MonoBehaviour
{
    [Header("지정 위치 프리팹(SpawnPoint) 태그")]
    public string spawnPointTag = "SpawnPoint";

    [Header("지정 위치에 소환할 프리팹(랜덤)")]
    public List<GameObject> fixedPrefabs;

    [Header("지정 위치 소환 확률 (0~1)")]
    [Range(0, 1)] public float fixedSpawnChance = 1.0f; // 1.0=무조건 소환, 0.5=50% 확률

    [Header("랜덤 위치에 소환할 프리팹(랜덤)")]
    public List<GameObject> randomPrefabs;

    [Header("랜덤 위치에 소환할 총 인원 수 범위")]
    public int minRandomCount = 50;
    public int maxRandomCount = 100;

    [Header("랜덤 소환 영역")]
    public Vector3 areaMin = new Vector3(-10, 0, -10);
    public Vector3 areaMax = new Vector3(10, 0, 10);

    [Header("겹침 방지 최소 거리")]
    public float minDistance = 1.5f;

    [Header("최대 위치 시도 횟수")]
    public int maxAttempts = 50;

    private Collider[] forbiddenZones;

    [Header("실시간 소환 카운트")]
    public int fixedSpawnedCount = 0;
    public int randomSpawnedCount = 0;

    void Start()
    {
        // "NoSpawn" 태그 처리
        GameObject[] noSpawnObjects = GameObject.FindGameObjectsWithTag("NoSpawn");
        List<Collider> zones = new List<Collider>();
        foreach (var obj in noSpawnObjects)
        {
            Collider col = obj.GetComponent<Collider>();
            if (col != null) zones.Add(col);
        }
        forbiddenZones = zones.ToArray();

        fixedSpawnedCount = 0;
        randomSpawnedCount = 0;

        // 1. 지정 위치 프리팹(SpawnPoint 태그) 모두 찾기
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(spawnPointTag);
        List<Vector3> usedPositions = new List<Vector3>();
        foreach (GameObject point in spawnPoints)
        {
            // 소환 확률 적용 (예: 0.7이면 70% 확률로 소환)
            if (Random.value > fixedSpawnChance)
                continue;

            Vector3 spawnPos = point.transform.position;
            Quaternion spawnRot = point.transform.rotation;

            if (IsPositionValid(spawnPos))
            {
                int prefabIndex = Random.Range(0, fixedPrefabs.Count);
                GameObject prefab = fixedPrefabs[prefabIndex];
                GameObject person = Instantiate(prefab, spawnPos, spawnRot);
                person.tag = "Person";
                fixedSpawnedCount++;
                GameManager.Instance?.AddSummonedPerson();

            }
        }

        // 2. 랜덤 위치에 랜덤 프리팹 소환
        int randomCount = Random.Range(minRandomCount, maxRandomCount + 1);
        for (int i = 0; i < randomCount; i++)
        {
            Vector3 pos = FindNonOverlappingPosition();
            if (pos != Vector3.positiveInfinity)
            {
                int prefabIndex = Random.Range(0, randomPrefabs.Count);
                GameObject prefab = randomPrefabs[prefabIndex];
                Quaternion randomRotation = Quaternion.Euler(
      0,
      Random.Range(0f, 360f),
      0
  );

                GameObject person = Instantiate(prefab, pos, randomRotation);
                person.tag = "Person";
                randomSpawnedCount++;
                GameManager.Instance?.AddSummonedPerson();

            }
        }

        Debug.Log($"[PeopleSpawner] 지정 위치 소환: {fixedSpawnedCount}명, 랜덤 위치 소환: {randomSpawnedCount}명");
    }

    bool IsPositionValid(Vector3 pos)
    {
        foreach (var zone in forbiddenZones)
        {
            if (zone.bounds.Contains(pos)) return false;
        }
        Collider[] hits = Physics.OverlapSphere(pos, minDistance);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Person")) return false;
        }
        return true;
    }

    Vector3 FindNonOverlappingPosition()
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            float x = Random.Range(areaMin.x, areaMax.x);
            float z = Random.Range(areaMin.z, areaMax.z);
            float y = areaMin.y;

            Vector3 pos = new Vector3(x, y, z);

            if (!IsPositionValid(pos)) continue;

            return pos;
        }
        return Vector3.positiveInfinity;
    }
}
