using UnityEngine;

namespace Player
{
    ///<summary>
    ///�ĤT�H����v���t��
    ///�l�ܫ��w�ؼ�
    ///�åB�i�H���k�B�����tile
    ///</summary>
    public class Script_PlayerCam : MonoBehaviour
    {
        #region ���
        [Header("�ؼЪ���")]
        public Transform target;

        [Header("�l�ܳt��"),Range(0,100)]
        public float trackSpeed = 1.5f;

        [Header("���k����t��"),Range(0,100)]
        public float turnSpeed_Horizontal = 5;

        [Header("�W�U����t��"), Range(0, 100)]
        public float turnSpeed_Vertical = 5;

        #endregion

        #region �ݩ�
        //���o�ƹ������y��
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        //���o�ƹ������y��
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }
        #endregion

        #region �ƥ�
        #endregion

        #region ��k
        private void LateUpdate() //�o�|�b Update�PFixed Update�����
        {
            TrackTarget();
            RotateCam();
            

        }
        private void TrackTarget()
        {
            Vector3 posTarget = target.position; // ���o�ؼЮy��
            Vector3 posCam = transform.position; //���o��v���y��

            //�p�G�S�����W�@��frame������ɶ��A�b���P���]�ƶ��i��|���ͫܤj���t�׮t��
            posCam = Vector3.Lerp(posCam, posTarget, trackSpeed * Time.deltaTime); //��v���y�� = ����
            
            transform.position = posCam; //�N�s����v���y�н�Ȧ^�h
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
