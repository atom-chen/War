using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 屏幕血屏效果.
/// </summary>
public class BloodScreenPanel : MonoBehaviour
{
    public static BloodScreenPanel Instance;

    private Image m_Image;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        m_Image = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// 屏幕血屏效果.
    /// </summary>
    public void UpdateBloodScreen(int lifeValue)
    {
        float currentAlpha = 1.0f - lifeValue / 1000.0f;
        Color color = new Color(m_Image.color.r, m_Image.color.g, m_Image.color.b, 200 * currentAlpha / 255.0f);

        m_Image.color = color;
    }
}
