using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹壳管理器.
/// </summary>
public class Shell : MonoBehaviour
{
    private Transform m_Transform;
    private Vector3 randomVector;

    void Start()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        randomVector = new Vector3(Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f));
    }

    void Update()
    {
        m_Transform.Rotate(randomVector);
    }
}
