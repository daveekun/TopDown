using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public float speed;
    public Vector3 dir;
    public LayerMask lm;

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, speed * Time.fixedDeltaTime, lm);
        if (hit)
            transform.position = new Vector3(hit.point.x, hit.point.y);
        else
            transform.position += dir.normalized * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Damageable"))
        {
            col.gameObject.GetComponent<health>().TakeDamage(damage);
            ObjectPooler.Instance.Spawn("bullet_hit", transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            ObjectPooler.Instance.Spawn("bullet_hit", transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
