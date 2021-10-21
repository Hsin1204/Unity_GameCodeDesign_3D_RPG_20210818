using System.Collections;
using UnityEngine;

public class Learn_Coroutine : MonoBehaviour
{
    /// <summary>
    /// 認識協同程序
    /// </summary>
    private IEnumerator TestCoroutine()
    {
        print("協同程序開始執行");
        yield return new WaitForSeconds(2);
        print("協同程序等待兩秒後執行此行");
    }
    private void Start()
    {
        //協同程序的執行方法
        StartCoroutine(TestCoroutine());
    }
}
