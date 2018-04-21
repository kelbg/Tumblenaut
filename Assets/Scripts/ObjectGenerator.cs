using UnityEngine;

public class ObjectGenerator : MonoBehaviour {
    public GameObject[] debrisPrefabs;
    public const int Length = 500;
    private int minSpacingX = 2;
    private int maxSpacingX = 10;
    private int minHeightY = -8;
    private int maxHeightY = 8;
    private Vector2 lastPos;

    private void Start() {
        lastPos = Vector2.zero;
        Generate();
    }

    private void Generate() {
        int count = 0;

        while (lastPos.x < Length) {
            int posX = Random.Range(minSpacingX, maxSpacingX);
            int posY = Random.Range(minHeightY, maxHeightY);
            Vector2 pos = new Vector2(lastPos.x + posX, posY);
            Instantiate(debrisPrefabs[0], pos, Quaternion.identity);

            print("Object position: " + pos);
            lastPos = pos;
            count++;

            // HACK: Prevents freezing
            if (count > 2000) {
                print("Something has gone terribly wrong");
                break;
            }
        }

        print(count + " objects generated.");
    }

}
