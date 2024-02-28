using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject bullet;
    Vector2 mouseDir;
    public float reload;
    float lastshot;
    public bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && lastshot + reload < Time.time && canShoot)
        {
            Shoot();
            lastshot = Time.time;
        }
    }

    void Shoot()
    {
        GameObject Bullet = ObjectPooler.Instance.Spawn("bullet", transform.position, Quaternion.identity);
        mouseDir = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x,
                                Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y);
        Bullet.GetComponent<Bullet>().dir = mouseDir;
    }
}
