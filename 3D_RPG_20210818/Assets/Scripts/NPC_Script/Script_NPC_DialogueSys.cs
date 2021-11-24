using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;


/// <summary>
/// 對話系統
/// 顯示對話框、對話內容打字效果
/// </summary>

public class Script_NPC_DialogueSys : MonoBehaviour
{
    [Header("對話系統需要的介面物件")]
    public CanvasGroup dialogueGroup;
    public Text nameText;
    public Text contentText;
    public GameObject finHint;
    [Header("對話間隔"), Range(0, 10)]
    public float intervalDialogue = 0.3f;
    [Header("對話按鍵")]
    public KeyCode dialougeKey = KeyCode.F;

    /// <summary>
    /// 開始對話
    /// </summary>
    public void StartDialogue(Script_DataDialogue data)
    {
        StopAllCoroutines();
        StartCoroutine(SwithDialogueGroup(true));
        StartCoroutine(ShowDialogueContent(data));
    }

    public void StopDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(SwithDialogueGroup(false));
    }
    /// <summary>
    /// 對話窗淡入或淡出
    /// </summary>
    ///<param name="fadeIn">是否淡入</param>
    private IEnumerator SwithDialogueGroup(bool fadeIn)
    {
        float alpha = fadeIn ? 0.1f :  -0.1f;

        for(int i=0;i<10;i++)
        {
            dialogueGroup.alpha += alpha;
            yield return new WaitForSeconds(0.01f);
        }

        
    }
    
    //顯示對話內容
    private IEnumerator ShowDialogueContent(Script_DataDialogue data)
    {
        nameText.text = "";
        nameText.text = data.name;
        string[] dialogueContents = data.beforeMission;
        
        //遍循每一段對話
        for(int j = 0;j<dialogueContents.Length;j++)
        {
            contentText.text = "";
            finHint.SetActive(false);
            //遍巡對話每一個字
            for (int i = 0; i < dialogueContents[j].Length; i++)
            {
                contentText.text += dialogueContents[j][i];
                yield return new WaitForSeconds(intervalDialogue);
            }
            finHint.SetActive(true);

            //持續等待玩家按下對話按鍵 
            while (!Input.GetKeyDown(dialougeKey)) yield return null;
        }

        StartCoroutine(SwithDialogueGroup(false));
    }
}
