using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 工具栏模块V层.
/// </summary>
public class ToolBarPanelView : MonoBehaviour
{
    private Transform m_Transform;
    private Transform grid_Transform;                   // 工具栏物品槽父物体.

    private GameObject prefab_Slot;                     // 工具栏物品槽预制体.

    public Transform Grid_Transform { get => grid_Transform; }
    public GameObject Prefab_Slot { get => prefab_Slot; }

    void Awake()
    {
        FindAndLoadInit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        grid_Transform = m_Transform.Find("Grid");

        prefab_Slot = Resources.Load<GameObject>("ToolBar/ToolBarSlot");
    }
}
