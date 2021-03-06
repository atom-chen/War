﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包模块C层.
/// </summary>
public class InventoryPanelController : MonoBehaviour, IUIPanelHideAndShow
{
    public static InventoryPanelController Instance;

    // 持有V和M.
    private InventoryPanelView m_InventoryPanelView;
    private InventoryPanelModel m_InventoryPanelModel;

    private const int slotsNum = 27;                    // 背包物品槽数量.
    private List<GameObject> slotsList;                 // 背包物品槽集合.

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();
        CreateAllSlots();
        CreateAllItems();
    }

    void OnDisable()
    {
        // 存档.
        if (m_InventoryPanelModel != null)
            m_InventoryPanelModel.ObjectToJson(slotsList);
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_InventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
        m_InventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();

        slotsList = new List<GameObject>(slotsNum);
    }

    /// <summary>
    /// 生成全部背包物品槽.
    /// </summary>
    private void CreateAllSlots()
    {
        for(int i = 0; i < slotsNum; ++i)
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
        List<InventoryItem> itemList = m_InventoryPanelModel.GetInentoryDataByName("InventoryJsonData.txt");
        for(int i = 0; i < itemList.Count; ++i)
        {
            GameObject item = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Item,
                slotsList[i].GetComponent<Transform>());
            item.GetComponent<InventoryItemController>().InitItem(itemList[i]);
        }
    }

    /// <summary>
    /// 添加材料到背包中去.
    /// </summary>
    public void AddItems(List<GameObject> materialsList)
    {
        int materialIndex = 0;

        for (int i = 0; i < slotsNum; ++i)
        {
            Transform tempTransform = slotsList[i].GetComponent<Transform>();
            if (tempTransform.Find("InventoryItem") == null && materialIndex < materialsList.Count)
            {
                materialsList[materialIndex].GetComponent<Transform>().SetParent(tempTransform);
                materialsList[materialIndex].GetComponent<InventoryItemController>().InInventory = true;
                materialIndex++;
            }
        }
    }

    /// <summary>
    /// 传递信息, 降低耦合度.
    /// </summary>
    public void SendDragMaterialsItem()
    {
        CraftingPanelController.Instance.DragMaterialsItem();
    }

    /// <summary>
    /// 爆出材料采集.
    /// </summary>
    public void CollectMaterials(string itemName)
    {
        for (int i = 0; i < slotsNum; ++i)
        {
            Transform tempTransform = slotsList[i].GetComponent<Transform>().Find("InventoryItem");
            if (tempTransform != null && tempTransform.GetComponent<Image>().sprite.name == itemName)
            {
                tempTransform.GetComponent<InventoryItemController>().ItemNum++;
            }
        }
    }

    public void UIPanelShow()
    {
        gameObject.SetActive(true);
    }

    public void UIPanelHide()
    {
        gameObject.SetActive(false);
    }
}
