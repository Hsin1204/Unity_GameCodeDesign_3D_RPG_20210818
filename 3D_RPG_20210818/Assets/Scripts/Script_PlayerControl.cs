using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player 
{
    public class Script_PlayerControl : MonoBehaviour
    {
        #region ���
        [Header("�O�_�b�a�O�W"), Tooltip("�����O�_�b�a�O�W")]
        public bool isLanding = false;
        [Range(0, 3)]
        public float checkfloorRadius = 0.2f;
        public Vector3 floorOffset;

        [Header("����")]
        public AudioClip soundJump;
        public AudioClip soundLanding;

        [Header("���Ⲿ�ʳt��")]
        public float speed;

        [Header("���D����"), Range(0, 1000)]
        public float jumpHeight = 100f;

        [Header("�ʵe�Ѽ�")]
        public string animP_Jumpping = "isJumpping";
        public string animP_Landing = "isLanding";

        [Header("���V�t��"), Range(0, 50)]
        public float speedLookAt = 2;

        //C# 6.0 �s���l �i�H�ϥ�lambda �B��l
        //�y�k : get => {�{���϶�}
        private bool keyJump { get => Input.GetKeyDown(KeyCode.Space); }

        private AudioSource aud;
        private Rigidbody rigid;
        private Animator ani;

        private Script_PlayerCam playerCam;
        #endregion


        #region ��k
        private void Start()
        {
            aud = gameObject.GetComponent<AudioSource>();
            rigid = gameObject.GetComponent<Rigidbody>();
            ani = gameObject.GetComponent<Animator>();

            playerCam = FindObjectOfType<Script_PlayerCam>();

            
        }
        /// <summary>
        /// ���ʳt��
        /// </summary>
        /// <param name="speed"></param>
        private void movement(float speed)
        {
            //�n����Animator��Apply Root Motion
            //Rigidbody.velocity = �T���V�q - �[�t�ץΨӱ������T�Ӷb�V���B�ʳt��
            //�e��*��J��*���ʳt��
            //�ϥΫe�ᥪ�k�b���B�ʨåB�O���쥻���a�ߤޤO
            //Vector3.forward �@�ɮy�Ъ��e��
            //transform.forward �ϰ�y�Ъ��e��
            rigid.velocity =
                transform.forward * moveInput("Vertical") * speed +
                transform.right * moveInput("Horizontal") * speed +
                Vector3.up * rigid.velocity.y;//�o�q�O��쥻���a�ߤޤO�[�^�h
        }
        /// <summary>
        /// ���ʿ�J
        /// </summary>
        /// <param name="axisName">�n���o���b�V�W��</param>
        /// <returns>���ʫ����</returns>
        private float moveInput(string axisName)
        {
            return Input.GetAxis(axisName);
        }
        /// <summary>
        /// �ˬd�a�O
        /// </summary>
        /// <returns></returns>
        private bool checkFloor()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position +
                 transform.right * floorOffset.x +
                 transform.up * floorOffset.y +
                 transform.forward * floorOffset.z,
                 checkfloorRadius, 1 << 3);

            //print("�I�쪺�Ĥ@�Ӫ���" + hits[0].name);

            isLanding = hits.Length > 0;

            if (!isLanding && hits.Length > 0)
            { aud.PlayOneShot(soundLanding, Random.Range(50f, 150f)); }

            //�Ǧ^ �I���}�C�ƶq > 0 �N�Ǧ^true
            return hits.Length >= 1;
        }
        /// <summary>
        /// ���D
        /// </summary>
        private void jump()
        {
            //print("�O�_�b�a���W" + checkFloor());

            if (checkFloor() == true && Input.GetKeyDown(KeyCode.Space))
            {

                rigid.AddForce(transform.up * jumpHeight);

                aud.PlayOneShot(soundJump, Random.Range(0.5f, 1.0f));
            }

        }
        /// <summary>
        /// ��s�ʵe
        /// </summary>
        private void UpdateAni()
        {
            ani.SetBool("isWalking", moveInput("Vertical") != 0 || moveInput("Horizontal") != 0);
            ani.SetBool(animP_Landing, isLanding);
            if (keyJump == true)
            {
                ani.SetTrigger(animP_Jumpping);
            }
            /* if (Input.GetAxis("Vertical") != 0)
             { ani.SetBool("isWalking", true); }
             else
             { ani.SetBool("isWalking", false); }*/

        }

        /// <summary>
        /// ���V��v���e���m
        /// </summary>
        private void LookForward()
        {
            //�����b�V������ȫ�j��0.1�N�B�z���V
            if(Mathf.Abs(moveInput("Vertical")) >0.1f)
            {
                //���o�e�訤�� = �|��.���V����(�e��y��-�����y��)
                Quaternion angle = Quaternion.LookRotation(playerCam.posForward - transform.position);
                //�����󪺨��� = �|��.�t��
                transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime*speedLookAt);
            }
           
        }
        private void Update()
        {
            UpdateAni();
            jump();
            LookForward();
        }
        //�T�w0.02�����@��(�ϥλ�í�w��s���\��W)
        //�q�`�ΨӳB�z���z�欰
        private void FixedUpdate()
        {
            movement(speed);


        }

        //ø�s�ϥܨƥ�
        //�bUnity Editor��ø�s�ϥܻ��U�}�k,�o����|�۰�����
        private void OnDrawGizmos()
        {

            //1.���w�C��
            //2.ø�s�ϧ�
            Gizmos.color = new Color(1, 0, 0.2f, 0.3f);
            Gizmos.DrawSphere(transform.position, checkfloorRadius);
        }
        #endregion


    }

}
