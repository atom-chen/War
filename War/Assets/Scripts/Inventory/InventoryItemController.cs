using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 单个背包物品控制器.
/// </summary>
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform m_RectTransform;
    private CanvasGroup m_CanvasGroup;          // 用于射线检测的取消和恢复.
    private Image m_Image;                      // 物品展示图片.
    private Text m_Text;                        // 物品数量文字UI.
    private Image m_Bar;                        // 物品耐久UI.

    private Transform dragParent;               // 拖拽中背包物品的父物体.
    private Transform originParent;             // 背包物品的原始父物体.

    private int itemId = -1;                    // 背包物品编号.
    private int itemNum = -1;                   // 背包物品数量.
    private int itemBar = 0;                    // 物品显示耐久值. 0:不显示; 1:显示.

    private bool isDrag = false;                // 背包物品正在被拖拽.
    private bool canBreak = true;               // 背包物品可以被拆分, 防止拖拽中多次拆分.
    private bool inInventory = true;            // 是否在背包.

    public int ItemId { get => itemId; set => itemId = value; }
    public int ItemNum 
    { 
        get => itemNum; 
        set
        {
            itemNum = value;
            m_Text.text = itemNum.ToString();
        }
    }
    public bool InInventory
    {
        get => inInventory;
        set
        {
            inInventory = value;
            m_RectTransform.localPosition = Vector3.zero;
            if (inInventory == true)
                ResetSpriteSize(m_RectTransform, 85, 85);
        }
    }

    void Awake()
    {
        FindAndLoadInit();
    }

    void Update()
    {
        // 按下鼠标右键尝试拆分.
        if (Input.GetMouseButtonDown(1) && isDrag && canBreak)
        {
            BreakMaterials();
            canBreak = false;
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_RectTransform = gameObject.GetComponent<RectTransform>();
        m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
        m_Image = gameObject.GetComponent<Image>();
        m_Text = m_RectTransform.Find("ItemCount").GetComponent<Text>();
        m_Bar = m_RectTransform.Find("ItemBar").GetComponent<Image>();

        dragParent = GameObject.Find("Canvas").GetComponent<Transform>();
    }

    /// <summary>
    /// 外部调用初始化背包物品基本信息.
    /// </summary>
    public void InitItem(InventoryItem item)
    {
        this.itemId = item.ItemId;
        this.itemNum = item.ItemNum;
        this.itemBar = item.ItemBar;

        gameObject.name = "InventoryItem";
        m_Image.sprite = Resources.Load<Sprite>("Textures/Inventory/Item/" + item.ItemName);
        m_Text.text = item.ItemNum.ToString();

        BarOrNum();
    }

    /// <summary>
    /// 显示耐久或者数量.
    /// </summary>
    private void BarOrNum()
    {
        if (itemBar == 1)
        {
            m_Bar.gameObject.SetActive(true);
            m_Text.gameObject.SetActive(false);
        }
        else
        {
            m_Bar.gameObject.SetActive(false);
            m_Text.gameObject.SetActive(true);
        }
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        originParent = m_RectTransform.parent;              // 物品有可能回到原始位置.
        m_RectTransform.SetParent(dragParent);              // 防止UI遮挡.
        m_CanvasGroup.blocksRaycasts = false;               // 取消射线检测.
        isDrag = true;                                      // 拖拽中, 可以拆分.
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // 屏幕坐标转世界坐标.
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position,
            eventData.enterEventCamera, out pos);
        m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        GameObject target = eventData.pointerEnter;

        ItemDrag(target);

        // 通用重置代码.
        m_RectTransform.localPosition = Vector3.zero;
        m_CanvasGroup.blocksRaycasts = true;                    // 恢复射线检测.
        isDrag = false;
        canBreak = true;                                        // 完成拖拽, 可以再次拆分.
    }

    /// <summary>
    /// 物品拖拽逻辑实现.
    /// </summary>
    private void ItemDrag(GameObject target)
    {
        // 安全判断, 确保和UI进行交互.
        if (target != null)
        {
            Transform targetTransform = target.GetComponent<Transform>();

            // 拖拽到背包物品槽.
            if (target.tag == "InventorySlot")
            {
                m_RectTransform.SetParent(targetTransform);
                InInventory = true;
            }

            // 拖拽到了其他背包物品上.
            else if (target.tag == "InventoryItem")
            {
                InventoryItemController iic = target.GetComponent<InventoryItemController>();

                // 两个物品都在背包.
                if (InInventory && iic.InInventory)
                {
                    // 两个物品相同, 材料合并.
                    if (ItemId == iic.ItemId)
                    {
                        MergeMaterials(iic);
                    }
                    // 交换位置.
                    else
                    {
                        m_RectTransform.SetParent(targetTransform.parent);
                        targetTransform.SetParent(originParent);
                        targetTransform.localPosition = Vector3.zero;
                    }
                }

                // 物品从合成面板拖回背包, ID相同.
                else if (InInventory == false && iic.InInventory && iic.ItemId == itemId)
                {
                    MergeMaterials(iic);
                }

                // 回归原始位置.
                else
                {
                    BackToOriginPlace();
                }
            }

            // 拖拽到了合成图谱槽.
            else if (target.tag == "CraftingSlot")
            {
                // 和参考图片匹配.
                if (target.GetComponent<CraftingSlotController>().ItemId == itemId)
                {
                    m_RectTransform.SetParent(targetTransform);
                    ResetSpriteSize(m_RectTransform, 70, 62);
                    InInventory = false;

                    InventoryPanelController.Instance.SendDragMaterialsItem();
                }
                // 和参考图片不匹配, 回归原始位置.
                else
                {
                    BackToOriginPlace();
                }
            }

            // 拖拽到了非交互UI, 回归原始位置.
            else if (target.tag != "InventorySlot" && target.tag != "CraftingSlot" && target.tag != "InventoryItem")
            {
                BackToOriginPlace();
            }
        }

        // 非UI区域, 回归原始位置.
        else
        {
            BackToOriginPlace();
        }
    }

    /// <summary>
    /// 回归原始位置逻辑.
    /// </summary>
    private void BackToOriginPlace()
    {
        Transform tempTransform = originParent.Find("InventoryItem");
        if (tempTransform != null)
        {
            MergeMaterials(tempTransform.GetComponent<InventoryItemController>());
        }

        m_RectTransform.SetParent(originParent);
    }

    /// <summary>
    /// 重置图片尺寸.
    /// </summary>
    private void ResetSpriteSize(RectTransform rectTransform, float width, float height)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    /// <summary>
    /// 合成材料拆分.
    /// </summary>
    private void BreakMaterials()
    {
        // 背包物品数量大于1才可以拆分.
        if (itemNum < 1)
            return;

        // 复制一份自身A为B.
        GameObject copy = GameObject.Instantiate<GameObject>(gameObject, originParent);
        RectTransform copyTransform = copy.GetComponent<RectTransform>();
        copyTransform.localPosition = Vector3.zero;
        copyTransform.localScale = Vector3.one;

        // 数量拆分.
        int copyNum = itemNum / 2;
        InventoryItemController iic = copy.GetComponent<InventoryItemController>();
        iic.ItemNum = copyNum;
        ItemNum -= copyNum;

        // 重置属性.
        copy.name = gameObject.name;
        iic.ItemId = itemId;
        copy.GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (copyTransform.parent.tag != "InventorySlot")
            iic.InInventory = false;
    }

    /// <summary>
    /// 合成材料合并.
    /// </summary>
    private void MergeMaterials(InventoryItemController itemController)
    {
        itemController.ItemNum += ItemNum;
        GameObject.Destroy(gameObject);
    }
}
