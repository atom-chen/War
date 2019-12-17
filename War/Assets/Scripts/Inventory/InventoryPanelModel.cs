using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    /// <summary>
    /// 背包数据存档.
    /// </summary>
    public void ObjectToJson(List<GameObject> slotsList)
    {
        List<InventoryItem> tempList = new List<InventoryItem>(slotsList.Count);

        // 遍历物品槽数据.
        for (int i = 0; i < slotsList.Count; ++i)
        {
            Transform tempTransform = slotsList[i].GetComponent<Transform>().Find("InventoryItem");

            if (tempTransform != null)
            {
                InventoryItemController iic = tempTransform.GetComponent<InventoryItemController>();
                InventoryItem item = new InventoryItem(iic.ItemId, iic.GetComponent<Image>().sprite.name, 
                    iic.ItemNum, iic.ItemBar);
                tempList.Add(item);
            }  
        }

        // 转换为Json数据.
        string jsonStr = JsonMapper.ToJson(tempList);
        string jsonPath = Application.dataPath + "/Resources/JsonData/InventoryJsonData.txt";

        // 更新Json文件.
        File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(jsonStr);
        sw.Close();
    }
}
