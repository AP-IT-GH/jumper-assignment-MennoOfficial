using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    public float jumpForce = 10.0f;
    private Rigidbody rb;
    private bool canJump = true;
    private bool collidedWithSphere = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("Spacebar pressed");
    }
}
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0, 0.5f, 0);
        rb.velocity = Vector3.zero;
        canJump = false;
        collidedWithSphere = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(canJump ? 1.0f : 0.0f);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        bool jumpAction = actionBuffers.DiscreteActions[0] == 1;

        if (jumpAction && canJump)
        {
            Debug.Log("JUMPED");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
        if (collidedWithSphere)
        {
            SetReward(-0.5f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Adjust tag as needed
        {
            canJump = true; // Update canJump when colliding with the ground
        }
        else if (collision.gameObject.CompareTag("Sphere"))
        {
            collidedWithSphere = true;
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        Debug.Log("Hello Heuristic!");
        var discreteActionsOut = actionsOut.DiscreteActions;
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndMarker"))
        {
            SetReward(1.0f);
            EndEpisode();
        }
    }
}
