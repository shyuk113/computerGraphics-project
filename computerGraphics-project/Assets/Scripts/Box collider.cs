using UnityEngine;

public class BoxColliderAdder : MonoBehaviour
{
    void Start()
    {
        foreach (var obj in FindObjectsOfType<Transform>())
        {
            // 예: "NoCollider" 태그가 붙은 오브젝트는 건너뜀
            if (obj.CompareTag("No Collider")) continue;


            // 이미 Collider가 있으면 생략
            if (obj.GetComponent<Collider>()) continue;

            // 콜라이더 추가
            obj.gameObject.AddComponent<MeshCollider>();
        }

    }
}
