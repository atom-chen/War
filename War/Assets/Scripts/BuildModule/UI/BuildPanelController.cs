using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造模块C层.
/// </summary>
public class BuildPanelController : MonoBehaviour, IUIPanelHideAndShow
{
    public static BuildPanelController Instance;

    private BuildPanelView m_BuildPanelView;

    // 射线相关对象.
    private Ray ray;
    private RaycastHit hit;

    private const int categoryNum = 9;                      // 建造类别数量.
    private string[] categoryNames;                         // 建造类别名称.
    private List<CategoryItemController> categoryItemList;  // 建造类别UI集合.

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

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        // 切换到建造角色, 默认打开建造面板.
        if (m_BuildPanelView != null)
        {
            isUIShow = false;
            ShowOrHide();
        }
    }

    void Start()
    {
        FindAndLoadInit();
        CreateAllCategory();
        ShowOrHide();
    }

    void Update()
    {
        RightMouseButtonDown();
        MouseScroll();
        LeftMouseButtonDown();
        SetModelPosition();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_BuildPanelView = gameObject.GetComponent<BuildPanelView>();

        categoryNames = new string[] { "", "[杂项]", "[屋顶]", "[楼梯]", "[窗户]", "[门]", "[墙壁]", "[地板]", "[地基]" };

        categoryItemList = new List<CategoryItemController>(categoryNum);
    }

    /// <summary>
    /// 生成全部类别UI以及类别下的具体材料UI.
    /// </summary>
    private void CreateAllCategory()
    {
        for (int i = 0; i < categoryNum; ++i)
        {
            // 生成类别UI.
            CategoryItemController cic = GameObject.Instantiate<GameObject>(
                m_BuildPanelView.Prefab_CategoryItem, m_BuildPanelView.WheelBG_Transform).
                GetComponent<CategoryItemController>();
            cic.InitCategoryItem(i, m_BuildPanelView.CategoryIconsList[i]);
            categoryItemList.Add(cic);

            // 对应类别下的材料UI.
            for (int j = 0; m_BuildPanelView.MaterialIconList[i] != null && 
                j < m_BuildPanelView.MaterialIconList[i].Length; ++j)
            {           
                GameObject materialItem = GameObject.Instantiate<GameObject>(
                    m_BuildPanelView.Prefab_MaterialItem, m_BuildPanelView.WheelBG_Transform);

                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, materialRoatationZ));
                materialItem.GetComponent<MaterialItemController>().InitMaterialItem(
                    m_BuildPanelView.MaterialIconList[i][j],
                    rotation, cic.GetComponent<Transform>());
                materialRoatationZ += 13.3f;

                cic.AddMaterial(materialItem);
                cic.NormalItem();
            }
        }

        currentItem = categoryItemList[0];
        m_BuildPanelView.M_CategoryNameText.text = categoryNames[0];
    }

    /// <summary>
    /// 建造面板显示或者隐藏.
    /// </summary>
    private void ShowOrHide()
    {
        if (isUIShow)
        {
            m_BuildPanelView.WheelBG_Transform.gameObject.SetActive(false);
        }
        else
        {
            m_BuildPanelView.WheelBG_Transform.gameObject.SetActive(true);
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
            m_BuildPanelView.M_CategoryNameText.text = categoryNames[categoryIndex];

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

            m_BuildPanelView.M_CategoryNameText.text = 
                m_BuildPanelView.MaterialIconNameList[categoryIndex][materialIndex];
            currentMaterialModel =
                m_BuildPanelView.MaterialModelList[categoryIndex][materialIndex];

            currentMaterial = targetMaterial;
        }
    }

    /// <summary>
    /// 设置建造模型的位置信息.
    /// </summary>
    private void SetModelPosition()
    {
        if (materialModel != null) 
        {
            ray = m_BuildPanelView.M_EnvCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 15.0f, ~(1 << LayerMask.NameToLayer("BuildModel"))))
            {
                // 当前模型在吸附, 就不需要设置位置.
                MaterialModelBase mmb = materialModel.GetComponent<MaterialModelBase>();
                if (!mmb.IsAttach)
                    materialModel.GetComponent<Transform>().position = hit.point;

                // 当模型相隔足够远时, 不再吸附.
                if (Vector3.Distance(hit.point, materialModel.GetComponent<Transform>().position) > 3)
                {
                    mmb.IsAttach = false;
                }
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

    /// <summary>
    /// 鼠标右键逻辑.
    /// </summary>
    private void RightMouseButtonDown()
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
    }

    /// <summary>
    /// 鼠标滚轮操作逻辑.
    /// </summary>
    private void MouseScroll()
    {
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
    }

    /// <summary>
    /// 鼠标左键事件.
    /// </summary>
    private void LeftMouseButtonDown()
    {
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
                m_BuildPanelView.WheelBG_Transform.gameObject.SetActive(false);
                isUIShow = false;
                isCategoryCtrl = true;
            }

            // 将实例化的材料回归默认颜色, 如果不能生成就直接退出.
            MaterialModelBase mmb = null;
            if (materialModel != null && !(mmb = materialModel.GetComponent<MaterialModelBase>()).CanPut)
            {
                return;
            }
            else if (materialModel != null && mmb.CanPut)
            {
                materialModel.layer = LayerMask.NameToLayer("BuildModelEnd");
                mmb.NormalModel();

                // 临时删除测试.
                GameObject.Destroy(mmb);
            }

            // 实例化建造材料.
            if (currentMaterialModel != null)
            {
                materialModel = GameObject.Instantiate<GameObject>(currentMaterialModel,
                    m_BuildPanelView.Player_Transform.position + Vector3.forward * 10,
                    Quaternion.identity, m_BuildPanelView.BuildModelsParent);
            }
        }
    }

    public void UIPanelShow()
    {
        gameObject.SetActive(true);
    }

    public void UIPanelHide()
    {
        if (this != null)
            gameObject.SetActive(false);
    }
}
