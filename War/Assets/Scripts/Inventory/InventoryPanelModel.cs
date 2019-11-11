using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 背包模块M层.
/// </summary>
public class InventoryPanelModel : MonoBehaviour
{
    /// <summary>
    /// 通过Json文件名获取背包数据.
    /// </summary>
    /// <param name="fileName">Json文件名.</param>
    /// <returns>背包数据集合.</returns>
    public List<InventoryItem> GetInentoryDataByName(string fileName)
    {
        return JsonTools.GetInentoryDataByName<InventoryItem>(fileName);
    }
}
