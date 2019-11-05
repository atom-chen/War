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
        List<InventoryItem> tempList = new List<InventoryItem>();

        // 解析Json数据.
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for(int i = 0; i < jsonData.Count; ++i)
        {
            tempList.Add(JsonMapper.ToObject<InventoryItem>(jsonData[i].ToJson()));
        }

        return tempList;
    }
}
