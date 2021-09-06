using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// �ĤT�H�ٱ��
/// �D�n�\��:���D�B����
/// </summary>
public class Script_ThirdPersonControlloer : MonoBehaviour
{
    #region ��� Field
    /*
     �x�s�C����ơA�Ҧp:���ʳt�סB���D���׵���...
     �`�Υ|�j���� : ���(int)�B�B�I��(float)�B�r��(string)�B���L��(bool)
     ���y�k:�׹��� ��ƫ��A �W�� ����
     �׹��� : 
     1.���} public  - ���\��L���O�ק� (�i�b�ݩʭ��O���ƭ�)
     2.�p�� private - �T���L���O�s�� (���i�b�ݩʭ��O���ƭ�)
     Unity �H�ݩʭ��O��Ƭ��D
     ����ݩ� : ���U�����
     ����ݩʻy�k : [�ݩʦW��(�ݩʭ�)]
     */
    //Unity�������
    //�C��
    public Color color;
    public Color colorW = Color.white;
    public Color c1 = new Color(1f, 0.5f, 0);
    public Color acolor = new Color(1f,0.5f,0,0.5f);

    //�y�� Vector 2-4
    public Vector2 v2;
    public Vector2 v2R = Vector2.right;
    public Vector2 v2U = Vector2.up;
    public Vector2 V2One = Vector2.one;
    public Vector2 v2Cus = new Vector2(2.5f, 50f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector3 v3F = Vector3.forward;
    public Vector4 v4 = new Vector4(1, 2, 3, 4);
    //�C�| enum
    public KeyCode keys;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    //�C���������
    public AudioClip sound;
    public VideoClip movie;
    public Sprite sprite;
    public Texture2D tex;
    public Material mat;

    #endregion

    #region �ݩ� Property

    #endregion

    #region ��k Method

    #endregion

    #region �ƥ� Event

    #endregion
    [Header("���ʳt��"),Tooltip("�Ψӽվ㨤�Ⲿ�ʳt��"),Range(1,50)]
    public float speed = 10.5f;


}
