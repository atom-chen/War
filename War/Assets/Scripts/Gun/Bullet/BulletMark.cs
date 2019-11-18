using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹痕管理器.
/// </summary>
public class BulletMark : MonoBehaviour
{
    [SerializeField]
    private MaterialType m_MaterialType;                // 当前模型材质类型.

    private Texture2D m_MainTexture;                    // 模型主贴图--源、弹痕消除.
    private Texture2D m_MainTexture_Bak;                // 模型主贴图备份--弹痕融合.

    private Texture2D m_BulletMark;                     // 弹痕贴图.
    private GameObject prefab_Effect;                   // 子弹命中特效.
    private Transform effectParent;                     // 特效父物体.

    private Queue<Vector2> bulletMarkQueue;             // 弹痕UV坐标队列.

    private ObjectPool objectPool;                      // 对象池管理.

    [SerializeField]
    private int hp;                                     // 环境物体的"生命值".

    public int Hp 
    { 
        get => hp;
        set
        {
            hp = value;
            if (hp <= 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

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

        switch (m_MaterialType)
        {
            case MaterialType.WOOD:
                TypeInit("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Wood_Parent");
                break;

            case MaterialType.METAL:
                TypeInit("Bullet Decal_Metal", "Bullet Impact FX_Metal", "Effect_Metal_Parent");
                break;

            case MaterialType.STONE:
                TypeInit("Bullet Decal_Stone", "Bullet Impact FX_Stone", "Effect_Stone_Parent");
                break;
        }

        bulletMarkQueue = new Queue<Vector2>();

        objectPool = gameObject.AddComponent<ObjectPool>();
    }

    /// <summary>
    /// 不同材质相关资源加载.
    /// </summary>
    private void TypeInit(string bulletMarkName, string effectName, string effectParentName)
    {
        m_BulletMark = Resources.Load<Texture2D>("Gun/BulletMarks/" + bulletMarkName);
        prefab_Effect = Resources.Load<GameObject>("Effects/Gun/" + effectName);
        effectParent = GameObject.Find("TempObject/" + effectParentName).GetComponent<Transform>();
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

        PlayEffect(hit);
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

    /// <summary>
    /// 播放子弹命中特效.
    /// </summary>
    private void PlayEffect(RaycastHit hit)
    {
        GameObject go;
        if (objectPool.IsEmpty())
        {
            go = GameObject.Instantiate<GameObject>(prefab_Effect, hit.point,
                Quaternion.LookRotation(hit.normal), effectParent);
        }
        else
        {
            go = objectPool.GetObject();

            Transform tempTransform = go.GetComponent<Transform>();
            tempTransform.position = hit.point;
            tempTransform.rotation = Quaternion.LookRotation(hit.normal);
        }

        StartCoroutine(DelayToPool(go, 0.5f));
    }

    /// <summary>
    /// 延时进入对象池.
    /// </summary>
    private IEnumerator DelayToPool(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);
        objectPool.AddObject(go);
    }
}
