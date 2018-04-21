using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject target;
    public const float MinHeightY = -8;
    public const float MaxHeightY = 8;

    private void Update() {
        if (target.transform.position.x > transform.position.x) {
            TrackX();
        }
        if (target.transform.position.y > MinHeightY && target.transform.position.y < MaxHeightY) {
            TrackY();
        }
    }

    private void TrackX() {
        Vector3 newPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }

    private void TrackY() {
        Vector3 newPos = new Vector3(transform.position.x, target.transform.position.y, transform.position.z);
        transform.position = newPos;
    }

}
