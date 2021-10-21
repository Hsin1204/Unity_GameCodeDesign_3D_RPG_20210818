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
    /// <summary>
    /// 開始對話
    /// </summary>
    public void StartDialogue(Script_DataDialogue data)
    {
        StartCoroutine(SwithDialogueGroup());
        StartCoroutine(ShowDialogueContent(data));
    }
    private IEnumerator SwithDialogueGroup()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += 0.1f;
            dialogueGroup.alpha += alpha;
            yield return new WaitForSeconds(0.03f);
        }
    }
    private IEnumerator ShowDialogueContent(Script_DataDialogue data)
    {
        contentText.text = "";
        nameText.text = "";

        for(int i=0; i < data.beforeMission[0].Length;i++ )
        {
            contentText.text += data.beforeMission[0][i];
            yield return new WaitForSeconds(intervalDialogue);
        }
    }
}
