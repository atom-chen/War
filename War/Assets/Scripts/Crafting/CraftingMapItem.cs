﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 合成图谱数据实体类.
/// </summary>
public class CraftingMapItem
{
    private int mapId;                          // 图谱编号.
    private string[] mapContents;               // 图谱内容.
    private int materialsCount;                 // 图谱需要的材料数量.
    private string mapName;                     // 图谱名称.

    public int MapId 
    { 
        get => mapId; 
        set => mapId = value; 
    }

    public string[] MapContents 
    { 
        get => mapContents; 
        set => mapContents = value; 
    }

    public int MaterialsCount 
    {
        get => materialsCount; 
        set => materialsCount = value;
    }

    public string MapName 
    { 
        get => mapName; 
        set => mapName = value; 
    }
    

    public CraftingMapItem(int mapId, string[] mapContents, int materialsCount, string mapName)
    {
        this.mapId = mapId;
        this.mapContents = mapContents;
        this.materialsCount = materialsCount;
        this.mapName = mapName;
    }
}
