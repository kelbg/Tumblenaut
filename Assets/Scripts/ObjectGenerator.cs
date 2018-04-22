using UnityEngine;

public class ObjectGenerator : MonoBehaviour {
    public GameObject[] debrisPrefabs;
    public const int Length = 500;
    public const int MaxForce = 120;
    public const int MaxObjects = 2000;
    private float minSpacingX = 2f;
    private float maxSpacingX = 10f;
    private float minHeightY = -8f;
    private float maxHeightY = 8f;
    private float minScale = 2;
    private float maxScale = 3;
    private Vector2 lastPos;

    private void Start() {
        lastPos = Vector2.zero;
        Generate();
    }

    private void Generate() {
        int count = 0;

        while (lastPos.x < Length) {
            float posX = Random.Range(minSpacingX, maxSpacingX);
            float posY = Random.Range(minHeightY, maxHeightY);
            int index = Random.Range(0, debrisPrefabs.Length);

            Vector2 pos = new Vector2(lastPos.x + posX, posY);
            GameObject newGO = Instantiate(debrisPrefabs[index], pos, Quaternion.identity);

            Vector2 force = new Vector2(Random.Range(0f, MaxForce), Random.Range(0f, MaxForce));
            float scale = Random.Range(minScale, maxScale);
            newGO.transform.Rotate(0, 0, Random.Range(0f, 360));
            newGO.transform.localScale = new Vector2(scale, scale);
            newGO.GetComponent<Rigidbody2D>().AddForceAtPosition(force, Vector2.right);
            

            lastPos = pos;
            count++;

            // HACK: Prevents freezing
            if (count > MaxObjects) {
                Debug.LogError("Something has gone terribly wrong. [Too many objects generated]");
                break;
            }
        }

        print(count + " objects generated.");
    }

}
