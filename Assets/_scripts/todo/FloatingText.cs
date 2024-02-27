using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour, IPooledObject
{
    public void _Awake()
    {
        StartCoroutine(FloatCoroutine());
    }

    void Update()
    {
        transform.position += new Vector3(0, .1f) * Time.deltaTime; 
    }

    IEnumerator FloatCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
