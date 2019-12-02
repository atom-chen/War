using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造模块C层.
/// </summary>
public class BuildPanelController : MonoBehaviour
{
    private Transform m_Transform;
    private Transform wheelBG_Transform;                // 环形UI背景.

    private GameObject prefab_CategoryItem;             // 建造类别UI预制体.

    private const int categoryNum = 9;                  // 建造类别数量.

    private List<Sprite> categoryIconsList;             // 建造类别图标.

    void Start()
    {
        FindAndLoadInit();
        LoadCategoryIcons();

        CreateAllCategory();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        wheelBG_Transform = m_Transform.Find("Wheel_BG");

        prefab_CategoryItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/CategoryItem");

        categoryIconsList = new List<Sprite>(categoryNum);
    }

    /// <summary>
    /// 加载建造类别图标.
    /// </summary>
    private void LoadCategoryIcons()
    {
        categoryIconsList.Add(null);
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Question Mark"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Roof_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Stairs_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Window_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Door_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Wall_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Floor_Category"));
        categoryIconsList.Add(Resources.Load<Sprite>("BuildModule/UI/Category/Foundation_Category"));
    }

    /// <summary>
    /// 生成全部类别UI.
    /// </summary>
    private void CreateAllCategory()
    {
        for (int i = 0; i < categoryNum; ++i)
        {
            GameObject go = GameObject.Instantiate<GameObject>(prefab_CategoryItem, wheelBG_Transform);
            go.GetComponent<CategoryItemController>().InitItem(i, categoryIconsList[i]);
        }
    }
}
