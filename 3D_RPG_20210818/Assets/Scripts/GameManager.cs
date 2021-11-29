using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region ���
    [Header("�s�ժ���")]
    public CanvasGroup finGroup;
    [Header("�����e�����D")]
    public Text textTitle;

    private string titleWin = "Victory";
    private string titleLose = "Lose";
    #endregion

    #region ��k : ���}
    /// <summary>
    /// �}�l�H�J��������
    /// </summary>
    /// <param name="win"></param>
    public void StartFade(bool win)
    {
        StartCoroutine(FadeInUI(win ? titleWin : titleLose));
    }
    #endregion

    #region ��k : �p��
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
