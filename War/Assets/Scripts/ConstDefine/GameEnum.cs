﻿
/// <summary>
/// 武器类型.
/// </summary>
public enum GunType
{
    /// <summary>
    /// 突击步枪.
    /// </summary>
    AssaultRifle,
    
    /// <summary>
    /// 霰弹枪.
    /// </summary>
    Shotgun,

    /// <summary>
    /// 木弓.
    /// </summary>
    WoodenBow,

    /// <summary>
    /// 长矛.
    /// </summary>
    WoodenSpear
};

/// <summary>
/// 模型材质类型.
/// </summary>
public enum MaterialType
{
    /// <summary>
    /// 木材.
    /// </summary>
    WOOD,

    /// <summary>
    /// 金属.
    /// </summary>
    METAL,

    /// <summary>
    /// 石头.
    /// </summary>
    STONE
}

/// <summary>
/// AI角色类型.
/// </summary>
public enum AIType
{
    /// <summary>
    /// 默认类型.
    /// </summary>
    NULL,

    /// <summary>
    /// 人形丧尸AI.
    /// </summary>
    CANNIBAL,

    /// <summary>
    /// 野猪AI.
    /// </summary>
    BOAR
}