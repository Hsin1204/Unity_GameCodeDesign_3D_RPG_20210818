using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API_NonStatic : MonoBehaviour
{
    public Transform tra1; //修飾詞 要存取非靜態的類別 欄位名稱
    public Camera cam;
    public Light lig;
    // Start is called before the first frame update
    void Start()
    {
        #region 非靜態屬性
        /*與靜態差異
         1.需要實體物件
         2.取得實體物件 - 定義欄位並將要存取的物件存入欄位
         取得 Get
         語法 : 欄位名稱.非靜態屬性
         */
        print("攝影機座標 : " + tra1.position);
        print("攝影機座標 : " + cam.depth);

        //設定 Set
        //語法 : 欄位名稱.非靜態屬性 指定 值;
        tra1.position = new Vector3(99, 99, 99);
        cam.depth = 7;
        #endregion

        #region 非靜態方法
        //呼叫
        //語法 : 
        //欄位.非靜態方法名稱(對應引數);
        lig.Reset();
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
