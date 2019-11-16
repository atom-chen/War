using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对象池临时资源管理.
/// </summary>
public class ObjectPool : MonoBehaviour
{
    private Queue<GameObject> objectPool;

    void Start()
    {
        objectPool = new Queue<GameObject>();
    }

    /// <summary>
    /// 添加数据到对象池.
    /// </summary>
    public void AddObject(GameObject obj)
    {
        objectPool.Enqueue(obj);
        obj.SetActive(false);
    }

    /// <summary>
    /// 从对象池取出数据.
    /// </summary>
    public GameObject GetObject()
    {
        GameObject obj = null;
        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.SetActive(true);
        }

        return obj;
    }

    /// <summary>
    /// 对象池是否为空.
    /// </summary>
    public bool IsEmpty()
    {
        return objectPool.Count == 0;
    }
}
