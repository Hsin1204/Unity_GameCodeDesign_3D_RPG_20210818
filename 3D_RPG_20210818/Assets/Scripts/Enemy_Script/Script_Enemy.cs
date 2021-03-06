using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Script_Enemy : MonoBehaviour
    {
        #region 欄位 :公開
        [Header("移動速度"), Range(0, 20)]
        public float speed = 2.5f;
        [Header("攻擊力"), Range(0, 200)]
        public float attack = 35;
        [Header("範圍:追蹤與攻擊")]
        public float attackRange = 5;
        public float trackRange = 15;
        [Header("等待隨機秒數")]
        public Vector2 v2RandomWait = new Vector2(1f, 5f);
        [Header("走路隨機秒數")]
        public Vector2 v2RandomWalk = new Vector2(5f, 10f);
        [Header("AI可行走範圍")]
        public Vector3 v3Random = new Vector3(1f, 1f, 2f);
        [Header("攻擊區域位移與尺寸")]
        public Vector3 v3AttackOffset;
        public Vector3 v3AttackSize = Vector3.one; 
        [Header("攻擊冷卻時間")]
        public float attackTime;
        [Header("攻擊延遲傳送傷害時間")]
        public float delaySendDamage = 0.5f;
        [Header("面向玩家的速度")]
        public float turnSpeed = 2f;
        [Header("NPC名字")]
        public string npcName = "free_male_1";
       
        #endregion

        #region 欄位 :私人
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
        /// 繪製追蹤、攻擊範圍
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

            #region 攻擊碰撞判定區域
            Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
            //繪製方形，需要跟著角色旋轉時使用matrix指定座標角度與尺寸
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
        #region 事件
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
            playerPos = GameObject.Find(playerName).transform;
            nma.SetDestination(v3WalkFin);
            nma.speed = speed;

            hurtSys = GetComponent<Script_HurtSystem>();
            npc = GameObject.Find(npcName).GetComponent<Script_NPC>();

            //受傷系統 -- 死亡事件觸發時 請NPC更新數量
            //AddListener(方法) 添加監聽器(方法)
            hurtSys.onDead.AddListener(npc.UpdateMission);
        }
        private void Update()
        {
            StateManager();
        }
        
        #endregion

        #region 方法:私人
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
            nma.SetDestination(v3WalkFin);                                                 //代理器,設定目的地(座標)
            ani.SetBool(paraIdleWalk, nma.remainingDistance > 0.1f);                       // 走路動畫 -- 離目的地的距離大於0.1時走路
            if (isWalk) return;
            isWalk = true;
            NavMeshHit hit;                                                                //導覽網格碰撞 --儲存網格碰撞狀資訊
            NavMesh.SamplePosition(v3RandomWalk, out hit, trackRange, NavMesh.AllAreas);   //導覽網格.取得座標(隨機座標,碰撞資訊,半徑,區域) -網格內可行走的座標
            v3WalkFin = hit.position;                                                      //最終座標 = 碰撞資訊的座標

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
        /// 攻擊玩家
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

