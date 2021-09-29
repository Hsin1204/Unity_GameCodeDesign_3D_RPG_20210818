using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerContro : MonoBehaviour
{

    [Header("是否在地板上"), Tooltip("偵測是否在地板上")]
    public bool isOnFloor = false;
    [Range(0, 3)]
    public float checkfloorRadius = 0.2f;

    public Vector3 floorOffset;

    [Header("角色移動速度")]
    public float speed;
    Rigidbody rigid;


    private void Start()
    {
        
        rigid = gameObject.GetComponent<Rigidbody>();
    }
    /// <summary>
    /// 移動速度
    /// </summary>
    /// <param name="speed"></param>
    private void movement(float speed)
    {
        //要取消Animator的Apply Root Motion
        //Rigidbody.velocity = 三維向量 - 加速度用來控制剛體三個軸向的運動速度
        //前方*輸入值*移動速度
        //使用前後左右軸項運動並且保持原本的地心引力
        rigid.velocity =
            Vector3.forward * moveInput("Vertical") * speed +
            Vector3.right * moveInput("Horizontal") * speed +
            Vector3.up * rigid.velocity.y;//這段是把原本的地心引力加回去
    }
    /// <summary>
    /// 移動輸入
    /// </summary>
    /// <param name="axisName">要取得的軸向名稱</param>
    /// <returns>移動按鍵值</returns>
    private float moveInput(string axisName)
    {
        return Input.GetAxis(axisName);
    }
    /// <summary>
    /// 檢查地板
    /// </summary>
    /// <returns></returns>
    private bool checkFloor()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position +
             transform.right * floorOffset.x +
             transform.up * floorOffset.y +
             transform.forward * floorOffset.z,
             checkfloorRadius,1 << 3);

        //print("碰到的第一個物件" + hits[0].name);

        //傳回 碰撞陣列數量 > 0 就傳回true
        return hits.Length >= 1;
    }
    /// <summary>
    /// 跳躍
    /// </summary>
    private void jump()
    {
        //print("是否在地面上" + checkFloor());

    }
    /// <summary>
    /// 更新動畫
    /// </summary>
    private void refreshAni()
    {

    }

    private void Update()
    {
        jump();
        checkFloor();
    }
    //固定0.02秒執行一次(使用需穩定更新的功能上)
    //通常用來處理物理行為
    private void FixedUpdate()
    {
        movement(speed);

       
    }

    //繪製圖示事件
    //在Unity Editor內繪製圖示輔助開法,發布後會自動隱藏
    private void OnDrawGizmos()
    {
        
        //1.指定顏色
        //2.繪製圖形
        Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
        Gizmos.DrawSphere(transform.position, checkfloorRadius);
    }


}
