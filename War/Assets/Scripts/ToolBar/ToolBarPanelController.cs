using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 工具栏模块C层.
/// </summary>
public class ToolBarPanelController : MonoBehaviour
{
    public static ToolBarPanelController Instance;

    private ToolBarPanelView m_ToolBarPanelView;

    private const int slotsNum = 8;                             // 工具栏物品槽数量.
    private List<GameObject> slotsList;                         // 工具栏物品槽集合.

    private GameObject currentActiveSlot;                       // 当前激活的物品槽.
    private GameObject currentWeapon;                           // 当前选中武器.

    private Dictionary<GameObject, GameObject> toolBarDic;      // 工具栏和实例武器对应.

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();
        CreateAllSlot();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_ToolBarPanelView = gameObject.GetComponent<ToolBarPanelView>();

        slotsList = new List<GameObject>(slotsNum);
        toolBarDic = new Dictionary<GameObject, GameObject>();
    }

    /// <summary>
    /// 创建全部工具栏物品槽.
    /// </summary>
    private void CreateAllSlot()
    {
        for (int i = 0; i < slotsNum; ++i)
        {
            GameObject slot = GameObject.Instantiate<GameObject>(m_ToolBarPanelView.Prefab_Slot,
                m_ToolBarPanelView.Grid_Transform);
            slot.GetComponent<ToolBarSlotController>().InitSlot(i);
            slotsList.Add(slot);
        }
    }

    /// <summary>
    /// 存储当前激活的物品槽.
    /// </summary>
    private void SaveActiveSlot(GameObject activeSlot)
    {
        if (currentActiveSlot != null && currentActiveSlot != activeSlot)
        {
            currentActiveSlot.GetComponent<ToolBarSlotController>().NormalSlot();
        }
        currentActiveSlot = activeSlot;
    }

    /// <summary>
    /// 通过按键存储激活的物品槽.
    /// </summary>
    public void SaveActiveSlotByKey(int keyIndex)
    {
        slotsList[keyIndex].GetComponent<ToolBarSlotController>().SlotClick();
        CallGunFactory();
    }

    /// <summary>
    /// 调用枪械工厂生成武器.
    /// </summary>
    private void CallGunFactory()
    {
        Transform tempTransform = currentActiveSlot.GetComponent<Transform>().Find("InventoryItem");
        if (tempTransform != null)
        {
            // 隐藏当前武器.
            if (currentWeapon != null)
            {
                currentWeapon.SetActive(false);
            }

            // 从字典查找实例.
            GameObject temp = null;
            toolBarDic.TryGetValue(tempTransform.gameObject, out temp);

            // 字典没有, 生成武器.
            if(temp == null)
            {
                temp = GunFactory.Instance.CreateGun(tempTransform.GetComponent<Image>().sprite.name, tempTransform.gameObject);
                toolBarDic.Add(tempTransform.gameObject, temp);
            }

            // 有武器, 直接取数据.
            else
            {
                if (currentActiveSlot.GetComponent<ToolBarSlotController>().IsActiveSlot)
                    temp.SetActive(true);
            }

            // 更新引用.
            currentWeapon = temp;
        }
    }
}
