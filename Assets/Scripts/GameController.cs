using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Defines resources and starting amounts
public enum Resources {
    Oxygen = 75,
    Fuel = 30
}

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public Image fade;
    public GameObject gameOver;
    public Slider oxygenSlider;
    public Slider fuelSlider;
    public Slider progressSlider;
    public AudioSource bgmSource;
    public float CurrentOxygen { get; private set; }
    public float CurrentFuel { get; private set; }
    private GameObject player;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");

        oxygenSlider.maxValue = (float)Resources.Oxygen;
        fuelSlider.maxValue = (float)Resources.Fuel;
        oxygenSlider.value = oxygenSlider.maxValue;
        fuelSlider.value = fuelSlider.maxValue;
        progressSlider.maxValue = ObjectGenerator.Length;
        progressSlider.value = progressSlider.minValue;

        CurrentOxygen = (float)Resources.Oxygen;
        CurrentFuel = (float)Resources.Fuel;
        StartCoroutine(DepleteOxygen());
    }

    private void Update() {
        progressSlider.value = player.transform.position.x;

        if (Input.GetKeyDown(KeyCode.Space)) {
            ResumeGame(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.M)) {
            if (bgmSource.isPlaying) {
                bgmSource.Stop();
            } else {
                bgmSource.Play();
            }
        }
    }

    public void ConsumeResource(Resources resource, float amount) {
        switch (resource) {
            case Resources.Oxygen:
                CurrentOxygen -= amount;
                oxygenSlider.value = CurrentOxygen;
                break;
            case Resources.Fuel:
                CurrentFuel -= amount;
                fuelSlider.value = CurrentFuel;
                break;
        }
    }

    // Consumes 1 unit of oxygen every second by default
    IEnumerator DepleteOxygen(float amount = 1f, float delay = 1f) {
        while (CurrentOxygen > 0) {
            ConsumeResource(Resources.Oxygen, amount);
            yield return new WaitForSeconds(delay);
        }
        PauseGame();
        gameOver.SetActive(true);
        yield break;
    }

    public static bool GamePaused { get { return Time.timeScale == 0 ? true : false; } }

    public void PauseGame(bool fade = true) {
        if (fade) {
            this.fade.CrossFadeAlpha(128f, 0.25f, true);
        }
        Time.timeScale = 0;
    }

    public void ResumeGame(bool fade = true) {
        if (fade) {
            this.fade.CrossFadeAlpha(0f, 0.25f, true);
        }
        Time.timeScale = 1;
    }

}
