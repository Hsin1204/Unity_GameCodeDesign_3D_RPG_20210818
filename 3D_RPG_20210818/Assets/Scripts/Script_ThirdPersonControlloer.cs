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

    //遊戲類型

    public Transform trans;
    public Animator newAnimator;
    public Animation newAni;
    public Light lit;
    public Camera cam;
    */
    [Header("移動速度"), Range(0, 500)]
    public float speed = 10.5f;

    [Header("跳躍高度"), Range(0, 1000)]
    public float jumpHeight = 100f;

    [Header("是否在地板上"), Tooltip("偵測是否在地板上")]
    public bool isOnFloor = false;

    public Vector3 floorMovement;

    [Range(0, 3)]
    public float floorRadius = 0.2f;

    [Header("音效")]
    public AudioClip soundJump;

    public AudioClip soundLanding;

    [Header("動畫參數")]
    public string walk = "isWaking";
    public string run = "isRunnging";
    public string injury = "isInjury";
    public string dead = "isDead";

    private AudioSource aSource;
    private Rigidbody rigid;
    private Animator cus_Animator;

    public GameObject PlayerObj;
    #endregion

    #region 屬性 Property
    //不會出現在面板上
    //儲存資料，與欄位相同
    //差異在於可以設定存取權限 Get Set
    //屬性語法 : 修飾詞 資料類型 屬性名稱 {get;set}
    public int readAndWrite { get; set; }

    /*唯寫屬性:禁止，必須要有get
      public int write{set;}
      value 指的是後面賦予的值
    */
    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            _hp = value;
        }
    }
    #endregion
  
    #region 方法 Method
    /*定義與實作較複雜程式的區塊、功能
      方法語法 : 修飾詞 傳回資料類型 方法名稱(參數(可用多個，用","分開*建議不要超過三個))
                {程式區塊}
      常用傳回類型:無傳回 void - 此方法沒有傳回資料
      自動排版 : Ctrl+K+D
      自訂方法 : 名稱顏色為淡黃色 - 沒有被呼叫
                名稱顏色為亮黃色 - 有被呼叫
      自訂方法必須被呼叫才會被執行
     */
    private void Test()
    {
        print("我是自訂方法");
    }
    private int Jump()
    {
        return 999;
    }
    private void damage(float d)
    {
        print("傷害" + d);
    }
    //選填式參數一定要在最右邊
    private void effectAttack(int dama,string attaName = "Dust",string sound = "嘎嘎嘎")
    {
        print("damge = " + dama);
        print(attaName);
        print("音效 = " + sound);
    }
    private float BMI(float w,float h,string name = "test" )
    {
        print(name + "的BMI");
        if(h>5)
        {
            h = h / 100;
        }
        return (w / (h * h));
    }

    #region 事件 Event
    //特定時間點會執行的方法，程式的入口Start等於Console Main
    //開始事件 : 遊戲開始時執行一次 - 處理初始化、取得資料等等

    private void Start()
    {
        /* print("欄位資料 - 移動速度" + speed);
         print("欄位資料 - 讀寫屬性" + readAndWrite);
         speed = 20.5f;
         readAndWrite = 90;
         print("修改後的資料");
         print("欄位資料 - 移動速度" + speed);
         print("欄位資料 - 讀寫屬性" + readAndWrite);*/
        /*Test();
        int j = Jump();
        print(j);
        damage(500f);
        effectAttack(500,sound:"咻咻咻");*/
        #region
        /*print("hp = " + hp);
        hp = 100;
        print("hp = " + hp);*/
        #endregion
        print(BMI(50, 155));
        #endregion

        //取得元件的方式
        //1.物件欄位的名稱.取得元件(類型(元件類型)) 當作 元件類型
        aSource = PlayerObj.GetComponent(typeof(AudioSource)) as AudioSource;
        //2.此腳本遊戲物件.取得元件<泛型>();
        rigid = gameObject.GetComponent<Rigidbody>();
        //3.取得元件<泛型>();
        //類別可以使用繼承類別(父類別)的成員,公開或保護 欄位、屬性與方法;
        cus_Animator = GetComponent<Animator>();
    }
    //更新事件 : 執行次數以FPS為準
    //處理持續性運動，移動物件，監聽玩家輸入按鍵
    private void Update()
    {
        
    }
    #endregion
    




}
