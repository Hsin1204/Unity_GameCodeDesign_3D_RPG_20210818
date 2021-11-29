using UnityEngine;
using UnityEngine.Events;
using Dialogue;

/// <summary>
/// NPC�t��
/// �����ؼЬO�_�i�J��ܽd��
/// �åB�}�ҹ�ܨt��
/// </summary>

public class Script_NPC : MonoBehaviour
{
    [Header("�������Ȩƥ�")]
    public UnityEvent onFinish;
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
    //�ثe���ȼƶq
    private int currCount;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,0.5f);
        Gizmos.DrawSphere(transform.position, checkRadius);
    }

    private void Awake()
    {
        InitialState();
    }
    private void Update()
    {
        hint.SetActive(CheckPlayer());
        FaceToPlayer();
        StartDialogue();
    }
    
    /// <summary>
    /// ��l�]�w
    /// ���A��_�����ȫe
    /// </summary>
    private void InitialState()
    {
        dataDialogue.NPC_MS = NPC_MissionState.BeforeMission;
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
    /// <summary>
    /// NPC��ܨt��
    /// </summary>
    private void StartDialogue()
    {
        if (CheckPlayer() && startDialoguKey)
        {
            dialogueSys.StartDialogue(dataDialogue);

            //�P�_NPC�����Ȫ��A
            if(dataDialogue.NPC_MS == NPC_MissionState.BeforeMission)
            {
                dataDialogue.NPC_MS = NPC_MissionState.InMission;
            }
        }
        else if(!CheckPlayer()) dialogueSys.StopDialogue();
    }
    /// <summary>
    /// ��s���ȻݨD�ƶq
    /// ���ȥؼЪ���o��Φ��`��B�z
    /// </summary>
    public void UpdateMission()
    {
        currCount++;

        // �ثe�ƶq ���� �ݨD�ƶq 
        if(currCount == dataDialogue.missionCount)
        {
            //���A ���� ��������
            dataDialogue.NPC_MS = NPC_MissionState.FinMission;
            onFinish.Invoke();
        }
    }
}


