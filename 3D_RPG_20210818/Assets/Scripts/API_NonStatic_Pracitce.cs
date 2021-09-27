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

        print("攝影機深度 : " + cam.depth);
        print("圖片顏色" + sr.color);

        //設定攝影機背景顏色
        cam.backgroundColor = Random.ColorHSV();

        //圖片上下翻轉
        sr.flipY = true;


        fly_rigid = sr_fly.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //第一張圖片旋轉
        sr_rotate.transform.Rotate(Vector3.forward, 0.1f);

        //第二張圖片往上飛
        fly_rigid.AddForce(Vector2.up * 5f);
    }
}
