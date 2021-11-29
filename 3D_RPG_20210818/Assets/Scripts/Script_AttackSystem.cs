using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_AttackSystem : MonoBehaviour
{
    #region ��� : ���}
    [Header("�����O"), Range(0, 500)]
    public float attack = 20;
    [Header("�����N�o�ɶ�"), Range(0, 5)]
    public float attack_Time = 1.3f;
    [Header("����ǰe�ˮ`�ɶ�"), Range(0, 3)]
    public float delaySend = 0.2f;
    [Header("�����ϰ�ؤo�P�첾")]
    public Vector3 v3AttackOffest;
    public Vector3 v3AttackRegion;
    [Header("�����ʵe�Ѽ�")]
    public string paraAttack = "isAttackLayer";
    public string paraWalk = "isWalking";
    [Header("�����ƥ�")]
    public UnityEvent onAttack;
    [Header("�����ϼh�B���")]
    public AvatarMask attackMask;
    
    #endregion

    #region ��� : �p��
    private Animator ani;
    private bool isAttack;
    #endregion

    #region �ݩ� : �p��
    private bool keyAttack { get => Input.GetKeyDown(KeyCode.Mouse0); }
    #endregion

    #region �ƥ�
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
    }
    #endregion

    #region ø�s�ϧ�
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

    #region ��k : �p��
    /// <summary>
    /// ����
    /// </summary>
    private void Attack()
    {
        if(keyAttack && !isAttack)
        {
            #region �����ϼh�B����B�z
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
