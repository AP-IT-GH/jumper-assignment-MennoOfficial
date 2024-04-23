using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private float moveSpeed;
    private bool episodeStarted = false;

    private void Start()
    {
        transform.position = startMarker.position;
    }

    private void Update()
    {
        if (!episodeStarted)
        {
            // Generate a random movement speed for this episode
            moveSpeed = Random.Range(1.0f, 5.0f);
            episodeStarted = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, endMarker.position, moveSpeed * Time.deltaTime);

        if (transform.position == endMarker.position)
        {
            // End episode when sphere reaches end marker
            FindObjectOfType<CubeAgent>().EndEpisode();
            episodeStarted = false; // Reset flag for next episode
        }
    }
}
