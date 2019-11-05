using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包模块V层.
/// </summary>
public class InventoryPanelView : MonoBehaviour
{
    private Transform m_Transform;
    private Transform grid_Transform;               // 背包物品槽的父物体.

    private GameObject prefab_Slot;                 // 背包物品槽预制体.
    private GameObject prefab_Item;                 // 背包物品预制体.

    public Transform Grid_Transform { get => grid_Transform; set => grid_Transform = value; }
    public GameObject Prefab_Slot { get => prefab_Slot; set => prefab_Slot = value; }
    public GameObject Prefab_Item { get => prefab_Item; set => prefab_Item = value; }

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
        grid_Transform = m_Transform.Find("Background/Grid");

        Prefab_Slot = Resources.Load<GameObject>("Inventory/InventorySlot");
        Prefab_Item = Resources.Load<GameObject>("Inventory/InventoryItem");
    }
}
