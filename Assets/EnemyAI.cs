using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum state
{
    idle,
    chase,
    attack,
}

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public state State = state.idle;
    public bool hasVision;
    public float visionDistance;
    public LayerMask wall;
    public bool debug;
    private bool drawRay = true;

    private void OnDrawGizmos()
    {
        if (drawRay)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (target.position - transform.position).normalized * visionDistance + transform.position);
        }
        if (debug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionDistance);
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(target.position, transform.position) < visionDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, visionDistance, wall);
            if (hit)
            { 
                hasVision = false;
                drawRay = false;
            }
            else
            {
                hasVision = true;
                drawRay = true;
            }
        }
        else
        {
            hasVision = false;
            drawRay = false;
        }
    }

    void Attack()
    {

    }

    void Chase()
    {

    }
    void Idle()
    {

    }
}

