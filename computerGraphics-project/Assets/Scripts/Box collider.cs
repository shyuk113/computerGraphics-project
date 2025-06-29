using UnityEngine;

public class BoxColliderAdder : MonoBehaviour
{
    void Start()
    {
        foreach (var obj in FindObjectsOfType<Transform>())
        {
            // ��: "NoCollider" �±װ� ���� ������Ʈ�� �ǳʶ�
            if (obj.CompareTag("No Collider")) continue;


            // �̹� Collider�� ������ ����
            if (obj.GetComponent<Collider>()) continue;

            // �ݶ��̴� �߰�
            obj.gameObject.AddComponent<MeshCollider>();
        }

    }
}
