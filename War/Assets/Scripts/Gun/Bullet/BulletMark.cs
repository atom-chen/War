﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹痕管理器.
/// </summary>
public class BulletMark : MonoBehaviour
{
    private Texture2D m_MainTexture;                    // 模型主贴图--源、弹痕消除.
    private Texture2D m_MainTexture_Bak;                // 模型主贴图备份--弹痕融合.

    private Texture2D m_BulletMark;                     // 弹痕贴图.

    private Queue<Vector2> bulletMarkQueue;             // 弹痕UV坐标队列.

    void Start()
    {
        FindAndLoadInit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_MainTexture = gameObject.GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
        m_MainTexture_Bak = GameObject.Instantiate<Texture2D>(m_MainTexture);

        gameObject.GetComponent<MeshRenderer>().material.mainTexture = m_MainTexture_Bak;

        m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/Bullet Decal_Wood");

        bulletMarkQueue = new Queue<Vector2>();
    }

    /// <summary>
    /// 弹痕融合.
    /// </summary>
    public void CreateBulletMark(RaycastHit hit)
    {
        Vector2 uv = hit.textureCoord;
        bulletMarkQueue.Enqueue(uv);

        for (int i = 0; i < m_BulletMark.width; ++i)
        {
            for (int j = 0; j < m_BulletMark.height; ++j)
            {
                float x = m_MainTexture.width * uv.x - m_BulletMark.width / 2 + i;
                float y = m_MainTexture.height * uv.y - m_BulletMark.height / 2 + j;

                Color color = m_BulletMark.GetPixel(i, j);

                if (color.a > 0.2f)
                    m_MainTexture_Bak.SetPixel((int)x, (int)y, color);
            }
        }

        m_MainTexture_Bak.Apply();

        Invoke("RemoveBulletMark", 5);
    }

    /// <summary>
    /// 弹痕移除.
    /// </summary>
    private void RemoveBulletMark()
    {
        if (bulletMarkQueue.Count > 0)
        {
            Vector2 uv = bulletMarkQueue.Dequeue();

            for (int i = 0; i < m_BulletMark.width; ++i)
            {
                for (int j = 0; j < m_BulletMark.height; ++j)
                {
                    float x = m_MainTexture.width * uv.x - m_BulletMark.width / 2 + i;
                    float y = m_MainTexture.height * uv.y - m_BulletMark.height / 2 + j;

                    Color color = m_MainTexture.GetPixel((int)x, (int)y);
                    m_MainTexture_Bak.SetPixel((int)x, (int)y, color);
                }
            }

            m_MainTexture_Bak.Apply();
        }
    }
}
