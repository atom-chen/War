using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包模块C层.
/// </summary>
public class InventoryPanelController : MonoBehaviour
{
    // 持有V和M.
    private InventoryPanelView m_InventoryPanelView;
    private InventoryPanelModel m_InventoryPanelModel;

    private const int slotsnum = 27;                    // 背包物品槽数量.
    private List<GameObject> slotsList;                 // 背包物品槽集合.

    void Start()
    {
        FindAndLoadInit();
        CreateAllSlots();
        CreateAllItems();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_InventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
        m_InventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();

        slotsList = new List<GameObject>(slotsnum);
    }

    /// <summary>
    /// 生成全部背包物品槽.
    /// </summary>
    private void CreateAllSlots()
    {
        for(int i = 0; i < slotsnum; ++i)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Slot, 
                m_InventoryPanelView.Grid_Transform);
            slot.name = "SLots_" + i;
            slotsList.Add(slot);
        }
    }

    /// <summary>
    /// 生成全部背包物品.
    /// </summary>
    private void CreateAllItems()
    {
        List<InventoryItem> itemList = m_InventoryPanelModel.GetInentoryDataByName("InventoryJsonData");
        for(int i = 0; i < itemList.Count; ++i)
        {
            GameObject item = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Item,
                slotsList[i].GetComponent<Transform>());
            item.GetComponent<InventoryItemController>().InitItem(i, itemList[i]);
        }
    }
}
