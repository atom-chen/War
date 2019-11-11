using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成面板C层.
/// </summary>
public class CraftingPanelController : MonoBehaviour
{
    // 持有V和M.
    private CraftingPanelView m_CraftingPanelView;
    private CraftingPanelModel m_CraftingPanelModel;

    private const int tabsNum = 2;                  // 合成选项卡数量.
    private List<GameObject> tabsList;              // 合成选项卡集合.
    private List<GameObject> contentsList;          // 合成内容集合.

    private const int slotsNum = 25;                // 合成图谱槽数量.
    private List<GameObject> slotsList;             // 合成图谱槽集合.

    private int currentTabIndex = -1;               // 当前选中激活的选项卡.

    private CraftingController m_CraftingControler; // 右侧合成功能区控制器.

    void Start()
    {
        FindAndLoadInit();
        CreateAllTabs();
        CreateAllContents();
        CreateAllSlots();

        ResetTabsAndContents();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
        m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();

        tabsList = new List<GameObject>(tabsNum);
        contentsList = new List<GameObject>(tabsNum);
        slotsList = new List<GameObject>(slotsNum);

        m_CraftingControler = m_CraftingPanelView.M_Transform.Find("Right").GetComponent<CraftingController>();
    }

    /// <summary>
    /// 生成全部选项卡.
    /// </summary>
    private void CreateAllTabs()
    {
        for (int i = 0; i < tabsNum; ++i)
        {
            GameObject tab = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabItem,
                m_CraftingPanelView.Tabs_Transform);

            Sprite sprite = m_CraftingPanelView.GetTabIconByName(m_CraftingPanelModel.GetTabsIconsName()[i]);
            tab.GetComponent<CraftingTabItemController>().InitTabItem(i, sprite);
            tabsList.Add(tab);
        }
    }

    /// <summary>
    /// 生成全部内容区域.
    /// </summary>
    private void CreateAllContents()
    {
        List<List<CraftingContentItem>> tempList = m_CraftingPanelModel.GetCraftingContentByName("CraftingContentsJsonData");

        for (int i = 0; i < tabsNum; ++i)
        {
            GameObject content = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content,
                m_CraftingPanelView.Contents_Transform);
            content.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem, tempList[i]);
            contentsList.Add(content);
        }
    }

    /// <summary>
    /// 重置选项卡和正文区域.
    /// </summary>
    /// <param name="index">显示的选项卡和内容区域索引.</param>
    private void ResetTabsAndContents(int index = 0)
    {
        if (currentTabIndex == index)
            return;

        for (int i = 0; i < tabsNum; ++i)
        {
            tabsList[i].GetComponent<CraftingTabItemController>().NormalItem();
            contentsList[i].gameObject.SetActive(false);
        }
        tabsList[index].GetComponent<CraftingTabItemController>().ActiveItem();
        contentsList[index].gameObject.SetActive(true);

        currentTabIndex = index;
    }

    /// <summary>
    /// 生成全部合成图谱槽.
    /// </summary>
    private void CreateAllSlots()
    {
        for (int i = 0; i < slotsNum; ++i)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Slot,
                m_CraftingPanelView.Center_Transform);
            slot.name = "Slot_" + i;
            slotsList.Add(slot);
        }
    }

    /// <summary>
    /// 生成图谱内容.
    /// </summary>
    private void CreateMapContents(int mapId)
    {
        // 重置图谱.
        ResetMap();

        // 合成材料生成.
        CraftingMapItem mapItem = m_CraftingPanelModel.GetMapDataById(mapId);
        if (mapItem == null) 
            return;

        for (int i = 0; i < slotsNum; ++i)
        {
            if (mapItem.MapContents[i] != "0")
            {
                Sprite sprite = m_CraftingPanelView.GetMaterialIconByName(mapItem.MapContents[i]);
                slotsList[i].GetComponent<CraftingSlotController>().InitSlot(sprite);
            }
        }

        // 最终合成物品展现.
        m_CraftingControler.Init(mapItem.MapName);
    }

    /// <summary>
    /// 重置合成图谱槽.
    /// </summary>
    private void ResetMap()
    {
        for (int i = 0; i < slotsNum; ++i)
        {
            slotsList[i].GetComponent<CraftingSlotController>().ResetSlot();
        }
    }
}
