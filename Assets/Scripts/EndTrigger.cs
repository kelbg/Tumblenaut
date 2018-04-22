using UnityEngine;

public class EndTrigger : MonoBehaviour {
    public GameObject victoryText;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            GameController.Instance.PauseGame();
            victoryText.SetActive(true);
        }
    }

}
