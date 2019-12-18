using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// Json数据处理工具类.
/// </summary>
public sealed class JsonTools
{
    /// <summary>
    /// 通过Json文件名加载数据, 并返回对象集合.
    /// </summary>
    public static List<T> GetInentoryDataByName<T>(string fileName)
    {
        List<T> tempList = new List<T>();

        // 解析Json数据.
        string jsonStr = File.ReadAllText(Application.dataPath + "/JsonData/" + fileName);
        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; ++i)
        {
            tempList.Add(JsonMapper.ToObject<T>(jsonData[i].ToJson()));
        }

        return tempList;
    }
}
