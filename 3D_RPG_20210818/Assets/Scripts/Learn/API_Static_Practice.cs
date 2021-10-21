using UnityEngine;

public class API_Static_Practice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ���o��v���ƶq
        int cam_count = Camera.allCamerasCount;
        print("�ثe�@�� " + cam_count + " �x��v��");

        //2D���O�j�p
        Vector2 gravity_2d = Physics2D.gravity;
        print("2D���O�� " + gravity_2d);

        //��P�v
        float pi = Mathf.PI;
        print("��P�v�� " + pi);

        //�]�w���O�j�p
        Physics2D.gravity = new Vector2(0.0f, -20f);
        print("�s�����O�� : " + Physics2D.gravity);

        //�ɶ��j�p�]�w��0.5
        Time.timeScale = 0.5f;

        //��9.999�h�p���I
        float new_nine = Mathf.Floor(9.999f);
        print("�h�p���I�ᬰ : " + new_nine);

        //���o���I�����Z��
        Vector3 a = new Vector3(1,1,1);
        Vector3 b = new Vector3(22, 22, 22);
        float distance = Vector3.Distance(a, b);
        print("���I�Z������ : " + distance);

        //�}�Һ��}
        Application.OpenURL("https://unity.com/");
    }

    // Update is called once per frame
    void Update()
    {
        
        //�O�_��J���N��
        print("�ثe�O�_�����U����" + Input.anyKey);

        //�g�L�ɶ�
        print("�g�L�ɶ� : " + Time.timeSinceLevelLoad);
        
        //�O�_���U�ť���
        print("�O�_���U�ť���: " + Input.GetKey(KeyCode.Space));

        




    }
}
