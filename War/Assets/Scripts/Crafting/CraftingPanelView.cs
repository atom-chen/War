﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成面板V层.
/// </summary>
public class CraftingPanelView : MonoBehaviour
{
    private Transform m_Transform;
    private Transform tabs_Transform;                   // 合成选项卡父物体.
    private Transform contents_Transform;               // 合成内容父物体.
    private Transform center_Transform;                 // 合成图谱槽父物体.

    private GameObject prefab_TabItem;                  // 合成选项卡预制体.
    private GameObject prefab_Content;                  // 合成内容预制体.
    private GameObject prefab_ContentItem;              // 单条合成内容预制体.
    private GameObject prefab_Slot;                     // 合成物品槽预制体.
    private GameObject prefab_InventoryItem;            // 最终合成物品.

    private Dictionary<string, Sprite> tabIconsDic;     // 合成选项卡字典.
    private Dictionary<string, Sprite> materialIconsDic;// 合成材料图标字典.

    public Transform M_Transform { get => m_Transform; }
    public Transform Tabs_Transform { get => tabs_Transform; }
    public Transform Contents_Transform { get => contents_Transform; }
    public Transform Center_Transform { get => center_Transform; }
    public GameObject Prefab_TabItem { get => prefab_TabItem; }
    public GameObject Prefab_Content { get => prefab_Content; }
    public GameObject Prefab_ContentItem { get => prefab_ContentItem; }
    public GameObject Prefab_Slot { get => prefab_Slot; }
    public GameObject Prefab_InventoryItem { get => prefab_InventoryItem; }

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
        tabs_Transform = m_Transform.Find("Left/Tabs");
        contents_Transform = m_Transform.Find("Left/Contents");
        center_Transform = m_Transform.Find("Center");

        prefab_TabItem = Resources.Load<GameObject>("Crafting/CraftingTabItem");
        prefab_Content = Resources.Load<GameObject>("Crafting/CraftingContent");
        prefab_ContentItem = Resources.Load<GameObject>("Crafting/CraftingContentItem");
        prefab_Slot = Resources.Load<GameObject>("Crafting/CraftingSlot");
        prefab_InventoryItem = Resources.Load<GameObject>("Inventory/InventoryItem");

        tabIconsDic = new Dictionary<string, Sprite>();
        materialIconsDic = new Dictionary<string, Sprite>();

        ResourceTools.LoadIconsAsset("Textures/Crafting/TabIcon", tabIconsDic);
        ResourceTools.LoadIconsAsset("Textures/Inventory/Material", materialIconsDic);
    }

    /// <summary>
    /// 通过名称获取选项卡图标.
    /// </summary>
    public Sprite GetTabIconByName(string spriteName)
    {
        return ResourceTools.GetIconByName(spriteName, tabIconsDic);
    }

    /// <summary>
    /// 通过名称获取合成材料图标.
    /// </summary>
    public Sprite GetMaterialIconByName(string spriteName)
    {
        return ResourceTools.GetIconByName(spriteName, materialIconsDic);
    }
}
