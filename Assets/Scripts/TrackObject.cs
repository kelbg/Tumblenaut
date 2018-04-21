using UnityEngine;

public class TrackObject : MonoBehaviour {
    public GameObject target;
    public GameObject ship;
    public const float MinHeightY = -4;
    public const float MaxHeightY = 4;
    private Vector3 offset;
    private float shipColliderSizeX;

    private void Start() {
        offset = transform.position;
        shipColliderSizeX = ship.transform.GetComponent<Collider2D>().bounds.size.x;
    }

    private void Update() {
        if (target.transform.position.x > transform.position.x - offset.x && target.transform.position.x < ship.transform.position.x - shipColliderSizeX) {
            TrackX();
        }

        if (target.transform.position.y > MinHeightY && target.transform.position.y < MaxHeightY) {
            TrackY();
        }
    }

    private void TrackX() {
        transform.position = new Vector3(target.transform.position.x + offset.x, transform.position.y, transform.position.z);
    }

    private void TrackY() {
        transform.position = new Vector3(transform.position.x, target.transform.position.y + offset.y, transform.position.z);
    }

}
