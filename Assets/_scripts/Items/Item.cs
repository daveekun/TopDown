using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IPooledObject
{
    public ItemObject item;
    public int amount;
    Vector3 startpos;
    SpriteRenderer sr;

    void Start()
    {
        startpos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        startpos = transform.position;
        sr.sprite = item.sprite;
    }

    public void _Awake()
    {
        startpos = transform.position;
        sr.sprite = item.sprite;
    }
    private void Update()
    {
        transform.position = startpos + new Vector3(0, (Mathf.Sin(Time.time * 6) + 1f) / 6f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            int added_amount = col.GetComponent<Inventory_Manager>().AddItem(item, amount);
            if (amount == added_amount)
                gameObject.SetActive(false);
            else
                amount -= added_amount;
        }
    }
}
