using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
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
    [Header("���r�ƥ�")]
    public UnityEvent onType;

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
    
    //��ܹ�ܤ��e
    private IEnumerator ShowDialogueContent(Script_DataDialogue data)
    {
        nameText.text = "";
        nameText.text = data.name;
        string[] dialogueContents = { };

        switch (data.NPC_MS)
        {
            case NPC_MissionState.BeforeMission:
                dialogueContents = data.beforeMission;
                break;
            case NPC_MissionState.InMission:
                dialogueContents = data.inMission;
                break;
            case NPC_MissionState.FinMission:
                dialogueContents = data.FinMission;
                break;
            default:
                break;
        }
        //�M�`�C�@�q���
        for(int j = 0;j<dialogueContents.Length;j++)
        {
            contentText.text = "";
            finHint.SetActive(false);
            //�M����ܨC�@�Ӧr
            for (int i = 0; i < dialogueContents[j].Length; i++)
            {
                onType.Invoke();
                contentText.text += dialogueContents[j][i];
                yield return new WaitForSeconds(intervalDialogue);
            }
            finHint.SetActive(true);

            //���򵥫ݪ��a���U��ܫ��� 
            while (!Input.GetKeyDown(dialougeKey)) yield return null;
        }

        StartCoroutine(SwithDialogueGroup(false));
    }
}
