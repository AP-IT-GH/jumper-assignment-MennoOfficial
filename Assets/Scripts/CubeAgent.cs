using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Linq;
using UnityEngine.UIElements;
public class CubeAgent : Agent
{
    public float jumpForce = 10.0f;
    public Transform Target;
    private bool canJump = true;

    public override void OnEpisodeBegin()
    {

        transform.localPosition = new Vector3(0, 0.5f, 0);
        transform.localRotation = Quaternion.identity;

        canJump = true;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(canJump);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        float jumpAction = actionBuffers.ContinuousActions[0];
        if (jumpAction > 0.5f && canJump)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false; // Prevent multiple jumps in a single action
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Sphere"))
        {
            SetReward(-1.0f);
            Debug.Log("collidedWithSphere with the ball!");
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;

            continuousActionsOut[0] = Input.GetAxis("Vertical");
    }
}

