using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_HurtSystem : MonoBehaviour
{
    #region ��� :���}
    [Header("��q"), Range(0, 5000)]
    public float hp = 100;
    [Header("���˨ƥ�")]
    public UnityEvent onHurt;
    [Header("���`�ƥ�")]
    public UnityEvent onDead;
    [Header("�ʵe�Ѽ� : ���ˡB���`")]
    public string paramHurt = "";
    public string paramDead = "";
    #endregion

    #region ��� : �p��
    protected float hpMax;
    private Animator ani;
    #endregion

    #region �ƥ�
    private void Awake()
    {
        hpMax = hp;
        ani = GetComponent<Animator>();
    }
    #endregion
    #region ��k : ���}
    ///<summary>
    ///����
    ///</summary>>
    ///<param name="damage">�����쪺�ˮ`</param>>
    public virtual bool Hurt(float damage)
    {
        if (ani.GetBool(paramDead)) return true;
        hp -= damage;
        ani.SetTrigger(paramHurt);
        onHurt.Invoke();
        if (hp <= 0)
        {
            Dead();
            return true;
        }
        else
        {
            return false;
        }

    }
    #endregion
    #region ��k : �p��
    ///<summary>
    ///���`
    ///</summary>
    private void Dead()
    {
        onDead.Invoke();
        ani.SetBool(paramDead, true);
    }
    #endregion
}
