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
    private Transform target = GameManager.instance.Player;
    public state s = state.idle;

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

