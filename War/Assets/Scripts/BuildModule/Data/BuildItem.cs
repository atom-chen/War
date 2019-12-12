using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 建造数据持久化需要的数据实体类.
/// </summary>
public class BuildItem
{
    /// <summary>
    /// 物体名称.
    /// </summary>
    private string name;

    /// <summary>
    /// 物体位置信息.
    /// </summary>
    private string posX;
    private string posY;
    private string posZ;

    /// <summary>
    /// 物体旋转四元数.
    /// </summary>
    private string rotX;
    private string rotY;
    private string rotZ;
    private string rotW;

    public string Name { get => name; set => name = value; }
    public string PosX { get => posX; set => posX = value; }
    public string PosY { get => posY; set => posY = value; }
    public string PosZ { get => posZ; set => posZ = value; }
    public string RotX { get => rotX; set => rotX = value; }
    public string RotY { get => rotY; set => rotY = value; }
    public string RotZ { get => rotZ; set => rotZ = value; }
    public string RotW { get => rotW; set => rotW = value; }

    public BuildItem() { }
    public BuildItem(string name, string posX, string posY, string posZ, 
        string rotX, string rotY, string rotZ, string rotW)
    {
        this.name = name;

        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;

        this.rotX = rotX;
        this.rotY = rotY;
        this.rotZ = rotZ;
        this.rotW = rotW;
    }
}
