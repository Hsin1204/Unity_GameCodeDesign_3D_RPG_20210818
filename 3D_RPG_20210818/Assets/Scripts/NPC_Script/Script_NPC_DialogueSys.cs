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
    [Header("��ܫ���")]
    public KeyCode dialougeKey = KeyCode.F;

    /// <summary>
    /// �}�l���
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
    /// ��ܵ��H�J�βH�X
    /// </summary>
    ///<param name="fadeIn">�O�_�H�J</param>
    private IEnumerator SwithDialogueGroup(bool fadeIn)
    {
        float alpha = fadeIn ? 0.1f :  -0.1f;

        for(int i=0;i<10;i++)
        {
            dialogueGroup.alpha += alpha;
            yield return new WaitForSeconds(0.01f);
        }

        
    }
    private IEnumerator ShowDialogueContent(Script_DataDialogue data)
    {
        nameText.text = "";
        finHint.SetActive(false);
        nameText.text = data.name;
        //�M�`�C�@�q���
        for(int j = 0;j<data.beforeMission.Length;j++)
        {
            contentText.text = "";

            //�M����ܨC�@�Ӧr
            for (int i = 0; i < data.beforeMission[j].Length; i++)
            {
                contentText.text += data.beforeMission[j][i];
                yield return new WaitForSeconds(intervalDialogue);
            }
            finHint.SetActive(true);

            //���򵥫ݪ��a���U��ܫ��� 
            while (!Input.GetKeyDown(dialougeKey)) yield return null;
        }

        StartCoroutine(SwithDialogueGroup(false));
    }
}
