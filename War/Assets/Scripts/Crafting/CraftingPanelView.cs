using System.Collections;
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

    private GameObject prefab_TabItem;                  // 合成选项卡预制体.
    private GameObject prefab_Content;                  // 合成内容预制体.
    private GameObject prefab_ContentItem;              // 单条合成内容预制体.
    private GameObject prefab_Slot;                     // 合成物品槽预制体.

    public Transform Tabs_Transform { get => tabs_Transform; set => tabs_Transform = value; }
    public Transform Contents_Transform { get => contents_Transform; set => contents_Transform = value; }
    public GameObject Prefab_TabItem { get => prefab_TabItem; set => prefab_TabItem = value; }
    public GameObject Prefab_Content { get => prefab_Content; set => prefab_Content = value; }
    public GameObject Prefab_ContentItem { get => prefab_ContentItem; set => prefab_ContentItem = value; }

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

        prefab_TabItem = Resources.Load<GameObject>("Crafting/CraftingTabItem");
        prefab_Content = Resources.Load<GameObject>("Crafting/CraftingContent");
        prefab_ContentItem = Resources.Load<GameObject>("Crafting/CraftingContentItem");
        prefab_Slot = Resources.Load<GameObject>("Crafting/CraftingSlot");
    }
}
