using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_AttackSystem : MonoBehaviour
{
    #region 欄位 : 公開
    [Header("攻擊力"), Range(0, 500)]
    public float attack = 20;
    [Header("攻擊冷卻時間"), Range(0, 5)]
    public float attack_Time = 1.3f;
    [Header("延遲傳送傷害時間"), Range(0, 3)]
    public float delaySend = 0.2f;
    [Header("攻擊區域尺寸與位移")]
    public Vector3 v3AttackOffest;
    public Vector3 v3AttackRegion;
    [Header("攻擊動畫參數")]
    public string paraAttack = "isAttackLayer";
    public string paraWalk = "isWalking";
    [Header("攻擊事件")]
    public UnityEvent onAttack;
    [Header("攻擊圖層遮色片")]
    public AvatarMask attackMask;
    
    #endregion

    #region 欄位 : 私有
    private Animator ani;
    private bool isAttack;
    #endregion

    #region 屬性 : 私有
    private bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }
    #endregion

    #region 事件
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
    }
    #endregion

    #region 繪製圖形
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0f, 0f, 0.5f);
        Gizmos.matrix = Matrix4x4.TRS(
            transform.position +
            transform.right * v3AttackOffest.x +
            transform.up * v3AttackOffest.y +
            transform.forward * v3AttackOffest.z,
            transform.rotation, transform.localScale);
        Gizmos.DrawCube(Vector3.zero, v3AttackRegion);
    }
    #endregion

    #region 方法 : 私有
    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if(keyAttack && !isAttack)
        {
            #region 攻擊圖層遮色片處理
            bool isWalk = ani.GetBool(paraWalk);
            attackMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftLeg, !isWalk);
            attackMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightLeg, !isWalk);
            attackMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.LeftFootIK, !isWalk);
            attackMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.RightFootIK, !isWalk);
            attackMask.SetHumanoidBodyPartActive(AvatarMaskBodyPart.Root, !isWalk);
            #endregion
            onAttack.Invoke();
            isAttack = true;
            ani.SetTrigger(paraAttack);
            StartCoroutine(DelaySend());
        }
    }
    private IEnumerator DelaySend()
    {
        yield return new WaitForSeconds(delaySend);

        Collider[] hits = Physics.OverlapBox(
            transform.position +
            transform.right * v3AttackOffest.x +
            transform.up * v3AttackOffest.y +
            transform.forward * v3AttackOffest.z,
            v3AttackRegion / 2, Quaternion.identity, 1 << 7
            );
        if(hits.Length > 0)
        {
            hits[0].GetComponent<Script_HurtSystem>().Hurt(attack);
        }
        float waitToNextAtt = attack_Time - delaySend;
        yield return new WaitForSeconds(waitToNextAtt);
        isAttack = false;
    }
    #endregion
}
