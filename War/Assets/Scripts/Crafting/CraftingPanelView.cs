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
    private Transform center_Transform;                 // 合成图谱槽父物体.

    private GameObject prefab_TabItem;                  // 合成选项卡预制体.
    private GameObject prefab_Content;                  // 合成内容预制体.
    private GameObject prefab_ContentItem;              // 单条合成内容预制体.
    private GameObject prefab_Slot;                     // 合成物品槽预制体.

    private Dictionary<string, Sprite> tabIconsDic;     // 合成选项卡字典.
    private Dictionary<string, Sprite> materialIconsDic;// 合成材料图标字典.

    public Transform Tabs_Transform { get => tabs_Transform; set => tabs_Transform = value; }
    public Transform Contents_Transform { get => contents_Transform; set => contents_Transform = value; }
    public Transform Center_Transform { get => center_Transform; set => center_Transform = value; }
    public GameObject Prefab_TabItem { get => prefab_TabItem; set => prefab_TabItem = value; }
    public GameObject Prefab_Content { get => prefab_Content; set => prefab_Content = value; }
    public GameObject Prefab_ContentItem { get => prefab_ContentItem; set => prefab_ContentItem = value; }
    public GameObject Prefab_Slot { get => prefab_Slot; set => prefab_Slot = value; }

    void Awake()
    {
        FindAndLoadInit();
        LoadTabIcons();
        LoadMaterialIcons();
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

        tabIconsDic = new Dictionary<string, Sprite>();
        materialIconsDic = new Dictionary<string, Sprite>();
    }

    /// <summary>
    /// 加载选项卡资源图标.
    /// </summary>
    private void LoadTabIcons()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Crafting/TabIcon");
        for (int i = 0; i < sprites.Length; ++i)
        {
            tabIconsDic.Add(sprites[i].name, sprites[i]);
        }
    }

    /// <summary>
    /// 加载合成材料.资源图标.
    /// </summary>
    private void LoadMaterialIcons()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Textures/Inventory/Material");
        for (int i = 0; i < sprites.Length; ++i)
        {
            materialIconsDic.Add(sprites[i].name, sprites[i]);
        }
    }

    /// <summary>
    /// 通过名称获取选项卡图标.
    /// </summary>
    public Sprite GetTabIconByName(string spriteName)
    {
        Sprite temp = null;
        tabIconsDic.TryGetValue(spriteName, out temp);
        return temp;
    }

    /// <summary>
    /// 通过名称获取合成材料图标.
    /// </summary>
    public Sprite GetMaterialIconByName(string spriteName)
    {
        Sprite temp = null;
        materialIconsDic.TryGetValue(spriteName, out temp);
        return temp;
    }
}
