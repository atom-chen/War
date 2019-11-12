using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包模块C层.
/// </summary>
public class InventoryPanelController : MonoBehaviour
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
        List<InventoryItem> itemList = m_InventoryPanelModel.GetInentoryDataByName("InventoryJsonData");
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
        int index = 0;

        for (int i = 0; i < slotsNum; ++i)
        {
            Transform tempTransform = slotsList[i].GetComponent<Transform>();
            if (tempTransform.Find("InventoryItem") == null && index < materialsList.Count)
            {
                materialsList[index].GetComponent<Transform>().SetParent(tempTransform);
                materialsList[index].GetComponent<InventoryItemController>().InInventory = true;
                index++;
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
}
