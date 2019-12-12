using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 按键输入控制器.
/// </summary>
public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private bool inventoryState = false;                // 背包面板默认隐藏.
    private bool buildState = false;                    // 建造面板默认隐藏.

    private FirstPersonController m_FPSController;      // 人物角色控制器.

    public bool BuildState 
    {
        get => buildState;
        set
        {
            if (value)
            {
                BuildPanelController.Instance.UIPanelShow();
                BuildPanelController.Instance.ResetUI();
            }
            else
            {
                BuildPanelController.Instance.UIPanelHide();
                BuildPanelController.Instance.DesctoryMaterialModel();
            }
            buildState = value;            
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();

        InitialState();
    }

    void Update()
    {
        InventoryPanelKey();
        ToolBarPanelKey();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_FPSController = GameObject.Find("FPSController").GetComponent<FirstPersonController>();
    }

    /// <summary>
    /// 初始化状态.
    /// </summary>
    private void InitialState()
    {
        InventoryPanelController.Instance.UIPanelHide();
        BuildPanelController.Instance.UIPanelHide();
    }

    /// <summary>
    /// 背包面板按下事件.
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(AppConst.InventoryPanelKey) && buildState == false)
        {
            if (inventoryState)
            {
                // 关闭背包.
                InventoryPanelController.Instance.UIPanelHide();

                // 启用脚本.
                m_FPSController.enabled = true;
            }
            else
            {
                // 打开背包.
                InventoryPanelController.Instance.UIPanelShow();

                // 禁用脚本.
                m_FPSController.enabled = false;

                // 显示鼠标.
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            inventoryState = !inventoryState;
        }
    }

    /// <summary>
    /// 工具栏模块按键检测.
    /// </summary>
    private void ToolBarPanelKey()
    {
        ToolBarKey(AppConst.ToolBarPanelKey_1, 0);
        ToolBarKey(AppConst.ToolBarPanelKey_2, 1);
        ToolBarKey(AppConst.ToolBarPanelKey_3, 2);
        ToolBarKey(AppConst.ToolBarPanelKey_4, 3);
        ToolBarKey(AppConst.ToolBarPanelKey_5, 4);
        ToolBarKey(AppConst.ToolBarPanelKey_6, 5);
        ToolBarKey(AppConst.ToolBarPanelKey_7, 6);
        ToolBarKey(AppConst.ToolBarPanelKey_8, 7);
    }

    /// <summary>
    /// 工具栏单个按键检测.
    /// </summary>
    private void ToolBarKey(KeyCode keyCode, int keyIndex)
    {
        if (Input.GetKeyDown(keyCode) && inventoryState == false)
        {
            ToolBarPanelController.Instance.SaveActiveSlotByKey(keyIndex);
        }
    }
}
