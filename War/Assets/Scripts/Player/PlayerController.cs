using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// 角色逻辑控制器.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Transform m_Transform;
    private FirstPersonController m_FPSController;

    private int lifeValue = 1000;                       // 角色生命值.
    private int vitValue = 100;                         // 角色体力值.
    private int timeClick = 0;                          // 计时点.

    private AudioSource breathAudio;                    // 玩家呼吸声.
    private bool isPlaybreathAudio = false;             // 地区是否播放呼吸声.

    public int LifeValue 
    { 
        get => lifeValue; 
        set
        {
            lifeValue = value;
            if (lifeValue <= 0)
            {
                PlayerDeath();
                lifeValue = 0;
            }            

            // 更新UI.
            PlayerInfoPanel.Instance.UpdateLifeBarUI(lifeValue);
            BloodScreenPanel.Instance.UpdateBloodScreen(lifeValue);
        }
    }
    public int VitValue 
    {
        get => vitValue; 
        set
        {
            vitValue = value;

            // 越界保护.
            if (vitValue > 100)
                vitValue = 100;
            else if (vitValue <= 30)
                vitValue = 30;

            // 体力值影响速度.
            m_FPSController.M_RunSpeed = 10.0f * (vitValue / 100.0f);
            m_FPSController.M_WalkSpeed = 5.0f * (vitValue / 100.0f);

            // 更新UI.
            PlayerInfoPanel.Instance.UpdateVitBarUI(vitValue);
        }
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        FindAndLoadInit();

        StartCoroutine("RestoreVit");
    }

    void Update()
    {
        CutVit();
    }

    void OnTriggerEnter(Collider other)
    {
        // 角色进入门型建造需要开门.
        if (other.gameObject.name == "DoorTrigger_Done")
        {
            Transform door = other.gameObject.GetComponent<Transform>().parent.Find("Door");
            if (door != null)
            {
                door.Rotate(Vector3.up, 90);
            }
        }

        // 触发搜集爆出的石头材料.
        else if (other.gameObject.tag == "StoneMaterial")
        {
            // 材料搜集逻辑.
            InventoryPanelController.Instance.CollectMaterials(other.gameObject.GetComponent<RockMaterial>().ItemName);

            // 销毁测试.
            GameObject.Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 角色退出门型建造需要关门.
        if (other.gameObject.name == "DoorTrigger_Done")
        {
            Transform door = other.gameObject.GetComponent<Transform>().parent.Find("Door");
            if (door != null)
            {
                door.Rotate(Vector3.up, -90);
            }
        }
    }

    /// <summary>
    /// 查找加载初始化.
    /// </summary>
    private void FindAndLoadInit()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_FPSController = gameObject.GetComponent<FirstPersonController>();

        breathAudio = AudioManager.Instance.AddAudioSourceComponent(gameObject,
            ClipName.PlayerBreathingHeavy, false, true);
    }

    /// <summary>
    /// 角色体力值自动恢复.
    /// </summary>
    private IEnumerator RestoreVit()
    {
        Vector3 lastPos;
        while (true)
        {
            lastPos = m_Transform.position;

            yield return new WaitForSeconds(1);
            if (m_Transform.position == lastPos)
            {
                VitValue += 5;
            }

            // 停止播放呼吸声.
            if (VitValue > 60 && isPlaybreathAudio == true)
            {
                breathAudio.Stop();
                isPlaybreathAudio = false;
            }
        }
    }

    /// <summary>
    /// 玩家体力值消耗.
    /// </summary>
    private void CutVit()
    {
        ++timeClick;
        if (timeClick < 60)
            return;

        if (m_FPSController.CurrentState == PlayerState.WALK)
        {
            VitValue -= 1;
        }
        else if (m_FPSController.CurrentState == PlayerState.RUN)
        {
            VitValue -= 2;
        }

        // 呼吸声音效.
        if (VitValue <= 60 && isPlaybreathAudio == false)
        {
            breathAudio.Play();
            isPlaybreathAudio = true;
        }

        timeClick = 0;
    }

    /// <summary>
    /// 玩家角色受伤音效.
    /// </summary>
    public void PlayPlayerHurtAudio()
    {
        AudioManager.Instance.PlayAudioClipByName(ClipName.PlayerHurt, m_Transform.position);
    }

    /// <summary>
    /// 玩家角色死亡.
    /// </summary>
    private void PlayerDeath()
    {
        // 播放死亡音效.
        AudioManager.Instance.PlayAudioClipByName(ClipName.PlayerDeath, m_Transform.position);

        // 禁用输入和角色控制.
        InputManager.Instance.enabled = false;
        m_FPSController.enabled = false;

        // 跳转过渡场景.
        Invoke("EnterResetScene", 1);
    }

    /// <summary>
    /// 跳转过渡场景.
    /// </summary>
    private void EnterResetScene()
    {
        SceneManager.LoadScene("ResetScene");
    }
}
