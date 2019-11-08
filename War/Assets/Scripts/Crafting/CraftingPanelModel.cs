using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 合成面板M层.
/// </summary>
public class CraftingPanelModel : MonoBehaviour
{
    Dictionary<int, CraftingMapItem> mapItemDic;                // 合成图谱数据字典.

    void Awake()
    {
        LoadMapDataByName(out mapItemDic, "CraftingMapJsonData");
    }

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
        for (int i = 0; i < jsonData.Count; ++i)
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

    /// <summary>
    /// 通过Json文件名获取合成图谱学习.
    /// </summary>
    /// <param name="fileName">Json文件名</param>
    private void LoadMapDataByName(out Dictionary<int, CraftingMapItem> mapItemDic, string fileName)
    {
        mapItemDic = new Dictionary<int, CraftingMapItem>();

        // Json解析.
        string jsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; ++i)
        {
            int mapId = int.Parse(jsonData[i]["MapId"].ToString());
            string[] mapContents = jsonData[i]["MapContents"].ToString().Split(',');
            string mapName = jsonData[i]["MapName"].ToString();

            mapItemDic.Add(mapId, new CraftingMapItem(mapId, mapContents, mapName));
        }
    }

    /// <summary>
    /// 通过图谱编号获取图谱数据.
    /// </summary>
    public CraftingMapItem GetMapDataById(int mapId)
    {
        CraftingMapItem temp = null;
        mapItemDic.TryGetValue(mapId, out temp);
        return temp;
    }
}
