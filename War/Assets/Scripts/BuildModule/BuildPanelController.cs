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

    private Transform player_Transform;                     // 玩家角色位置.
    private Camera m_EnvCamera;                             // 环境摄像机发射射线.

    // 射线相关对象.
    private Ray ray;
    private RaycastHit hit;

    private GameObject prefab_CategoryItem;                 // 建造类别UI预制体.
    private GameObject prefab_MaterialItem;                 // 建造材料UI预制体.

    private const int categoryNum = 9;                      // 建造类别数量.
    private string[] categoryNames;                         // 建造类别名称.
    private List<Sprite> categoryIconsList;                 // 建造类别图标.
    private List<CategoryItemController> categoryItemList;  // 建造类别UI集合.

    private List<Sprite[]> materialIconList;                // 建造材料图标.
    private List<string[]> materialIconNameList;            // 建造材料图标名称.
    private List<GameObject[]> materialModelList;           // 建造模型.
    private float materialRoatationZ = 26.6f;               // 建造材料初始Z轴旋转.  

    private bool isUIShow = false;                          // 建造面板是否显示.
    private float categoryScrollNum = 90000.0f;             // 用于记录滚轮的数值.
    private int categoryIndex = 0;                          // 建造类别索引.

    private CategoryItemController currentItem;             // 当前选中的建造类别.
    private CategoryItemController targetItem;              // 目标选中的建造类别.

    private float materialScrollNum = 3000.0f;              // 用于记录具体材料的鼠标滚动值.
    private int materialIndex = 0;                          // 建造材料索引.
    private MaterialItemController currentMaterial;         // 当前选中的建造材料.
    private MaterialItemController targetMaterial;          // 当前选中的建造材料.

    private bool isCategoryCtrl = true;                     // T:滚轮操作分类; F:滚轮操作材料.
    private GameObject currentMaterialModel;                // 当前需要实例化的建筑材料.
    private GameObject materialModel;                       // 实例化生成后的建造材料.

    void Start()
    {
        FindAndLoadInit();
        LoadCategoryIcons();
        LoadMaterialIcons();
        InitMaterialsIconName();
        LoadMaterialsModels();

        CreateAllCategory();

        ShowOrHide();
    }

    void Update()
    {
        // 按下鼠标右键打开或关闭建造.
        if (Input.GetMouseButtonDown(1))
        {
            if (isCategoryCtrl)
            {
                ShowOrHide();

                if (currentMaterialModel != null)
                    currentMaterialModel = null;
            }
            else
            {
                isCategoryCtrl = true;

                if (currentMaterial != null)
                    currentMaterial.NormalItem();
            }
        }

        // 鼠标滚轮切换建造类别逻辑.
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (isUIShow && isCategoryCtrl && scrollValue != 0) 
        {
            MouseScrollWheelCategory(scrollValue);
        }

        // 鼠标滚轮切换建造材料逻辑.
        if (isUIShow && !isCategoryCtrl && scrollValue != 0)
        {
            MouseScrollWheelMaterial(scrollValue);
        }

        // 鼠标左键进入二级菜单.
        if (Input.GetMouseButtonDown(0)) 
        {
            // 第一个空白区域相当于切换建造材料使用.
            if (currentItem == categoryItemList[0])
            {
                SetMaterialToNull();
                return;
            }

            if (currentMaterialModel == null)
                isCategoryCtrl = false;

            // 有建造材料模型, 隐藏建造UI.
            if (currentMaterialModel != null && isUIShow)
            {
                wheelBG_Transform.gameObject.SetActive(false);
                isUIShow = false;
                isCategoryCtrl = true;
            }

            // 将实例化的材料回归默认颜色, 如果不能生成就直接退出.
            if (materialModel != null && !materialModel.GetComponent<PlatformController>().CanPut)
            {
                return;
            }
            else if (materialModel != null && materialModel.GetComponent<PlatformController>().CanPut)
            {
                materialModel.GetComponent<PlatformController>().NormalModel();

                // 临时删除测试.
                GameObject.Destroy(materialModel.GetComponent<PlatformController>());
            }

            // 实例化建造材料.
            if (currentMaterialModel != null)
            {
                materialModel = GameObject.Instantiate<GameObject>(currentMaterialModel,
                    player_Transform.position + Vector3.forward * 10, Quaternion.identity);
            }
        }

        SetModelPosition();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        wheelBG_Transform = m_Transform.Find("Wheel_BG");
        m_CategoryNameText = wheelBG_Transform.Find("CategroyName").GetComponent<Text>();

        player_Transform = PlayerController.Instance.GetComponent<Transform>();
        m_EnvCamera = Camera.main;

        prefab_CategoryItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/CategoryItem");
        prefab_MaterialItem = Resources.Load<GameObject>("BuildModule/UI/Prefabs/MaterialItem");

        categoryNames = new string[] { "", "[杂项]", "[屋顶]", "[楼梯]", "[窗户]", "[门]", "[墙壁]", "[地板]", "[地基]" };

        categoryIconsList = new List<Sprite>(categoryNum);
        categoryItemList = new List<CategoryItemController>(categoryNum);
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

    /// <summary>
    /// 生成全部类别UI以及类别下的具体材料UI.
    /// </summary>
    private void CreateAllCategory()
    {
        for (int i = 0; i < categoryNum; ++i)
        {
            // 生成类别UI.
            CategoryItemController cic = GameObject.Instantiate<GameObject>(prefab_CategoryItem,
                wheelBG_Transform).GetComponent<CategoryItemController>();
            cic.InitCategoryItem(i, categoryIconsList[i]);
            categoryItemList.Add(cic);

            // 对应类别下的材料UI.
            for (int j = 0; materialIconList[i] != null && j < materialIconList[i].Length; ++j)
            {           
                GameObject materialItem = GameObject.Instantiate<GameObject>(prefab_MaterialItem,
                    wheelBG_Transform);

                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, materialRoatationZ));
                materialItem.GetComponent<MaterialItemController>().InitMaterialItem(materialIconList[i][j],
                    rotation, cic.GetComponent<Transform>());
                materialRoatationZ += 13.3f;

                cic.AddMaterial(materialItem);
                cic.NormalItem();
            }
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

    /// <summary>
    /// 鼠标滚轮操作切换建造类别.
    /// </summary>
    private void MouseScrollWheelCategory(float scrollValue)
    {
        // 滚轮切换建造类别.
        categoryScrollNum += scrollValue * 3;
        categoryIndex = Mathf.Abs((int)categoryScrollNum % categoryNum);

        targetItem = categoryItemList[categoryIndex];
        if (currentItem != targetItem)
        {
            currentItem.NormalItem();
            targetItem.ActiveItem();
            m_CategoryNameText.text = categoryNames[categoryIndex];

            currentItem = targetItem;
        }
    }

    /// <summary>
    /// 鼠标滚轮操作切换建造材料.
    /// </summary>
    private void MouseScrollWheelMaterial(float scrollValue)
    {
        // 滚轮切换建造材料.
        materialScrollNum += scrollValue * 3;
        materialIndex = Mathf.Abs((int)materialScrollNum % 3);

        targetMaterial = targetItem.MaterialsList[materialIndex].GetComponent<MaterialItemController>();
        if (currentMaterial != targetMaterial)
        {
            if (currentMaterial != null)
                currentMaterial.NormalItem();
            targetMaterial.ActiveItem();

            m_CategoryNameText.text = materialIconNameList[categoryIndex][materialIndex];
            currentMaterialModel = materialModelList[categoryIndex][materialIndex];

            currentMaterial = targetMaterial;
        }
    }

    /// <summary>
    /// 设置建造模型的位置信息.
    /// </summary>
    private void SetModelPosition()
    {
        ray = m_EnvCamera.ScreenPointToRay(Input.mousePosition);

        if (materialModel != null && Physics.Raycast(ray, out hit, 15.0f, ~(1 << 13))) 
        {
            // 当前模型在吸附, 就不需要设置位置.
            if (!materialModel.GetComponent<PlatformController>().IsAttach)
                materialModel.GetComponent<Transform>().position = hit.point;

            // 当模型相隔足够远时, 不再吸附.
            if (Vector3.Distance(hit.point, materialModel.GetComponent<Transform>().position) > 1)
            {
                materialModel.GetComponent<PlatformController>().IsAttach = false;
            }
        }
    }

    /// <summary>
    /// 置空当前建造材料, 才能切换另一个建造材料.
    /// </summary>
    private void SetMaterialToNull()
    {
        if (currentMaterialModel != null)
            currentMaterialModel = null;

        if (materialModel != null)
        {
            GameObject.Destroy(materialModel);
            materialModel = null;
        }
    }
}
