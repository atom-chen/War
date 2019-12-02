using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造模块C层.
/// </summary>
public class BuildPanelController : MonoBehaviour
{
    private Transform m_Transform;
    private Transform wheelBG_Transform;                // 环形UI背景.

    private GameObject prefab_CategoryItem;             // 建造类别UI预制体.

    private const int categoryNum = 9;                  // 建造类别数量.

    void Start()
    {
        FindAndLoadInit();
        CreateAllCategory();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        wheelBG_Transform = m_Transform.Find("Wheel_BG");

        prefab_CategoryItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/CategoryItem");
    }

    /// <summary>
    /// 生成全部类别UI.
    /// </summary>
    private void CreateAllCategory()
    {
        for (int i = 0; i < categoryNum; ++i)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab_CategoryItem, wheelBG_Transform);
            Transform tempTransform = go.GetComponent<Transform>();
            tempTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, i * 40));
            tempTransform.Find("Icon").rotation = Quaternion.identity;

            go.name = "CategoryItem_" + i;
        }
    }
}
