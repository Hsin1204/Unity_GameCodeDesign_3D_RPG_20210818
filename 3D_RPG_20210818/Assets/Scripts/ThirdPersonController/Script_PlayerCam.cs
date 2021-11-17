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

        [Header("�W�U���઺����")]
        public Vector2 limitAngleX = new Vector2(0, 0);

        [Header("����e��v���W�U���୭��")]
        public Vector2 limitAngleXFromTar = new Vector2(-0.2f, 0);


        /// <summary>
        /// ��v���e��y��
        /// </summary>
        private Vector3 _posForward;

        /// <summary>
        /// �e�誺����
        /// </summary>
        private float forwardLen = 3f;
        #endregion

        #region �ݩ�
        //���o�ƹ������y��
        private float inputMouseX { get => Input.GetAxis("Mouse X"); }
        //���o�ƹ������y��
        private float inputMouseY { get => Input.GetAxis("Mouse Y"); }

        /// <summary>
        /// ��v���e��y��
        /// </summary>
        public Vector3 posForward
        {
            get
            {
                _posForward = transform.position + transform.forward * forwardLen;
                _posForward.y = target.position.y;
                return _posForward;
            }
        }
        #endregion

        #region �ƥ�
        #endregion

        #region ��k
        private void LateUpdate() //�o�|�b Update�PFixed Update�����
        {
            TrackTarget();
            RotateCam();
            LimitAngleXandZfromTar();
            FreezeAngleZ();

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 1, 0.3f);
            //�e��y�� = ������y�� + ������e�� * ����
            _posForward = transform.position + transform.forward * forwardLen;
            //�e��y��.y = �ؼ�.�y��y(���e��y�Ъ����׻P�ؼЬۦP)
            _posForward.y = target.position.y;
            Gizmos.DrawSphere(_posForward, 0.15f);
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
        private void LimitAngleXandZfromTar()
        {
            Quaternion angle = transform.rotation;
            angle.x = Mathf.Clamp(angle.x, limitAngleX.x, limitAngleX.y);
            angle.z = Mathf.Clamp(angle.z, limitAngleXFromTar.x, limitAngleXFromTar.y);//�����Z�b - ��v���b�ؼЫe��
            transform.rotation = angle;
        }
        
        private void FreezeAngleZ()
        {
            Vector3 angle = transform.eulerAngles;
            angle.z = 0;
            transform.eulerAngles = angle;
        }    
        #endregion

    }
}
