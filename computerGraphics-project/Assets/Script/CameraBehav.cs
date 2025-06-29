using UnityEngine;

public class CameraBehav : MonoBehaviour {
    private float _defaultZPosition = 64;
    private float _defaultYRotation = 180;

    void Start() {
        transform.position = new Vector3(0, 0, _defaultZPosition);
        transform.rotation = Quaternion.Euler(0, _defaultYRotation, 0);
    }

    void Update() {
        float theta = Time.time;
        float yPosition = Mathf.Cos(theta) * 32f;

        // Update the camera's position
        transform.position = new Vector3(0, yPosition, _defaultZPosition);

        // Make the camera look at the origin
        transform.LookAt(Vector3.zero);
    }
}
