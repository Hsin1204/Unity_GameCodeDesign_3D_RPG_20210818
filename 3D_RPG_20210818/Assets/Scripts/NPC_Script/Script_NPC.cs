using UnityEngine;
using Dialogue;

/// <summary>
/// NPC�t��
/// �����ؼЬO�_�i�J��ܽd��
/// �åB�}�ҹ�ܨt��
/// </summary>

public class Script_NPC : MonoBehaviour
{
    [Header("��ܸ��")]
    public Script_DataDialogue dataDialogue;
    [Header("������T")]
    [Range(0, 10)]
    public float checkRadius = 3f;
    public GameObject hint;

    [Header("���V���a���t��")]
    public float turnSpeed = 3f;

    [Header("��ܨt��")]
    public Script_NPC_DialogueSys dialogueSys;
    private Transform target;
    private bool startDialoguKey { get => Input.GetKeyDown(KeyCode.E); }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(transform.position, checkRadius);
    }
    private void Update()
    {
        hint.SetActive(CheckPlayer());
        FaceToPlayer();
        StartDialogue();
    }
    /// <summary>
    /// �������a�O�_�i�J�����d��
    /// </summary>
    /// <returns> ���a�i�J �Ǧ^true�_�h��false</returns>
    private bool CheckPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius, 1 << 6);
        //print("�i�J�d�򤺪�����:" + hits[0].name);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
        return hits.Length > 0;
    }
    /// <summary>
    /// ���V���a
    /// </summary>
    private void FaceToPlayer()
    {
        if(CheckPlayer())
        {
            Quaternion angle = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, angle, Time.deltaTime * turnSpeed);
        }
    }
    private void StartDialogue()
    {
        if (CheckPlayer() && startDialoguKey)
        {
            dialogueSys.StartDialogue(dataDialogue);
        }
        else if(!CheckPlayer()) dialogueSys.StopDialogue();
    }
}


