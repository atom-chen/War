using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成选项卡正文区域控制器.
/// </summary>
public class CraftingContentController : MonoBehaviour
{
    private Transform m_Transform;

    private int index = -1;                 // 选项卡内容区域索引.

    // 当前选中激活的内容.
    private CraftingContentItemController currentActiveItem;

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
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitContent(int index, GameObject prefab)
    {
        this.index = index;
        gameObject.name = "Content_" + index;

        CreateAllItems(index, prefab);
    }

    /// <summary>
    /// 生成全部内容.
    /// </summary>
    private void CreateAllItems(int count, GameObject prefab)
    {
        for(int i = 0; i < count; ++i)
        {
            GameObject item = GameObject.Instantiate<GameObject>(prefab, m_Transform);
            item.GetComponent<CraftingContentItemController>().InitItem(i);
        }
    }

    /// <summary>
    /// 重置全部内容.
    /// </summary>
    private void ResetAllItems(CraftingContentItemController itemController)
    {
        if(currentActiveItem != null)
        {
            currentActiveItem.NormalItem();            
        }

        currentActiveItem = itemController;
        currentActiveItem.ActiveItem();
    }
}
