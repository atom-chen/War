using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 音频管理器.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private Dictionary<string, AudioClip> audioClipsDic;                // 音频资源字典.

    void Awake()
    {
        Instance = this;

        LoadAudios();
    }

    /// <summary>
    /// 加载音效资源.
    /// </summary>
    private void LoadAudios()
    {
        audioClipsDic = new Dictionary<string, AudioClip>();

        AudioClip[] clips = Resources.LoadAll<AudioClip>("AI/Audios");
        for (int i = 0; i < clips.Length; ++i)
        {
            audioClipsDic.Add(clips[i].name, clips[i]);
        }
    }

    /// <summary>
    /// 通过音频文件名称获取音频资源.
    /// </summary>
    public AudioClip GetAudioClipByName(ClipName clipName)
    {
        AudioClip temp = null;
        audioClipsDic.TryGetValue(clipName.ToString(), out temp);
        return temp;
    }

    /// <summary>
    /// 在指定位置播放指定音效.
    /// </summary>
    public void PlayAudioClipByName(ClipName clipName, Vector3 pos)
    {
        AudioClip clip = GetAudioClipByName(clipName);

        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, pos);
    }

    /// <summary>
    /// 动态添加音频播放组件.
    /// </summary>
    public AudioSource AddAudioSourceComponent(GameObject target, ClipName clipName,
        bool playOnAwake = true, bool loop = true)
    {
        AudioSource audioSource = target.AddComponent<AudioSource>();
        audioSource.clip = GetAudioClipByName(clipName);
        if (playOnAwake)
            audioSource.Play();
        audioSource.loop = loop;

        return audioSource;
    }
}
