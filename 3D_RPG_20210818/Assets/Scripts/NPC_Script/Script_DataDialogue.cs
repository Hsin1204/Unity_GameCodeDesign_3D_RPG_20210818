using UnityEngine;

namespace Dialogue
{
    /// <summary>
    /// ��ܨt�θ��
    /// NPC�n��ܪ��T�Ӷ��q���e
    /// �����ȫe�B�i�椤�B��������
    /// </summary>
    //ScriptableObject �~�Ӧ����O�|�ܦ��}���ƪ���
    //�i�N���}����Ʒ�����O�s�b�M��Peoject��
    //CreateAssetMenu ���O�ݩ�:�������O�إ߱M�פ����
    //menuName ���W�١A�i��/���h
    //fileName �ͦ����ɮצW��
    [CreateAssetMenu(menuName = "Cus/��ܸ��", fileName = "NPC��ܸ��")]
    public class Script_DataDialogue : ScriptableObject
    {
        [Header("NPC���W�r")]
        public string name;
        [Header("���ȫe��ܤ��e"), TextArea(3, 7)]
        public string[] beforeMission;
        [Header("���Ȥ���ܤ��e"), TextArea(3, 7)]
        public string[] inMission;
        [Header("���ȫ��ܤ��e"), TextArea(3, 7)]
        public string[] FinMission;
        [Header("���ȻݨD�ƶq"), Range(0, 10)]
        public int missionCount;

        //�ϥΦC�|:
        //�y�k : �׹��� �C�|�W�� �۩w�q���W��;
        [Header("NPC���Ȫ��A")]
        public NPC_MissionState NPC_MS = NPC_MissionState.BeforeMission;

    }

}
