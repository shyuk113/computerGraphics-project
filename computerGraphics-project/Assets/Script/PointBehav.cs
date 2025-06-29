using UnityEngine;
/*
 C011145 이성혁 컴퓨터 그래픽스 2분반
 */

public class PointBehav : MonoBehaviour {
    public LightSourceBehav LightSource { set; private get; }

    private Vector3 _position { get => transform.position; }
    private Vector3 _normal { get => new Vector3(0, 0, 1); }

    //Material
    private float _ambientConstant = 0.2f;
    private float _diffuseConstant = 0.8f;
    private float _specularConstant = 0.5f;
    private float _specularShininessConstant = 32;

    void Update() {
        float intensity = GetIntensity();
        SetScale(intensity);
    }

    private void SetScale(float value) {
        transform.localScale = new Vector3(value, value, value);
    }

    private Vector3 GetCameraPosition() {
        Camera camera = Camera.main;
        Vector3 pos = camera.gameObject.transform.position;
        return pos;
    }

    private float GetIntensity() {
        Vector3 L = (LightSource.Position - _position).normalized;
        Vector3 N = _normal.normalized;
        Vector3 R = (2 * Vector3.Dot(L, N) * N - L).normalized;
        Vector3 V = (GetCameraPosition()-_position).normalized;

        float ambient = _ambientConstant * LightSource.Ambient;
        float diffuse = _diffuseConstant * LightSource.Diffuse * Mathf.Max(0f, Vector3.Dot(L, N));
        float specular = _specularConstant * LightSource.Specular * Mathf.Pow(Mathf.Max(0f, Vector3.Dot(R, V)), _specularShininessConstant);

        return ambient + diffuse + specular;
    }
}
