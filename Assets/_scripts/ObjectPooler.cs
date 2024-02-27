using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    public Queue<GameObject> queue;
    public GameObject prefab;
    public string tag;
    public int size;
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;
    [NonReorderable]
    public List<Pool> pools;
    void Start()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(this);
        foreach (Pool pool in pools)
        {
            pool.queue = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject current = Instantiate(pool.prefab, transform.position, Quaternion.identity, transform);
                current.SetActive(false);
                pool.queue.Enqueue(current);
            }
        }
    }
    public GameObject Spawn(string _tag, Vector3 _pos, Quaternion _rot)
    {
        foreach (Pool pool in pools)
        {
            if (pool.tag.Equals(_tag))
            {
                GameObject current = pool.queue.Dequeue();
                current.SetActive(false);
                current.transform.position = _pos;
                current.transform.rotation = _rot;
                current.SetActive(true);
                pool.queue.Enqueue(current);
                IPooledObject op = current.GetComponent<IPooledObject>();
                if (op != null)
                    op._Awake();
                return current;
            }
        }
        Debug.Log("No such tag");
        return (null);
    }
}
