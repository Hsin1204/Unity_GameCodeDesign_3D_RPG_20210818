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

    //�C������

    public Transform trans;
    public Animator newAnimator;
    public Animation newAni;
    public Light lit;
    public Camera cam;
    */
    [Header("���ʳt��"), Range(0, 500)]
    public float speed = 10.5f;

    [Header("���D����"), Range(0, 1000)]
    public float jumpHeight = 100f;

    [Header("�O�_�b�a�O�W"), Tooltip("�����O�_�b�a�O�W")]
    public bool isOnFloor = false;

    public Vector3 floorMovement;

    [Range(0, 3)]
    public float floorRadius = 0.2f;

    [Header("����")]
    public AudioClip soundJump;

    public AudioClip soundLanding;

    [Header("�ʵe�Ѽ�")]
    public string walk = "isWaking";
    public string run = "isRunnging";
    public string injury = "isInjury";
    public string dead = "isDead";

    private AudioSource aSource;
    private Rigidbody rigid;
    private Animator cus_Animator;

    public GameObject PlayerObj;
    #endregion

    #region �ݩ� Property
    //���|�X�{�b���O�W
    //�x�s��ơA�P���ۦP
    //�t���b��i�H�]�w�s���v�� Get Set
    //�ݩʻy�k : �׹��� ������� �ݩʦW�� {get;set}
    public int readAndWrite { get; set; }

    /*�߼g�ݩ�:�T��A�����n��get
      public int write{set;}
      value �����O�᭱�ᤩ����
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
  
    #region ��k Method
    /*�w�q�P��@�������{�����϶��B�\��
      ��k�y�k : �׹��� �Ǧ^������� ��k�W��(�Ѽ�(�i�Φh�ӡA��","���}*��ĳ���n�W�L�T��))
                {�{���϶�}
      �`�ζǦ^����:�L�Ǧ^ void - ����k�S���Ǧ^���
      �۰ʱƪ� : Ctrl+K+D
      �ۭq��k : �W���C�⬰�H���� - �S���Q�I�s
                �W���C�⬰�G���� - ���Q�I�s
      �ۭq��k�����Q�I�s�~�|�Q����
     */
    private void Test()
    {
        print("�ڬO�ۭq��k");
    }
    private int Jump()
    {
        return 999;
    }
    private void damage(float d)
    {
        print("�ˮ`" + d);
    }
    //��񦡰ѼƤ@�w�n�b�̥k��
    private void effectAttack(int dama,string attaName = "Dust",string sound = "�ǹǹ�")
    {
        print("damge = " + dama);
        print(attaName);
        print("���� = " + sound);
    }
    private float BMI(float w,float h,string name = "test" )
    {
        print(name + "��BMI");
        if(h>5)
        {
            h = h / 100;
        }
        return (w / (h * h));
    }

    #region �ƥ� Event
    //�S�w�ɶ��I�|���檺��k�A�{�����J�fStart����Console Main
    //�}�l�ƥ� : �C���}�l�ɰ���@�� - �B�z��l�ơB���o��Ƶ���

    private void Start()
    {
        /* print("����� - ���ʳt��" + speed);
         print("����� - Ū�g�ݩ�" + readAndWrite);
         speed = 20.5f;
         readAndWrite = 90;
         print("�ק�᪺���");
         print("����� - ���ʳt��" + speed);
         print("����� - Ū�g�ݩ�" + readAndWrite);*/
        /*Test();
        int j = Jump();
        print(j);
        damage(500f);
        effectAttack(500,sound:"������");*/
        #region
        /*print("hp = " + hp);
        hp = 100;
        print("hp = " + hp);*/
        #endregion
        print(BMI(50, 155));
        #endregion

        //���o���󪺤覡
        //1.������쪺�W��.���o����(����(��������)) ��@ ��������
        aSource = PlayerObj.GetComponent(typeof(AudioSource)) as AudioSource;
        //2.���}���C������.���o����<�x��>();
        rigid = gameObject.GetComponent<Rigidbody>();
        //3.���o����<�x��>();
        //���O�i�H�ϥ��~�����O(�����O)������,���}�ΫO�@ ���B�ݩʻP��k;
        cus_Animator = GetComponent<Animator>();
    }
    //��s�ƥ� : ���榸�ƥHFPS����
    //�B�z����ʹB�ʡA���ʪ���A��ť���a��J����
    private void Update()
    {
        
    }
    #endregion
    




}
