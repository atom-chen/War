using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 按键输入控制器.
/// </summary>
public class InputManager : MonoBehaviour
{
    private bool inventoryState = false;                // 背包面板默认隐藏.

    void Start()
    {
        InitialState();
    }

    void Update()
    {
        InventoryPanelKey();
        ToolBarPanelKey();
    }

    /// <summary>
    /// 初始化状态.
    /// </summary>
    private void InitialState()
    {
        InventoryPanelController.Instance.UIPanelHide();
    }

    /// <summary>
    /// 背包面板按下事件.
    /// </summary>
    private void InventoryPanelKey()
    {
        if (Input.GetKeyDown(AppConst.InventoryPanelKey))
        {


            if (inventoryState)
                InventoryPanelController.Instance.UIPanelHide();
            else
                InventoryPanelController.Instance.UIPanelShow();

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
        if (Input.GetKeyDown(keyCode))
        {
            ToolBarPanelController.Instance.SaveActiveSlotByKey(keyIndex);
        }
    }
}
