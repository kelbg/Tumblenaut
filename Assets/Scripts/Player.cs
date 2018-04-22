using UnityEngine;

public class Player : MonoBehaviour {
    public const float forceMult = 250f;            // Force direction multiplier      
    public const float fuelConsumption = 0.01f;     // Base fuel consumption
    public const float collisionPitchDiv = 50;      // Modifier for collision sound pitch
    public const float collisionVolumeDiv = 10;     // Modifier for collision sound volume

    public GameObject psGO;
    private ParticleSystem ps;
    public AudioSource collisionSource;
    private AudioSource thrusterSource;
    private Rigidbody2D rb;

    private void Start() {
        ps = psGO.GetComponent<ParticleSystem>();
        thrusterSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (!GameController.GamePaused) {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mouseDir = mousePos - (Vector2)transform.position;
            mouseDir = mouseDir.normalized;

            if (Input.GetMouseButton(0) && GameController.Instance.CurrentFuel >= fuelConsumption) {
                rb.AddForce(-mouseDir * forceMult);
                GameController.Instance.ConsumeResource(Resources.Fuel, fuelConsumption);

                if (!thrusterSource.isPlaying) {
                    thrusterSource.Play();
                }

                if (!ps.emission.enabled) {
                    ToggleParticleEmission();
                }
            } else if (!Input.GetMouseButton(0) && ps.emission.enabled) {
                ToggleParticleEmission();
            } else if (!Input.GetMouseButton(0) && thrusterSource.isPlaying) {
                thrusterSource.Stop();
            }
        }
    }

    private void ToggleParticleEmission() {
        ParticleSystem.EmissionModule em = ps.emission;
        em.enabled = !em.enabled;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Asteroid") {
            collisionSource.pitch = 1 + (rb.velocity.magnitude / collisionPitchDiv);
            collisionSource.volume = rb.velocity.magnitude / collisionVolumeDiv;
            collisionSource.PlayOneShot(collisionSource.clip);
        }
    }

}
