using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 合成面板M层.
/// </summary>
public class CraftingPanelModel : MonoBehaviour
{
    /// <summary>
    /// 获取合成选项卡图标的名称.
    /// </summary>
    public string[] GetTabsIconsName()
    {
        return new string[] { "Icon_House", "Icon_Weapon" };
    }

    /// <summary>
    /// 通过Json文件名称获取合成内容.
    /// </summary>
    public List<List<CraftingContentItem>> GetCraftingContentByName(string fileName)
    {
        List<List<CraftingContentItem>> tempList = new List<List<CraftingContentItem>>();

        // Json文件解析.
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for(int i = 0; i < jsonData.Count; ++i)
        {
            JsonData jd = jsonData[i]["Type"];
            List<CraftingContentItem> temp = new List<CraftingContentItem>();

            for (int j = 0; j < jd.Count; ++j)
            {
                temp.Add(JsonMapper.ToObject<CraftingContentItem>(jd[j].ToJson()));
            }

            tempList.Add(temp);
        }

        return tempList;
    }
}
