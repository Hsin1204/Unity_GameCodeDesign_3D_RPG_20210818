using UnityEngine;

namespace Player
{
    ///<summary>
    ///第三人稱攝影機系統
    ///追蹤指定目標
    ///並且可以左右、有限制的tile
    ///</summary>
    public class Script_PlayerCam : MonoBehaviour
    {
        #region 欄位
        [Header("目標物件")]
        public Transform target;

        [Header("追蹤速度"),Range(0,100)]
        public float trackSpeed = 1.5f;

        [Header("左右旋轉速度"),Range(0,100)]
        public float turnSpeed_Horizontal = 5;

        [Header("上下旋轉速度"), Range(0, 100)]
        public float turnSpeed_Vertical = 5;

        #endregion

        #region 屬性
        //取得滑鼠水平座標
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        //取得滑鼠垂直座標
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region 事件
        #endregion

        #region 方法
        private void LateUpdate() //這會在 Update與Fixed Update後執行
        {
            TrackTarget();
            RotateCam();
            

        }
        private void TrackTarget()
        {
            Vector3 posTarget = target.position; // 取得目標座標
            Vector3 posCam = transform.position; //取得攝影機座標

            //如果沒有乘上一個frame的執行時間，在不同的設備間可能會產生很大的速度差異
            posCam = Vector3.Lerp(posCam, posTarget, trackSpeed * Time.deltaTime); //攝影機座標 = 插植
            
            transform.position = posCam; //將新的攝影機座標賦值回去
        }    
        private void RotateCam()
        {
            transform.Rotate(
                inputMouseY *turnSpeed_Vertical*Time.deltaTime,
                inputMouseX *turnSpeed_Horizontal*Time.deltaTime,
                0);
        }

        #endregion

    }
}
