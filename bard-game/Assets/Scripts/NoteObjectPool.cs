using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NoteObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 100;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(prefab, transform),
            actionOnGet: obj => {},
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: obj => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public GameObject Get()
    {
        return pool.Get();
    }

    public GameObject Get(Vector3 position)
    {
        GameObject obj = pool.Get();
        obj.transform.position = position;
        return obj;
    }

    public void Return(GameObject obj)
    {
        pool.Release(obj);
    }
}
        
    