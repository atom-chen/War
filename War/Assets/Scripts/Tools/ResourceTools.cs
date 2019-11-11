using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源加载工具类.
/// </summary>
public sealed class ResourceTools
{
    /// <summary>
    /// 加载文件夹的图标资源, 并添加到字典中.
    /// </summary>
    public static void LoadIconsAsset(string path, Dictionary<string, Sprite> iconsDic)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(path);
        for (int i = 0; i < sprites.Length; ++i)
        {
            iconsDic.Add(sprites[i].name, sprites[i]);
        }
    }

    /// <summary>
    /// 通过文件名称获取图片资源.
    /// </summary>
    public static Sprite GetIconByName(string spriteName, Dictionary<string, Sprite> iconsDic)
    {
        Sprite temp = null;
        iconsDic.TryGetValue(spriteName, out temp);
        return temp;
    }
}
