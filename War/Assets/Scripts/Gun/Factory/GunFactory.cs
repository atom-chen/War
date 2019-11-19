using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 枪械工厂类.
/// </summary>
public class GunFactory : MonoBehaviour
{
    public static GunFactory Instance;

    private Transform m_Transform;

    // 武器持有.
    private GameObject prefab_AssaultRifle;
    private GameObject prefab_Shotgun;
    private GameObject prefab_WoodenBow;
    private GameObject prefab_WoodenSpear;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();

        prefab_AssaultRifle = Resources.Load<GameObject>("Gun/Prefabs/Assault Rifle");
        prefab_Shotgun = Resources.Load<GameObject>("Gun/Prefabs/Shotgun");
        prefab_WoodenBow = Resources.Load<GameObject>("Gun/Prefabs/Wooden Bow");
        prefab_WoodenSpear = Resources.Load<GameObject>("Gun/Prefabs/Wooden Spear");
    }

    /// <summary>
    /// 实例化武器.
    /// </summary>
    public GameObject CreateGun(string weaponName)
    {
        GameObject weapon = null;

        switch (weaponName)
        {
            case "Assault Rifle":
                weapon = GameObject.Instantiate<GameObject>(prefab_AssaultRifle, m_Transform);
                InitWeapon(weapon, 100, 20, GunType.AssaultRifle);
                break;

            case "Shotgun":
                weapon = GameObject.Instantiate<GameObject>(prefab_Shotgun, m_Transform);
                InitWeapon(weapon, 200, 20, GunType.Shotgun);
                break;

            case "Wooden Bow":
                weapon = GameObject.Instantiate<GameObject>(prefab_WoodenBow, m_Transform);
                InitWeapon(weapon, 70, 20, GunType.WoodenBow);
                break;

            case "Wooden Spear":
                weapon = GameObject.Instantiate<GameObject>(prefab_WoodenSpear, m_Transform);
                InitWeapon(weapon, 50, 20, GunType.WoodenSpear);
                break;
        }

        return weapon;
    }

    /// <summary>
    /// 武器初始化.
    /// </summary>
    private void InitWeapon(GameObject weapon, int damage, int durable, GunType type)
    {
        GunControllerBase gcb = weapon.GetComponent<GunControllerBase>();
        gcb.Damage = damage;
        gcb.Durable = durable;
        gcb.M_GunType = type;
    }
}
