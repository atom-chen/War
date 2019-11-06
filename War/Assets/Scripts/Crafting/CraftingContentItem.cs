using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单条合成内容数据实体类.
/// </summary>
public class CraftingContentItem
{
    private int itemId;                 // 合成内容编号.
    private string itemName;            // 合成内容.

    public int ItemId { get => itemId; set => itemId = value; }
    public string ItemName { get => itemName; set => itemName = value; }
}
