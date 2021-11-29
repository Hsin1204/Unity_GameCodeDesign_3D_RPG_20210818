using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Script_Enemy : MonoBehaviour
    {
        #region ��� :���}
        [Header("���ʳt��"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("�����O"), Range(0, 200)]
        public float attack = 35;
        [Header("�d��:�l�ܻP����")]
        public float attackRange = 5;
        public float trackRange = 15;
        [Header("�����H������")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("�����H������")]
        public Vector2 v2RandomWalk = new Vector2(5f, 10f);
        [Header("AI�i�樫�d��")]
        public Vector3 v3Random = new Vector3(1f, 1f, 2f);
        [Header("�����ϰ�첾�P�ؤo")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one; 
        [Header("�����N�o�ɶ�")]
        public float attackTime;
        [Header("��������ǰe�ˮ`�ɶ�")]
        public float delaySendDamage = 0.5f;
        [Header("���V���a���t��")]
        public float turnSpeed = 2f;
        [Header("NPC�W�r")]
        public string npcName = "free_male_1";
       
        #endregion

        #region ��� :�p�H
        [SerializeField]
        private EnemyState state;
        private bool isIdle;
        private bool isWalk;
        private Animator ani;
        private string paraIdleWalk = "isWalking";
        private NavMeshAgent nma;
        private Vector3 v3RandomWalk
        {
            get => Random.insideUnitSphere * trackRange + transform.position;
        }
        private Vector3 v3WalkFin;
        private bool isPlayerInRange { get => Physics.OverlapSphere(transform.position, trackRange, 1 << 6).Length > 0; }
        private string playerName = "Player";
        private Transform playerPos;
        private bool isTrack;
        private string paraAttack = "isAttack";
        private bool isAttack;
        private bool isTargetDead;

        private Script_NPC npc;
        private Script_HurtSystem hurtSys;
        #endregion

        /// <summary>
        /// ø�s�l�ܡB�����d��
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 0.3f);
            Gizmos.DrawSphere(transform.position, attackRange);

            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawSphere(transform.position, trackRange);
            if(state == EnemyState.Walk)
            {
                Gizmos.color = new Color(1f, 0.5f, 0, 0.3f);
                Gizmos.DrawSphere(v3WalkFin, 0.3f);
            }

            #region �����I���P�w�ϰ�
            Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
            //ø�s��ΡA�ݭn��ۨ������ɨϥ�matrix���w�y�Ш��׻P�ؤo
            Gizmos.matrix = Matrix4x4.TRS(
                transform.position +
                transform.right * v3AttackOffset.x +
                transform.up * v3AttackOffset.y +
                transform.forward * v3AttackOffset.z,
                transform.rotation, transform.lossyScale
                ) ;
            Gizmos.DrawCube(Vector3.zero, v3AttackSize);
            #endregion
        }
        #region �ƥ�
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            playerPos = GameObject.Find(playerName).transform;
            nma.SetDestination(v3WalkFin);
            nma.speed = speed;

            hurtSys = GetComponent<Script_HurtSystem>();
            npc = GameObject.Find(npcName).GetComponent<Script_NPC>();

            //���˨t�� -- ���`�ƥ�Ĳ�o�� ��NPC��s�ƶq
            //AddListener(��k) �K�[��ť��(��k)
            hurtSys.onDead.AddListener(npc.UpdateMission);
        }
        private void Update()
        {
            StateManager();
        }
        
        #endregion

        #region ��k:�p�H
        private void StateManager()
        {
            switch (state)
            {
                case EnemyState.Idle:
                    Idle();
                    break;
                case EnemyState.Walk:
                    Walk();
                    break;
                case EnemyState.Track:
                    Track();
                    break;
                case EnemyState.Attack:
                    Attack();
                    break;
                case EnemyState.Injured:
                    break;
                case EnemyState.Death:
                    break;
                default:
                    break;
            }
        }

        private void Idle()
        {
            
            if (!isTargetDead && isPlayerInRange)
            {
                state = EnemyState.Track;
                
            }

            if (isIdle) return;
            isIdle = true;
            
            ani.SetBool(paraIdleWalk, false);
            
            StartCoroutine(IdleWait());
            
        }
        private IEnumerator IdleWait()
        {
            
            float randomWait = Random.Range(v2RandomWait.x, v2RandomWait.y);
            yield return new WaitForSeconds(randomWait);
            
            state = EnemyState.Walk;
            
            isIdle = false;

        }

        private void Walk()
        {
            if (!isTargetDead && isPlayerInRange) state = EnemyState.Track;
            nma.SetDestination(v3WalkFin);                                                 //�N�z��,�]�w�ت��a(�y��)
            ani.SetBool(paraIdleWalk, nma.remainingDistance > 0.1f);                       // �����ʵe -- ���ت��a���Z���j��0.1�ɨ���
            if (isWalk) return;
            isWalk = true;
            NavMeshHit hit;                                                                //��������I�� --�x�s����I������T
            NavMesh.SamplePosition(v3RandomWalk, out hit, trackRange, NavMesh.AllAreas);   //��������.���o�y��(�H���y��,�I����T,�b�|,�ϰ�) -���椺�i�樫���y��
            v3WalkFin = hit.position;                                                      //�̲׮y�� = �I����T���y��

            print("I'm walking");

            StartCoroutine(WalkWait());
        }    
        private IEnumerator WalkWait()
        {
            float randomwalk = Random.Range(v2RandomWalk.x, v2RandomWalk.y);
            yield return new WaitForSeconds(randomwalk);
            
            state = EnemyState.Idle;
            print("I'm wait");
            isWalk = false;

        }
        private void Track()
        {
            if(!isTrack)
            {
                StopAllCoroutines();
            }
            isTrack = true;

            nma.isStopped = false;
            nma.SetDestination(playerPos.position);
            ani.SetBool(paraIdleWalk, true);
            
            if (nma.remainingDistance <= attackRange) state = EnemyState.Attack;
            if(nma.remainingDistance <= 0)
            {
                isTrack = false;
                isWalk = false;
                state = EnemyState.Idle;
                ani.SetBool(paraIdleWalk, false);
            }
            
               
        }
        /// <summary>
        /// �������a
        /// </summary>
        private void Attack()
        {
            
            nma.isStopped = true;
            ani.SetBool(paraIdleWalk, false);
            nma.SetDestination(playerPos.position);
            FaceToPlayer();
            if (nma.remainingDistance > attackRange) state = EnemyState.Track;

            if (isAttack) return;
            ani.SetTrigger(paraAttack);
            StartCoroutine(DelaySendDamageToTarget());
            isAttack = true;
          
        }
        private IEnumerator DelaySendDamageToTarget()
        {
            yield return new WaitForSeconds(delaySendDamage);

            Collider[] hits = Physics.OverlapBox(
              transform.position +
              transform.right * v3AttackOffset.x +
              transform.up * v3AttackOffset.y +
              transform.forward * v3AttackOffset.z,
              v3AttackSize / 2, Quaternion.identity, 1 << 6
              );
            if (hits.Length > 0)
            {
                isTargetDead = hits[0].GetComponent<Script_HurtSystem>().Hurt(attack); 
            }
            if(isTargetDead)
            {
                TargetDead();
            }
            float waitToNextAttack = attackTime - delaySendDamage;
            yield return new WaitForSeconds(waitToNextAttack);
            isAttack = false;
        }
        private void TargetDead()
        {
            state = EnemyState.Walk;
            isIdle = false;
            isWalk = false;
            nma.isStopped = false;
        }
        private void FaceToPlayer()
        {
            Quaternion angle = Quaternion.LookRotation(playerPos.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle,Time.deltaTime* turnSpeed);
            ani.SetBool(paraIdleWalk, transform.rotation != angle);

        }

        #endregion

    }
}
