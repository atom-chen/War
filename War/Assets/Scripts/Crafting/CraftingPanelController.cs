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

    private const int tabsNum = 5;                  // 合成选项卡数量.
    private List<GameObject> tabsList;              // 合成选项卡集合.
    private List<GameObject> contentsList;          // 合成内容集合.

    void Start()
    {
        FindAndLoadInit();
        CreateAllTabs();
        CreateAllContents();

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
    }

    /// <summary>
    /// 生成全部选项卡.
    /// </summary>
    private void CreateAllTabs()
    {
        for(int i = 0; i < tabsNum; ++i)
        {
            GameObject tab = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabItem,
                m_CraftingPanelView.Tabs_Transform);
            tab.GetComponent<CraftingTabItemController>().InitTabItem(i);
            tabsList.Add(tab);
        }
    }

    /// <summary>
    /// 生成全部内容区域.
    /// </summary>
    private void CreateAllContents()
    {
        for (int i = 0; i < tabsNum; ++i)
        {
            GameObject content = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content,
                m_CraftingPanelView.Contents_Transform);
            content.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem);
            contentsList.Add(content);
        }
    }

    /// <summary>
    /// 重置选项卡和正文区域.
    /// </summary>
    /// <param name="index">显示的选项卡和内容区域索引.</param>
    private void ResetTabsAndContents(int index = 0)
    {
        for(int i = 0; i < tabsNum; ++i)
        {
            tabsList[i].GetComponent<CraftingTabItemController>().NormalItem();
            contentsList[i].gameObject.SetActive(false);
        }
        tabsList[index].GetComponent<CraftingTabItemController>().ActiveItem();
        contentsList[index].gameObject.SetActive(true);
    }
}
