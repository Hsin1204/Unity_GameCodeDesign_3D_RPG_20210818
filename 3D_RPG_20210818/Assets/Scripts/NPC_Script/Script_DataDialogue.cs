using UnityEngine;

namespace Dialogue
{
    /// <summary>
    /// 對話系統資料
    /// NPC要對話的三個階段內容
    /// 接任務前、進行中、完成任務
    /// </summary>
    //ScriptableObject 繼承此類別會變成腳本化物件
    //可將此腳本資料當成物件保存在專案Peoject內
    //CreateAssetMenu 類別屬性:為此類別建立專案內選單
    //menuName 選單名稱，可用/分層
    //fileName 生成的檔案名稱
    [CreateAssetMenu(menuName = "Cus/對話資料", fileName = "NPC對話資料")]
    public class Script_DataDialogue : ScriptableObject
    {
        [Header("NPC的名字")]
        public string name;
        [Header("任務前對話內容"), TextArea(3, 7)]
        public string[] beforeMission;
        [Header("任務中對話內容"), TextArea(3, 7)]
        public string[] inMission;
        [Header("任務後對話內容"), TextArea(3, 7)]
        public string[] FinMission;
        [Header("任務需求數量"), Range(0, 10)]
        public int missionCount;

        //使用列舉:
        //語法 : 修飾詞 列舉名稱 自定義欄位名稱;
        [Header("NPC任務狀態")]
        public NPC_MissionState NPC_MS = NPC_MissionState.BeforeMission;

    }

}
