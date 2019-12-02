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
    private Transform wheelBG_Transform;                    // 环形UI背景.
    private Text m_CategoryNameText;                        // 建造类别名称.

    private GameObject prefab_CategoryItem;                 // 建造类别UI预制体.

    private const int categoryNum = 9;                      // 建造类别数量.
    private string[] categoryNames;                         // 建造类别名称.
    private List<Sprite> categoryIconsList;                 // 建造类别图标.
    private List<CategoryItemController> categoryItemList;  // 建造类别UI集合.

    private bool isUIShow = false;                          // 建造面板是否显示.
    private float scrollNum = 90000.0f;                     // 用于记录滚轮的数值.
    private int categoryIndex = 0;                          // 建造类别索引.

    private CategoryItemController currentItem;             // 当前选中的建造类别.
    private CategoryItemController targetItem;              // 目标选中的建造类别.

    void Start()
    {
        FindAndLoadInit();
        LoadCategoryIcons();

        CreateAllCategory();

        ShowOrHide();
    }

    void Update()
    {
        // 临时测试, 按下鼠标右键打卡或关闭建造.
        if (Input.GetMouseButtonDown(1))
        {
            ShowOrHide();
        }

        // 鼠标滚轮切换逻辑.
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (isUIShow && scrollValue != 0)
        {
            // 滚轮切换建造类别.
            scrollNum += scrollValue * 3;
            categoryIndex = Mathf.Abs((int)scrollNum % categoryNum);

            targetItem = categoryItemList[categoryIndex];
            if (currentItem != targetItem)
            {
                currentItem.NormalItem();
                targetItem.ActiveItem();
                m_CategoryNameText.text = categoryNames[categoryIndex];

                currentItem = targetItem;
            }
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        wheelBG_Transform = m_Transform.Find("Wheel_BG");
        m_CategoryNameText = wheelBG_Transform.Find("CategroyName").GetComponent<Text>();

        prefab_CategoryItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/CategoryItem");

        categoryNames = new string[] { "", "杂项", "屋顶", "楼梯", "窗户", "门", "墙壁", "地板", "地基" };

        categoryIconsList = new List<Sprite>(categoryNum);
        categoryItemList = new List<CategoryItemController>(categoryNum);
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
            CategoryItemController cic = GameObject.Instantiate<GameObject>(prefab_CategoryItem,
                wheelBG_Transform).GetComponent<CategoryItemController>();
            cic.InitItem(i, categoryIconsList[i]);
            categoryItemList.Add(cic);
        }

        currentItem = categoryItemList[0];
        m_CategoryNameText.text = categoryNames[0];
    }

    /// <summary>
    /// 建造面板显示或者隐藏.
    /// </summary>
    private void ShowOrHide()
    {
        if (isUIShow)
        {
            wheelBG_Transform.gameObject.SetActive(false);
        }
        else
        {
            wheelBG_Transform.gameObject.SetActive(true);
        }

        isUIShow = !isUIShow;
    }
}
