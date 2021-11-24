using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_HurtSystemWithUI : Script_HurtSystem
{
    [Header("�n��s�����")]
    public Image hpBar;

    /// <summary>
    /// ����ĪG�M�Ϊ�����e��q
    /// </summary>
    private float hpEffectOri;


    public override bool Hurt(float damage)
    {
        hpEffectOri = hp;
        base.Hurt(damage);   //�u�Τ����O����k���e
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
