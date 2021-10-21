using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;


/// <summary>
/// ��ܨt��
/// ��ܹ�ܮءB��ܤ��e���r�ĪG
/// </summary>

public class Script_NPC_DialogueSys : MonoBehaviour
{
    [Header("��ܨt�λݭn����������")]
    public CanvasGroup dialogueGroup;
    public Text nameText;
    public Text contentText;
    public GameObject finHint;
    [Header("��ܶ��j"), Range(0, 10)]
    public float intervalDialogue = 0.3f;
    /// <summary>
    /// �}�l���
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
