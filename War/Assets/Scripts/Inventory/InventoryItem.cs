using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品数据实体类.
/// </summary>
public class InventoryItem 
{
    private string itemName;                // 背包物品名称.
    private int itemNum;                    // 背包物品数量.

    public string ItemName 
    { 
        get => itemName; 
        set => itemName = value; 
    }
    public int ItemNum 
    { 
        get => itemNum; 
        set => itemNum = value; 
    }

    public override string ToString()
    {
        return string.Format("物品名称: {0}, 物品数量: {1}", itemName, itemNum);
    }
}
