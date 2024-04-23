using UnityEngine;

public class SphereMovement : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private float moveSpeed;
    private bool episodeStarted = false;

    void Start()
    {
        RestartEpisode();
    }

    void Update()
    {
        if (!episodeStarted)
        {
            moveSpeed = Random.Range(5.0f, 10.0f);
            episodeStarted = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, endMarker.position, moveSpeed * Time.deltaTime);

        // Check if the sphere has reached the end marker
        if (transform.position == endMarker.position)
        {
            RestartEpisode();
        }
    }

    void RestartEpisode()
    {
        transform.position = startMarker.position;
        episodeStarted = false;
    }
}
