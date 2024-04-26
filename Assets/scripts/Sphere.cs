using UnityEngine;
using UnityEngine.Events;

public class SphereMovement : MonoBehaviour
{
    public Transform startMarker1;
    public Transform endMarker1;
    public Transform startMarker2;
    public Transform endMarker2;
    public float minSpeed = 5.0f;
    public float maxSpeed = 10.0f;
    private float moveSpeed;
    private bool episodeStarted = false;
    private Transform randomStartMarker;
    private Transform randomEndMarker;
    public UnityEvent OnWallEndCollision;
    void Start()
    {
        RestartEpisode();
    }

    void Update()
    {
        if (!episodeStarted)
        {
            randomStartMarker = Random.Range(0, 2) == 0 ? startMarker1 : startMarker2;
            randomEndMarker = randomStartMarker == startMarker1 ? endMarker1 : endMarker2;

            moveSpeed = Random.Range(minSpeed, maxSpeed);

            transform.position = randomStartMarker.position;

            episodeStarted = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, randomEndMarker.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, randomEndMarker.position) < 0.01f)
        {
            RestartEpisode();
        }
    }

    void RestartEpisode()
    {
        episodeStarted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestartEpisode();
        }
        if (collision.gameObject.CompareTag("wallend"))
        {
            OnWallEndCollision.Invoke();
            RestartEpisode();
        }
    }
}
