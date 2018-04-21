using System.Collections;
using UnityEngine;

// Defines resources and starting amounts
public enum Resources {
    Oxygen = 10,
    Fuel = 400
}

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    public float CurrentOxygen { get; private set; }
    public float CurrentFuel { get; private set; }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        CurrentOxygen = (float)Resources.Oxygen;
        CurrentFuel = (float)Resources.Fuel;
        StartCoroutine(DepleteOxygen());
    }

    public void ConsumeResource(Resources resource, float amount) {
        switch (resource) {
            case Resources.Oxygen:
                CurrentOxygen -= amount;
                break;
            case Resources.Fuel:
                CurrentFuel -= amount;
                break;
        }
    }

    // Consumes 1 unit of oxygen every second; Configurable
    IEnumerator DepleteOxygen(float amount = 1f, float delay = 1f) {
        while (CurrentOxygen > 0) {
            ConsumeResource(Resources.Oxygen, amount);
            yield return new WaitForSeconds(delay);
        }
        print("Game Over!");
        yield break;
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 30, 100, 25), "Oxygen: " + CurrentOxygen.ToString("F1"));
        GUI.Label(new Rect(10, 50, 100, 25), "Fuel: " + CurrentFuel.ToString("F2"));
    }

}
