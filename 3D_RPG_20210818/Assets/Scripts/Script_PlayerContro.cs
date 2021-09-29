using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerContro : MonoBehaviour
{

    [Header("�O�_�b�a�O�W"), Tooltip("�����O�_�b�a�O�W")]
    public bool isOnFloor = false;

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
        return false;
    }
    /// <summary>
    /// ���D
    /// </summary>
    private void jump()
    {

    }
    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void refreshAni()
    {

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
        Vector3 playerPosition = gameObject.transform.position;
        //1.���w�C��
        //2.ø�s�ϧ�
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
        Gizmos.DrawSphere(playerPosition, 1);
    }


}
