
using UnityEngine;

public class Script_LearnAPI_Static: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        #region
        //取得
        //語法:
        //類別.靜態屬性
        float rf = Random.value;
        print("取得隨機值: " + rf.ToString("0.0000"));

        //設定 Set
        //語法:
        //類別.靜態屬性 指定 值;
        //Cursor.visible = false;
        #endregion

        #region
        // 呼叫,參數、傳回
        //簽章: 參數、傳回
        //語法:
        //類別名稱.靜態方法(引數)

        float range = Random.Range(10.5f, 20.9f);
        print("取得具有範圍的隨機浮點數值: " + range.ToString("0.0000"));

        //*API 說明很重要 : 使用整數時不包含最大值
        int rangeInt = Random.Range(1, 5);
        print("取得具有範圍的隨機整數值: " + rangeInt);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region
        //print("時間 : "+Time.timeSinceLevelLoad);
        #endregion

        #region
        float h = Input.GetAxis("Horizontal");
        print("輸入軸向為 : " + h);
        #endregion
    }
}
