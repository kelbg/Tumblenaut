using UnityEngine;

public class RotateArm : MonoBehaviour {

    private void Update() {
        if (!GameController.GamePaused) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = mousePos - (Vector2)transform.position;
            mouseDir = mouseDir.normalized;

            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}
