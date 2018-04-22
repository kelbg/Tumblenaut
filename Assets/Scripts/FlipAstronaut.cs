using UnityEngine;

public class FlipAstronaut : MonoBehaviour {
    public Transform Hand;

    private void Update() {
        if (!GameController.GamePaused) {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (HandAngle > 0 && HandAngle < 180 && !sr.flipX) {
                sr.flipX = true;
            } else if (HandAngle < 0 && HandAngle > -180 && sr.flipX) {
                sr.flipX = false;
            }
        }
    }

    public float HandAngle {
        get {
            return Vector2.SignedAngle(transform.up, Hand.right);
        }
    }

}
