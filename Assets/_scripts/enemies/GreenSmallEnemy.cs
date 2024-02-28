using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSmallEnemy : EnemyAI
{
    public string bulletTag;
    public float lastAttacked;
    public override void Attack()
    {
        if (lastAttacked + attackSpeed < Time.time)
        {
            GameObject bullet = ObjectPooler.Instance.Spawn(bulletTag, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().dir = target.position - transform.position;
            bullet = ObjectPooler.Instance.Spawn(bulletTag, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().dir = target.position - transform.position + new Vector3(1f, 0);
            bullet = ObjectPooler.Instance.Spawn(bulletTag, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>().dir = target.position - transform.position + new Vector3(-1f, 0);
            lastAttacked = Time.time;
        }
        base.Attack();

    }
}
