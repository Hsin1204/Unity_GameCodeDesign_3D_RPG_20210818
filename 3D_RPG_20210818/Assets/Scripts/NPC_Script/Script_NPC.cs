using UnityEngine;
using UnityEngine.Events;
using Dialogue;

/// <summary>
/// NPC系統
/// 偵測目標是否進入對話範圍
/// 並且開啟對話系統
/// </summary>

public class Script_NPC : MonoBehaviour
{
    [Header("完成任務事件")]
    public UnityEvent onFinish;
    [Header("對話資料")]
    public Script_DataDialogue dataDialogue;
    [Header("相關資訊")]
    [Range(0, 10)]
    public float checkRadius = 3f;
    public GameObject hint;

    [Header("面向玩家的速度")]
    public float turnSpeed = 3f;

    [Header("對話系統")]
    public Script_NPC_DialogueSys dialogueSys;
    private Transform target;
    private bool startDialoguKey { get => Input.GetKeyDown(KeyCode.E); }
    //目前任務數量
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
    /// 初始設定
    /// 狀態恢復為任務前
    /// </summary>
    private void InitialState()
    {
        dataDialogue.NPC_MS = NPC_MissionState.BeforeMission;
    }
    /// <summary>
    /// 偵測玩家是否進入偵測範圍
    /// </summary>
    /// <returns> 玩家進入 傳回true否則為false</returns>
    private bool CheckPlayer()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, checkRadius, 1 << 6);
        //print("進入範圍內的物件:" + hits[0].name);
        if(hits.Length > 0)
        {
            target = hits[0].transform;
        }
        return hits.Length > 0;
    }
    /// <summary>
    /// 面向玩家
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
    /// NPC對話系統
    /// </summary>
    private void StartDialogue()
    {
        if (CheckPlayer() && startDialoguKey)
        {
            dialogueSys.StartDialogue(dataDialogue);

            //判斷NPC的任務狀態
            if(dataDialogue.NPC_MS == NPC_MissionState.BeforeMission)
            {
                dataDialogue.NPC_MS = NPC_MissionState.InMission;
            }
        }
        else if(!CheckPlayer()) dialogueSys.StopDialogue();
    }
    /// <summary>
    /// 更新任務需求數量
    /// 任務目標物件得到或死亡後處理
    /// </summary>
    public void UpdateMission()
    {
        currCount++;

        // 目前數量 等於 需求數量 
        if(currCount == dataDialogue.missionCount)
        {
            //狀態 等於 完成任務
            dataDialogue.NPC_MS = NPC_MissionState.FinMission;
            onFinish.Invoke();
        }
    }
}


