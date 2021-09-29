using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API_NonStatic_Pracitce : MonoBehaviour
{
    public Camera cam;
    public SpriteRenderer sr;
    public SpriteRenderer sr_rotate, sr_fly;
    Rigidbody2D fly_rigid;
    // Start is called before the first frame update
    void Start()
    {

        print("��v���`�� : " + cam.depth);
        print("�Ϥ��C��" + sr.color);

        //�]�w��v���I���C��
        cam.backgroundColor = Random.ColorHSV();

        //�Ϥ��W�U½��
        sr.flipY = true;


        fly_rigid = sr_fly.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //�Ĥ@�i�Ϥ�����
        sr_rotate.transform.Rotate(Vector3.forward, 0.1f);

        //�ĤG�i�Ϥ����W��
        fly_rigid.AddForce(Vector2.up * 5f);
    }
}
