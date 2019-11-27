
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

/// <summary>
/// AI当前状态.
/// </summary>
public enum AIState
{
    /// <summary>
    /// 默认状态.
    /// </summary>
    IDLE,

    /// <summary>
    /// 行走状态.
    /// </summary>
    WALK,

    /// <summary>
    /// 进入奔跑状态.
    /// </summary>
    ENTERRUN,

    /// <summary>
    /// 退出奔跑状态.
    /// </summary>
    EXITRUN,

    /// <summary>
    /// AI进入攻击状态.
    /// </summary>
    ENTERATTACK,

    /// <SUMMARY>
    /// AI退出攻击状态.
    /// </SUMMARY>
    EXITATTACK,

    /// <summary>
    /// AI死亡状态.
    /// </summary>
    DEATHSTATE
}

public enum ClipName
{
    /// <summary>
    /// 野猪攻击音效.
    /// </summary>
    BoarAttack,

    /// <summary>
    /// 野猪死亡音效.
    /// </summary>
    BoarDeath,

    /// <summary>
    /// 野猪受伤音效.
    /// </summary>
    BoarInjured,

    /// <summary>
    /// 丧尸攻击音效.
    /// </summary>
    ZombieAttack,

    /// <summary>
    /// 丧尸死亡音效.
    /// </summary>
    ZombieDeath,

    /// <summary>
    /// 丧尸受伤音效.
    /// </summary>
    ZombieInjured,

    /// <summary>
    /// 丧尸尖叫音效.
    /// </summary>
    ZombieScream,

    /// <summary>
    /// 子弹命中地面音效.
    /// </summary>
    BulletImpactDirt,

    /// <summary>
    /// 子弹命中身体音效.
    /// </summary>
    BulletImpactFlesh,

    /// <summary>
    /// 子弹命中金属音效.
    /// </summary>
    BulletImpactMetal,

    /// <summary>
    /// 子弹命中石头音效.
    /// </summary>
    BulletImpactStone,

    /// <summary>
    /// 子弹命中木材音效.
    /// </summary>
    BulletImpactWood,

    /// <summary>
    /// 玩家角色急促呼吸声.
    /// </summary>
    PlayerBreathingHeavy,

    /// <summary>
    /// 玩家角色受伤音效.
    /// </summary>
    PlayerHurt,

    /// <summary>
    /// 玩家角色死亡音效.
    /// </summary>
    PlayerDeath,

    /// <summary>
    /// 身体命中音效.
    /// </summary>
    BodyHit
}