﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背包物品数据实体类.
/// </summary>
public class InventoryItem 
{
    private int itemId;                     // 背包物品编号.
    private string itemName;                // 背包物品名称.
    private int itemNum;                    // 背包物品数量.
    private int itemBar;                    // 物品耐久UI.

    public int ItemId 
    { 
        get => itemId; 
        set => itemId = value; 
    }

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
    public int ItemBar 
    { 
        get => itemBar; 
        set => itemBar = value; 
    }

    public InventoryItem() { }
    public InventoryItem(int itemId, string itemName, int itemNum, int itemBar)
    {
        this.itemId = itemId;
        this.itemName = itemName;
        this.itemNum = itemNum;
        this.itemBar = itemBar;
    }
}
