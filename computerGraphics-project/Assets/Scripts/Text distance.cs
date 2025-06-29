using UnityEngine;

public class LetterArranger : MonoBehaviour
{
    public Transform[] letters; // 한 글자씩 만든 오브젝트를 Inspector에 등록
    public float spacing = 2.0f; // 글자 사이 간격

    void Start()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            letters[i].localPosition = new Vector3(i * spacing, 0, 0);
        }
    }
}
