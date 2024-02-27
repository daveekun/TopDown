using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance;
    public GameObject text;
    Queue<GameObject> queue = new Queue<GameObject>();
    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
        for (int i = 0; i < 100; i++)
        {
            GameObject g = Instantiate(text, transform.position, Quaternion.identity, transform);
            queue.Enqueue(g);
            g.SetActive(false);
        }
    }

    public void SpawnText(string _text, Color _color, Vector3 _p)
    {
        GameObject text = queue.Dequeue();
        queue.Enqueue(text);
        text.SetActive(true);
        text.transform.position = _p;
        Text t = text.GetComponent<Text>();
        t.text = _text;
        t.color = _color;
        text.GetComponent<FloatingText>()._Awake();
    }
}
