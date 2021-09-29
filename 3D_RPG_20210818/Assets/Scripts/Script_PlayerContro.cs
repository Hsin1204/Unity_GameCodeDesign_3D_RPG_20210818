using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerContro : MonoBehaviour
{

    [Header("�O�_�b�a�O�W"), Tooltip("�����O�_�b�a�O�W")]
    public bool isOnFloor = false;
    [Range(0, 3)]
    public float checkfloorRadius = 0.2f;

    public Vector3 floorOffset;

    [Header("���Ⲿ�ʳt��")]
    public float speed;
    Rigidbody rigid;


    private void Start()
    {
        
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    /// <summary>
    /// ���ʳt��
    /// </summary>
    /// <param name="speed"></param>
    private void movement(float speed)
    {
        //�n����Animator��Apply Root Motion
        //Rigidbody.velocity = �T���V�q - �[�t�ץΨӱ������T�Ӷb�V���B�ʳt��
        //�e��*��J��*���ʳt��
        //�ϥΫe�ᥪ�k�b���B�ʨåB�O���쥻���a�ߤޤO
        rigid.velocity =
            Vector3.forward * moveInput("Vertical") * speed +
            Vector3.right * moveInput("Horizontal") * speed +
            Vector3.up * rigid.velocity.y;//�o�q�O��쥻���a�ߤޤO�[�^�h
    }
    /// <summary>
    /// ���ʿ�J
    /// </summary>
    /// <param name="axisName">�n���o���b�V�W��</param>
    /// <returns>���ʫ����</returns>
    private float moveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }
    /// <summary>
    /// �ˬd�a�O
    /// </summary>
    /// <returns></returns>
    private bool checkFloor()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position +
             transform.right * floorOffset.x +
             transform.up * floorOffset.y +
             transform.forward * floorOffset.z,
             checkfloorRadius,1 << 3);

        //print("�I�쪺�Ĥ@�Ӫ���" + hits[0].name);

        //�Ǧ^ �I���}�C�ƶq > 0 �N�Ǧ^true
        return hits.Length >= 1;
    }
    /// <summary>
    /// ���D
    /// </summary>
    private void jump()
    {
        //print("�O�_�b�a���W" + checkFloor());

    }
    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void refreshAni()
    {

    }

    private void Update()
    {
        jump();
        checkFloor();
    }
    //�T�w0.02�����@��(�ϥλ�í�w��s���\��W)
    //�q�`�ΨӳB�z���z�欰
    private void FixedUpdate()
    {
        movement(speed);

       
    }

    //ø�s�ϥܨƥ�
    //�bUnity Editor��ø�s�ϥܻ��U�}�k,�o����|�۰�����
    private void OnDrawGizmos()
    {
        
        //1.���w�C��
        //2.ø�s�ϧ�
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, checkfloorRadius);
    }


}
