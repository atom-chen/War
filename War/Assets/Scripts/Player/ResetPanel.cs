using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetPanel : MonoBehaviour
{
    private bool isEnter = false;                   // 是否已经按下重置.

    void Update()
    {
        if (isEnter == false)
        {
            if (Input.GetKeyDown(AppConst.ResetGameKey_1) || Input.GetKeyDown(AppConst.ResetGameKey_2))
            {
                isEnter = true;
                SceneManager.LoadScene("Game");
            }
        }
    }
}
