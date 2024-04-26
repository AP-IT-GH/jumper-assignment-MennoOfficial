using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


public class MLAgentPlayer : Agent
{
    public float Force = 15f;
    private Rigidbody rb = null;
    public Transform orig = null;
    private bool isGrounded = false;
    private SphereMovement sphereMovement;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        orig.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        sphereMovement = FindObjectOfType<SphereMovement>();
        sphereMovement.OnWallEndCollision.AddListener(OnWallEndCollisionHandler);
    }

    private void OnWallEndCollisionHandler()
    {
        Debug.Log("+1f");
        AddReward(1f);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Movement
        int action = actions.DiscreteActions[0];

        if (action == 1 && isGrounded)
        {
            Jump();
            AddReward(-0.1f);
            Debug.Log("-0.1f");
        }


    }

    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
        Debug.Log("OnEpisodeBegin: " + isGrounded);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * Force, ForceMode.Impulse);
        isGrounded = false;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
        if (discreteActionsOut[0] == 1 && isGrounded)
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("collidedWithSphere with the ball!");
            Debug.Log("-1f");
            SetReward(-1f);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }

    private void ResetMyAgent()
    {
        transform.position = new Vector3(orig.position.x, orig.position.y, orig.position.z);
        isGrounded = true;
    }

    public override void CollectObservations(VectorSensor sensor) { }
}
