using UnityEngine;

public class Player : MonoBehaviour {
    public const float forceMult = 120f;             // Force direction multiplier      
    public const float fuelConsumption = 0.01f;     // Base fuel consumption
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        //if (Input.GetAxis("Horizontal") != 0) {
        //    body.AddForce(Vector2.right * Input.GetAxis("Horizontal") * forceMult);
        //}

        //if (Input.GetAxis("Vertical") != 0) {
        //    body.AddForce(Vector2.up * Input.GetAxis("Vertical") * forceMult);
        //}

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseDir = mousePos - (Vector2)transform.position;
        mouseDir = mouseDir.normalized;
        //Debug.DrawLine(mouseDir, transform.position, Color.magenta);

        if (Input.GetMouseButton(0) && GameController.Instance.CurrentFuel >= fuelConsumption) {
            rb.AddForce(-mouseDir * forceMult);
            GameController.Instance.ConsumeResource(Resources.Fuel, fuelConsumption);
        }
    }

    private void OnGUI() {
        GUI.Label(new Rect(10, 10, 200, 20), "Vel: " + rb.velocity.ToString("F2"));
    }

}
