using UnityEngine;

public class LightSourceBehav : MonoBehaviour {
    public Vector3 Position { get => transform.position; }

    public float Ambient = 1;
    public float Diffuse = 1;
    public float Specular = 1;

    private float _radius = 32;
    private float _height = 32;

    void Update() {
        float theta = Time.time;
       
        Vector3 position = new Vector3(_radius*Mathf.Cos(2*theta), 0, _height);
        transform.position = position;
    }
}