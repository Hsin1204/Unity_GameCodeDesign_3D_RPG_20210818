using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///���Ĩt��
///���ѵ��f���n���񭵮Ī�����
/// </summary>

//���u���b�Ĥ@���M�Τ~���@�Ρ�
//�M�γo�Ӹ}���ɡA�|�۰ʲK�[���w������
//[RequireComponent(typeof(����1����),typeof(����2����))]
[RequireComponent(typeof(AudioSource))]

public class Script_AudioSys : MonoBehaviour
{
    #region ���
    private AudioSource aud;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    #endregion

    #region ��k : ���}
    /// <summary>
    /// �H���`���q�����n��
    /// </summary>
    /// <param name="sound">����</param>
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
