using UnityEngine;

public class EndTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            print("You did it!");
        }
    }

}
