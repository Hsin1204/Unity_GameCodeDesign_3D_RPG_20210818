using System.Collections;
using UnityEngine;

public class Learn_Coroutine : MonoBehaviour
{
    /// <summary>
    /// �{�Ѩ�P�{��
    /// </summary>
    private IEnumerator TestCoroutine()
    {
        print("��P�{�Ƕ}�l����");
        yield return new WaitForSeconds(2);
        print("��P�{�ǵ��ݨ�����榹��");
    }
    private void Start()
    {
        //��P�{�Ǫ������k
        StartCoroutine(TestCoroutine());
    }
}
