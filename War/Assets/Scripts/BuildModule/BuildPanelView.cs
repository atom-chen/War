using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造模块V层.
/// </summary>
public class BuildPanelView : MonoBehaviour
{
    private Transform m_Transform;
    private Transform wheelBG_Transform;                    // 环形UI背景.
    private Text m_CategoryNameText;                        // 建造类别名称.

    private Transform player_Transform;                     // 玩家角色位置.
    private Camera m_EnvCamera;                             // 环境摄像机发射射线.

    private GameObject prefab_CategoryItem;                 // 建造类别UI预制体.
    private GameObject prefab_MaterialItem;                 // 建造材料UI预制体.

    private Transform buildModelsParent;                    // 实例化后的建造模型的父物体.

    private const int categoryNum = 9;                      // 建造类别数量.
    private List<Sprite> categoryIconsList;                 // 建造类别图标.
    private List<Sprite[]> materialIconList;                // 建造材料图标.
    private List<string[]> materialIconNameList;            // 建造材料图标名称.
    private List<GameObject[]> materialModelList;           // 建造模型.

    public Transform M_Transform { get => m_Transform; }
    public Transform WheelBG_Transform { get => wheelBG_Transform; }
    public Text M_CategoryNameText { get => m_CategoryNameText; }
    public Transform Player_Transform { get => player_Transform; }
    public Camera M_EnvCamera { get => m_EnvCamera; }
    public GameObject Prefab_CategoryItem { get => prefab_CategoryItem; }
    public GameObject Prefab_MaterialItem { get => prefab_MaterialItem; }
    public Transform BuildModelsParent { get => buildModelsParent; }
    public List<Sprite> CategoryIconsList { get => categoryIconsList; }
    public List<Sprite[]> MaterialIconList { get => materialIconList; }
    public List<string[]> MaterialIconNameList { get => materialIconNameList; }
    public List<GameObject[]> MaterialModelList { get => materialModelList; }

    void Awake()
    {
        FindAndLoadInit();
        LoadCategoryIcons();
        LoadMaterialIcons();
        InitMaterialsIconName();
        LoadMaterialsModels();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        wheelBG_Transform = m_Transform.Find("Wheel_BG");
        m_CategoryNameText = wheelBG_Transform.Find("CategroyName").GetComponent<Text>();

        m_EnvCamera = Camera.main;
        player_Transform = m_EnvCamera.GetComponent<Transform>().parent;

        prefab_CategoryItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/CategoryItem");
        prefab_MaterialItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/MaterialItem");

        buildModelsParent = GameObject.Find("BuildModels").GetComponent<Transform>();

        categoryIconsList = new List<Sprite>(categoryNum);
        materialIconList = new List<Sprite[]>(categoryNum);
        materialIconNameList = new List<string[]>(categoryNum);
        materialModelList = new List<GameObject[]>(categoryNum);
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
    /// 加载合成材料图标资源.
    /// </summary>
    private void LoadMaterialIcons()
    {
        materialIconList.Add(null);

        // 灯、柱子、梯子.
        materialIconList.Add(new Sprite[] {
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Ceiling Light"),
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Pillar_Wood"),
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Wooden Ladder")
        });

        // 屋顶.
        materialIconList.Add(new Sprite[] {
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Roof_Metal"),
            null
        });

        // 楼梯.
        materialIconList.Add(new Sprite[] {
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/L Shaped Stairs_Wood"),
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Stairs_Wood")
        });

        // 窗户.
        materialIconList.Add(new Sprite[] {
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Window_Wood"),
            null
        });

        // 门.
        materialIconList.Add(new Sprite[] {
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Wooden Door"),
            null
        });

        // 墙壁.
        materialIconList.Add(new Sprite[] {
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Wall_Wood"),
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Window Frame_Wood"),
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Doorway_Wood")
        });

        // 地板.
        materialIconList.Add(new Sprite[] {
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Floor_Wood"),
            null
        });

        // 地基.
        materialIconList.Add(new Sprite[] {
            null,
            Resources.Load<Sprite>("BuildModule/UI/MaterialIcons/Platform_Wood"),
            null
        });
    }

    /// <summary>
    /// 初始化材料名称, 在中央才能显示.
    /// </summary>
    private void InitMaterialsIconName()
    {
        materialIconNameList.Add(null);
        materialIconNameList.Add(new string[] { "吊灯", "木柱", "梯子" });
        materialIconNameList.Add(new string[] { null, "屋顶", null });
        materialIconNameList.Add(new string[] { "直梯", null, "L形梯" });
        materialIconNameList.Add(new string[] { null, "窗户", null });
        materialIconNameList.Add(new string[] { null, "门", null });
        materialIconNameList.Add(new string[] { "普通墙壁", "门形墙壁", "窗形墙壁" });
        materialIconNameList.Add(new string[] { null, "地板", null });
        materialIconNameList.Add(new string[] { null, "地基", null });
    }

    /// <summary>
    /// 加载建造模型.
    /// </summary>
    private void LoadMaterialsModels()
    {
        materialModelList.Add(null);

        // 灯、柱子、梯子.
        materialModelList.Add(new GameObject[] {
            Resources.Load<GameObject>("BuildModule/BuildModels/Ceiling_Light"),
            Resources.Load<GameObject>("BuildModule/BuildModels/Pillar"),
            Resources.Load<GameObject>("BuildModule/BuildModels/Wooden")
        });

        // 屋顶.
        materialModelList.Add(new GameObject[] {
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Roof"),
            null
        });

        // 楼梯.
        materialModelList.Add(new GameObject[] {
            Resources.Load<GameObject>("BuildModule/BuildModels/L_Shaped_Stairs"),
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Stairs")
        });

        // 窗户.
        materialModelList.Add(new GameObject[] {
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Window"),
            null
        });

        // 门.
        materialModelList.Add(new GameObject[] {
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Door"),
            null
        });

        // 墙壁.
        materialModelList.Add(new GameObject[] {
            Resources.Load<GameObject>("BuildModule/BuildModels/Wall"),
            Resources.Load<GameObject>("BuildModule/BuildModels/Window_Frame"),
            Resources.Load<GameObject>("BuildModule/BuildModels/Doorway")
        });

        // 地板.
        materialModelList.Add(new GameObject[] {
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Floor"),
            null
        });

        // 地基.
        materialModelList.Add(new GameObject[] {
            null,
            Resources.Load<GameObject>("BuildModule/BuildModels/Platform"),
            null
        });
    }
}
