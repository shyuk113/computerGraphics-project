using UnityEngine;

public class PlaneBehav : MonoBehaviour {
    public GameObject PointPrefab;
    public GameObject LightSource;

    private int _w = 64, _h = 64;
    public int Width { get => _w; }
    public int Height { get => _h; }

    private PointBehav[] _points; // Change to store PixelBehav references

    void Start() {
        // Initialize the _points array
        _points = new PointBehav[Height * Width];

        // Instantiate the PointPrefab in a grid
        for (int y = 0; y < Height; y++) {
            for (int x = 0; x < Width; x++) {
                // Instantiate the prefab at position
                Vector3 position = new Vector3(-Width/2 + x, -Height/2 + y, 0);
                var pointGameObject = Instantiate(PointPrefab, position, Quaternion.identity);

                // Get the PixelBehav component and store it in the _points array
                var pointBehavior = pointGameObject.GetComponent<PointBehav>();
                pointBehavior.LightSource = LightSource.GetComponent<LightSourceBehav>();
                _points[y*Width + x] = pointBehavior;
            }
        }
    }

    public PointBehav GetPoint(int idx) {
        return _points[idx];
    }
}