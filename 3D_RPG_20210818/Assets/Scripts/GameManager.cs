using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region 欄位
    [Header("群組物件")]
    public CanvasGroup finGroup;
    [Header("結束畫面標題")]
    public Text textTitle;

    private string titleWin = "Victory";
    private string titleLose = "Lose";
    #endregion

    #region 方法 : 公開
    /// <summary>
    /// 開始淡入結束介面
    /// </summary>
    /// <param name="win"></param>
    public void StartFade(bool win)
    {
        StartCoroutine(FadeInUI(win ? titleWin : titleLose));
    }
    #endregion

    #region 方法 : 私有
    private IEnumerator FadeInUI(string title)
    {
        textTitle.text = title;
        finGroup.interactable = true;
        finGroup.blocksRaycasts = true;

        for(int i=0; i<10;i++)
        {
            finGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.02f);
        }
    }
    #endregion

}
