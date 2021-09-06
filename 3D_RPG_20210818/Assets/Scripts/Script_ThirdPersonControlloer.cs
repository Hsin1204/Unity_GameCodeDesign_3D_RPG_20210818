using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/// <summary>
/// 第三人稱控制器
/// 主要功能:跳躍、移動
/// </summary>
public class Script_ThirdPersonControlloer : MonoBehaviour
{
    #region 欄位 Field
    /*
     儲存遊戲資料，例如:移動速度、跳躍高度等等...
     常用四大類型 : 整數(int)、浮點數(float)、字串(string)、布林值(bool)
     欄位語法:修飾詞 資料型態 名稱 結尾
     修飾詞 : 
     1.公開 public  - 允許其他類別修改 (可在屬性面板更改數值)
     2.私有 private - 禁止其他類別存取 (不可在屬性面板更改數值)
     Unity 以屬性面板資料為主
     欄位屬性 : 輔助欄位資料
     欄位屬性語法 : [屬性名稱(屬性值)]
     */
    //Unity資料類型
    //顏色
    public Color color;
    public Color colorW = Color.white;
    public Color c1 = new Color(1f, 0.5f, 0);
    public Color acolor = new Color(1f,0.5f,0,0.5f);

    //座標 Vector 2-4
    public Vector2 v2;
    public Vector2 v2R = Vector2.right;
    public Vector2 v2U = Vector2.up;
    public Vector2 V2One = Vector2.one;
    public Vector2 v2Cus = new Vector2(2.5f, 50f);
    public Vector3 v3 = new Vector3(1, 2, 3);
    public Vector3 v3F = Vector3.forward;
    public Vector4 v4 = new Vector4(1, 2, 3, 4);
    //列舉 enum
    public KeyCode keys;
    public KeyCode move = KeyCode.W;
    public KeyCode jump = KeyCode.Space;

    //遊戲資料類型
    public AudioClip sound;
    public VideoClip movie;
    public Sprite sprite;
    public Texture2D tex;
    public Material mat;

    #endregion

    #region 屬性 Property

    #endregion

    #region 方法 Method

    #endregion

    #region 事件 Event

    #endregion
    [Header("移動速度"),Tooltip("用來調整角色移動速度"),Range(1,50)]
    public float speed = 10.5f;


}
