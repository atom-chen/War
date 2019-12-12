using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// 讲建造数据写入Json文件.
/// </summary>
public class BuildModelsJson : MonoBehaviour
{
    private Transform m_Transform;

    private List<BuildItem> modelsList;                 // 建造物体的数据信息.
    private List<BuildItem> jsonList;                   // 读取Json文件获取的建造物体信息.

    private string jsonPath;                            // Json文本文件的路径地址.

    private GameObject prefab_Model;                    // 建造物品.

    void Start()
    {
        FindAndLoadInit();

        // 开始游戏就读取存档.
        JsonToObject();
    }

    void OnDisable()
    {
        // 存档.
        ObjectToJson();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        modelsList = new List<BuildItem>();
        jsonList = new List<BuildItem>();

        jsonPath = Application.dataPath + "/Resources/JsonData/ModelsJson.txt";
    }

    /// <summary>
    /// 对象转换为Json数据.
    /// </summary>
    private void ObjectToJson()
    {
        modelsList.Clear();

        for (int i = 0; i < m_Transform.childCount; ++i)
        {
            Vector3 pos = m_Transform.GetChild(i).position;
            Quaternion rot = m_Transform.GetChild(i).rotation;

            BuildItem item = new BuildItem(
                m_Transform.GetChild(i).gameObject.name,      // 名称.
                Math.Round(pos.x, 2).ToString(),            // 位置.
                Math.Round(pos.y, 2).ToString(),
                Math.Round(pos.z, 2).ToString(),
                Math.Round(rot.x, 2).ToString(),            // 旋转.
                Math.Round(rot.y, 2).ToString(),
                Math.Round(rot.z, 2).ToString(),
                Math.Round(rot.w, 2).ToString());

            modelsList.Add(item);
        }

        string str = JsonMapper.ToJson(modelsList);
        File.Delete(jsonPath);
        StreamWriter sw = new StreamWriter(jsonPath);
        sw.Write(str);
        sw.Close();
    }

    /// <summary>
    /// JSON转换为多个对象.
    /// </summary>
    private void JsonToObject()
    {
        string jsonStr = File.ReadAllText(jsonPath);

        JsonData jsonData = JsonMapper.ToObject(jsonStr);
        for (int i = 0; i < jsonData.Count; ++i)
        {
            BuildItem item = JsonMapper.ToObject<BuildItem>(jsonData[i].ToJson());
            jsonList.Add(item);
        }

        for (int i = 0; i < jsonList.Count; ++i)
        {
            Vector3 pos = new Vector3(float.Parse(jsonList[i].PosX), float.Parse(jsonList[i].PosY), float.Parse(jsonList[i].PosZ));
            Quaternion rot = new Quaternion(float.Parse(jsonList[i].RotX), float.Parse(jsonList[i].RotY), float.Parse(jsonList[i].RotZ), float.Parse(jsonList[i].RotW));

            prefab_Model = Resources.Load<GameObject>("BuildModule/BuildModels/" + jsonList[i].Name);
            GameObject buildModel = GameObject.Instantiate<GameObject>(prefab_Model, pos, rot, m_Transform);

            // 初始化逻辑.
            buildModel.name = jsonList[i].Name;
            buildModel.layer = LayerMask.NameToLayer("BuildModelEnd");
            MaterialModelBase mmb = buildModel.GetComponent<MaterialModelBase>();
            mmb.NormalModel();            
            GameObject.Destroy(mmb);
        }
    }
}
