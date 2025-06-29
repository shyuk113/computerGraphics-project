using UnityEngine;

public class LetterArranger : MonoBehaviour
{
    public Transform[] letters; // �� ���ھ� ���� ������Ʈ�� Inspector�� ���
    public float spacing = 2.0f; // ���� ���� ����

    void Start()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].localPosition = new Vector3(i * spacing, 0, 0);
        }
    }
}
