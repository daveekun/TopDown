using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum state
{
    idle,
    chase,
    attack,
    checkLast
}

public abstract class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Vector3 lastKnowPos;
    public state State = state.idle;
    public bool hasVision;
    public float visionDistance;
    public LayerMask wall;
    public bool debug;

    public float attackRange;
    public float attackSpeed;

    public float moveSpeed;
    public float smalNum = 0.05f;

    private void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, visionDistance);
            if (hasVision)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, (target.position - transform.position).normalized * visionDistance + transform.position);
            }
            else
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, lastKnowPos);
            }
        }
    }

    private void Awake()
    {
        lastKnowPos = transform.position;
    }

    private void RequestRange()
    {
        if (Vector3.Distance(target.position, transform.position) < visionDistance)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, target.position - transform.position, visionDistance, wall);
            if (hit)
            { 
                hasVision = false;
                State = state.checkLast;
            }
            else
            {
                lastKnowPos = target.position;
                hasVision = true;
                State = state.chase;
            }
        }
        else
        {
            hasVision = false;
            State = state.checkLast;
        }
        if (hasVision)
        {
            if (Vector3.Distance(lastKnowPos, transform.position) < attackRange)
                State = state.attack;
            else
                State = state.chase;
        }
    }

    private void FixedUpdate()
    {
        RequestRange();
        switch (State)
        {
            case state.idle:
                Idle();
                break;
            case state.attack:
                Attack();
                break;
            case state.checkLast:
                CheckLast();
                break;
            case state.chase:
                Chase();
                break;
        }
    }

    public void CheckLast()
    {
        if ((lastKnowPos - transform.position).magnitude > smalNum)
            transform.position += (lastKnowPos - transform.position).normalized * Time.fixedDeltaTime * moveSpeed;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public virtual void Attack()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public virtual void Chase()
    {
        transform.position += (target.position - transform.position).normalized * Time.fixedDeltaTime * moveSpeed;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    public virtual void Idle()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

