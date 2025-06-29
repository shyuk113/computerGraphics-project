using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SnappableObject))]
public class EditorMagneticSnap : Editor
{
    private const float SnapThreshold = 0.5f;

    void OnSceneGUI()
    {
        SnappableObject snapTarget = (SnappableObject)target;
        Transform t = snapTarget.transform;

        EditorGUI.BeginChangeCheck();
        Vector3 newPos = Handles.PositionHandle(t.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Bounds myBounds = GetTotalBounds(t.gameObject);
            GameObject[] all = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

            foreach (GameObject obj in all)
            {
                if (obj == t.gameObject || !obj.activeInHierarchy) continue;

                Bounds theirBounds = GetTotalBounds(obj);

                float myLeft = myBounds.min.x;
                float myRight = myBounds.max.x;
                float theirLeft = theirBounds.min.x;
                float theirRight = theirBounds.max.x;

                if (Mathf.Abs(myLeft - theirRight) < SnapThreshold)
                {
                    newPos = t.position + new Vector3(theirRight - myLeft, 0, 0);
                    break;
                }

                if (Mathf.Abs(myRight - theirLeft) < SnapThreshold)
                {
                    newPos = t.position + new Vector3(theirLeft - myRight, 0, 0);
                    break;
                }
            }

            Undo.RecordObject(t, "Snap Move");
            t.position = newPos;
        }
    }

    Bounds GetTotalBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0)
            return new Bounds(obj.transform.position, Vector3.zero);

        Bounds combined = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            combined.Encapsulate(renderers[i].bounds);
        }
        return combined;
    }
}
