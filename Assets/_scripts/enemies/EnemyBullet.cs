using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour, IPooledObject
{
    public int damage;
    public float speed;
    public Vector3 dir;
    bool canMove = true;
    public LayerMask lm;

    public void _Awake()
    {
        canMove = true;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, speed * Time.fixedDeltaTime, lm);
            if (hit)
                transform.position = new Vector3(hit.point.x, hit.point.y);
            else
                transform.position += dir.normalized * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            ObjectPooler.Instance.Spawn("bullet_hit", transform.position, Quaternion.identity);
            canMove = false;
            gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            ObjectPooler.Instance.Spawn("bullet_hit", transform.position, Quaternion.identity);
            canMove = false;
            gameObject.SetActive(false);
        }
    }
}
