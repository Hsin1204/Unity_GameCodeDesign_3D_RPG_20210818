
using UnityEngine;

public class Script_LearnAPI_Static: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region
        //���o
        //�y�k:
        //���O.�R�A�ݩ�
        float rf = Random.value;
        print("���o�H����: " + rf.ToString("0.0000"));

        //�]�w Set
        //�y�k:
        //���O.�R�A�ݩ� ���w ��;
        //Cursor.visible = false;
        #endregion

        #region
        // �I�s,�ѼơB�Ǧ^
        //ñ��: �ѼơB�Ǧ^
        //�y�k:
        //���O�W��.�R�A��k(�޼�)

        float range = Random.Range(10.5f, 20.9f);
        print("���o�㦳�d���H���B�I�ƭ�: " + range.ToString("0.0000"));

        //*API �����ܭ��n : �ϥξ�Ʈɤ��]�t�̤j��
        int rangeInt = Random.Range(1, 5);
        print("���o�㦳�d���H����ƭ�: " + rangeInt);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region
        //print("�ɶ� : "+Time.timeSinceLevelLoad);
        #endregion

        #region
        float h = Input.GetAxis("Horizontal");
        print("��J�b�V�� : " + h);
        #endregion
    }
}
