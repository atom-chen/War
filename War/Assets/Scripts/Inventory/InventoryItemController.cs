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

    private Transform dragParent;               // 拖拽中背包物品的父物体.
    private Transform originParent;             // 背包物品的原始父物体.

    private int itemId = -1;                    // 背包物品编号.
    private int itemNum = -1;                   // 背包物品数量.

    void Awake()
    {
        FindAndLoadInit();
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

        dragParent = GameObject.Find("Canvas").GetComponent<Transform>();
    }

    /// <summary>
    /// 外部调用初始化背包物品基本信息.
    /// </summary>
    public void InitItem(int index, InventoryItem item)
    {
        this.itemId = item.ItemId;
        this.itemNum = item.ItemNum;

        gameObject.name = "Item_" + index;
        m_Image.sprite = Resources.Load<Sprite>("Textures/Inventory/Item/" + item.ItemName);
        m_Text.text = item.ItemNum.ToString();
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        originParent = m_RectTransform.parent;              // 物品有可能回到原始位置.
        m_RectTransform.SetParent(dragParent);              // 防止UI遮挡.
        m_CanvasGroup.blocksRaycasts = false;               // 取消射线检测.
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

        // 安全判断, 确保和UI进行交互.
        if (target != null)
        {
            Transform targetTransform = target.GetComponent<Transform>();

            // 拖拽到背包物品槽.
            if (target.tag == "InventorySlot")
            {
                m_RectTransform.SetParent(targetTransform);
                ResetSpriteSize(m_RectTransform, 85, 85);
            }

            // 拖拽到了其他背包物品上.
            else if (target.tag == "InventoryItem")
            {
                // 物品交换.
                m_RectTransform.SetParent(targetTransform.parent);
                targetTransform.SetParent(originParent);
                targetTransform.localPosition = Vector3.zero;
            }

            // 拖拽到了合成图谱槽.
            else if (target.tag == "CraftingSlot")
            {
                // 和参考图片匹配.
                if (target.GetComponent<CraftingSlotController>().ItemId == itemId)
                {       
                    m_RectTransform.SetParent(targetTransform);
                    ResetSpriteSize(m_RectTransform, 70, 62);
                }
                // 和参考图片不匹配, 回归原始位置.
                else
                {
                    m_RectTransform.SetParent(originParent);
                }
            }

            // 拖拽到了非交互UI, 回归原始位置.
            else if (target.tag != "InventorySlot" && target.tag != "CraftingSlot" && target.tag != "InventoryItem")
            {
                m_RectTransform.SetParent(originParent);
            }
        }

        // 非UI区域, 回归原始位置.
        else
        {            
            m_RectTransform.SetParent(originParent);
        }

        // 通用重置代码.
        m_RectTransform.localPosition = Vector3.zero;
        m_CanvasGroup.blocksRaycasts = true;                    // 恢复射线检测.
    }

    /// <summary>
    /// 重置图片尺寸.
    /// </summary>
    private void ResetSpriteSize(RectTransform rectTransform, float width, float height)
    {
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
}
