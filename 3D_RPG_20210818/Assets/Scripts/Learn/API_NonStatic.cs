using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API_NonStatic : MonoBehaviour
{
    public Transform tra1; //�׹��� �n�s���D�R�A�����O ���W��
    public Camera cam;
    public Light lig;
    // Start is called before the first frame update
    void Start()
    {
        #region �D�R�A�ݩ�
        /*�P�R�A�t��
         1.�ݭn���骫��
         2.���o���骫�� - �w�q���ñN�n�s��������s�J���
         ���o Get
         �y�k : ���W��.�D�R�A�ݩ�
         */
        print("��v���y�� : " + tra1.position);
        print("��v���y�� : " + cam.depth);

        //�]�w Set
        //�y�k : ���W��.�D�R�A�ݩ� ���w ��;
        tra1.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region �D�R�A��k
        //�I�s
        //�y�k : 
        //���.�D�R�A��k�W��(�����޼�);
        lig.Reset();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
