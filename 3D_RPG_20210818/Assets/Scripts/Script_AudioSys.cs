using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///音效系統
///提供窗口給要播放音效的物件
/// </summary>

//※只有在第一次套用才有作用※
//套用這個腳本時，會自動添加指定的元件
//[RequireComponent(typeof(元件1類型),typeof(元件2類型))]
[RequireComponent(typeof(AudioSource))]

public class Script_AudioSys : MonoBehaviour
{
    #region 欄位
    private AudioSource aud;
    #endregion

    #region 事件
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    #endregion

    #region 方法 : 公開
    /// <summary>
    /// 以正常音量播放聲音
    /// </summary>
    /// <param name="sound">音效</param>
    public void PlaySound(AudioClip sound)
    {
        aud.PlayOneShot(sound);
    }

    public void RandomVolume(AudioClip sound)
    {
        float volume = Random.Range(0.7f, 1.2f);
        aud.PlayOneShot(sound, volume);
    }
    #endregion


}
