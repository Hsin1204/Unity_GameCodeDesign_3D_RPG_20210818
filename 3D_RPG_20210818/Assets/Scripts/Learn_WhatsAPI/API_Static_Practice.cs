using UnityEngine;

public class API_Static_Practice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 取得攝影機數量
        int cam_count = Camera.allCamerasCount;
        print("目前共有 " + cam_count + " 台攝影機");

        //2D重力大小
        Vector2 gravity_2d = Physics2D.gravity;
        print("2D重力為 " + gravity_2d);

        //圓周率
        float pi = Mathf.PI;
        print("圓周率為 " + pi);

        //設定重力大小
        Physics2D.gravity = new Vector2(0.0f, -20f);
        print("新的重力為 : " + Physics2D.gravity);

        //時間大小設定為0.5
        Time.timeScale = 0.5f;

        //對9.999去小數點
        float new_nine = Mathf.Floor(9.999f);
        print("去小數點後為 : " + new_nine);

        //取得兩點間的距離
        Vector3 a = new Vector3(1,1,1);
        Vector3 b = new Vector3(22, 22, 22);
        float distance = Vector3.Distance(a, b);
        print("兩點距離為為 : " + distance);

        //開啟網址
        Application.OpenURL("https://unity.com/");
    }

    // Update is called once per frame
    void Update()
    {
        
        //是否輸入任意鍵
        print("目前是否有按下按鍵" + Input.anyKey);

        //經過時間
        print("經過時間 : " + Time.timeSinceLevelLoad);
        
        //是否按下空白鍵
        print("是否按下空白鍵: " + Input.GetKey(KeyCode.Space));

        




    }
}
