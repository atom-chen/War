﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造类别UI控制器.
/// </summary>
public class CategoryItemController : MonoBehaviour
{
    private Transform m_Transform;
    private Image m_BGImage;                        // 背景UI.
    private Image m_CategoryIcon;                   // 类别图标UI.

    private List<GameObject> materialsList;         // 当前类别的建造材料集合.

    public List<GameObject> MaterialsList { get => materialsList; }

    void Awake()
    {
        FindAndLoadInit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_BGImage = gameObject.GetComponent<Image>();
        m_CategoryIcon = m_Transform.Find("Icon").GetComponent<Image>();

        materialsList = new List<GameObject>(3);
    }

    /// <summary>
    /// 外部调用初始化.
    /// </summary>
    public void InitCategoryItem(int index, Sprite iconSprite)
    {
        m_Transform.localRotation = Quaternion.Euler(new Vector3(0, 0, index * 40));
        m_CategoryIcon.GetComponent<Transform>().rotation = Quaternion.identity;

        if (iconSprite == null)
        {
            m_CategoryIcon.enabled = false;
        }
        else
        {
            m_CategoryIcon.sprite = iconSprite;
            m_BGImage.enabled = false;
        }


        gameObject.name = "CategoryItem_" + index;
    }

    /// <summary>
    /// 类别默认未选中状态.
    /// </summary>
    public void NormalItem()
    {
        m_BGImage.enabled = false;

        for (int i = 0; i < materialsList.Count; ++i)
        {
            materialsList[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 类别选中激活状态.
    /// </summary>
    public void ActiveItem()
    {
        m_BGImage.enabled = true;

        for (int i = 0; i < materialsList.Count; ++i)
        {
            materialsList[i].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 添加建造材料.
    /// </summary>
    public void AddMaterial(GameObject material)
    {
        materialsList.Add(material);
    }
}
