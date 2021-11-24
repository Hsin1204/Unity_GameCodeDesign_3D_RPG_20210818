using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Script_HurtSystem : MonoBehaviour
{
    #region 欄位 :公開
    [Header("血量"), Range(0, 5000)]
    public float hp = 100;
    [Header("受傷事件")]
    public UnityEvent onHurt;
    [Header("死亡事件")]
    public UnityEvent onDead;
    [Header("動畫參數 : 受傷、死亡")]
    public string paramHurt = "";
    public string paramDead = "";
    #endregion

    #region 欄位 : 私有
    protected float hpMax;
    private Animator ani;
    #endregion

    #region 事件
    private void Awake()
    {
        hpMax = hp;
        ani = GetComponent<Animator>();
    }
    #endregion
    #region 方法 : 公開
    ///<summary>
    ///受傷
    ///</summary>>
    ///<param name="damage">接收到的傷害</param>>
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
    #region 方法 : 私有
    ///<summary>
    ///死亡
    ///</summary>
    private void Dead()
    {
        onDead.Invoke();
        ani.SetBool(paramDead, true);
    }
    #endregion
}
