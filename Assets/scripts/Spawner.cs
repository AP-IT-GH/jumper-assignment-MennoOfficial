

using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab = null;
    public Transform spawnPoint1 = null;
    public Transform spawnPoint2 = null;
    public float minInterval = 6.0f;
    public float maxInterval = 10.0f;

    private void Start()
    {
        InvokeRepeating("Spawn", Random.Range(minInterval, maxInterval), Random.Range(minInterval, maxInterval));
    }

    private void Spawn()
    {
        // Randomly choose between spawnPoint1 and spawnPoint2
        Transform chosenSpawnPoint = Random.Range(0, 2) == 0 ? spawnPoint1 : spawnPoint2;

        // Instantiate the prefab at the chosen spawn point
        GameObject go = Instantiate(prefab, chosenSpawnPoint.position, Quaternion.identity);

        // Set the forward direction of the prefab to face along the negative Z-axis
        go.transform.forward = -chosenSpawnPoint.forward;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallend") == true)
        {
            Destroy(this.gameObject);
        }
    }
}
