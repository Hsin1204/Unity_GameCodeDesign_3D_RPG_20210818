using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_HurtSystemWithUI : Script_HurtSystem
{
    [Header("要更新的血條")]
    public Image hpBar;

    /// <summary>
    /// 血條效果專用的扣血前血量
    /// </summary>
    private float hpEffectOri;


    public override bool Hurt(float damage)
    {
        hpEffectOri = hp;
        base.Hurt(damage);   //沿用父類別的方法內容
        StartCoroutine(HpBarEffect());
        return hp <= 0;
        
    }
    private IEnumerator HpBarEffect()
    {
        while(hpEffectOri != hp)
        {
            hpEffectOri--;
            hpBar.fillAmount = hpEffectOri / hpMax;
            yield return new WaitForSeconds(0.001f);
        }
    }
}
