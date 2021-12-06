#if (UNITY_EDITOR)
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FixShader : MonoBehaviour
{
    public GameObject obj;

    [MenuItem("CusTool/FixShader")]
    static void Fix()
    {
        List<GameObject> objList = GetObjs();
        Renderer rs = null;


        foreach (var obj in objList)
        {
           if(obj.TryGetComponent(out Renderer rr))
            {
                rs = rr;
            }
           else
            {
                print("jump");
                continue;
            }
            for (int i = 0; i < rs.materials.Length; i++)
            {
                //print("Get Material");
                Material m = rs.materials[i]; ;
                if (m.shader != Shader.Find("HDRP/Lit") && m.shader != Shader.Find("HDRP/Unlit"))
                {
                    /*for (int j = 0; j < m.shader.GetPropertyCount(); j++)
                    {
                        print(m.shader.GetPropertyName(j) + " index : " + m.shader.FindPropertyIndex(m.shader.GetPropertyName(j)));
                    }*/

                    Texture baseMap = m.GetTexture("_MainTex");
                    Texture nMap = m.GetTexture("_BumpMap");
                    Texture rMap = m.GetTexture("_SpecGlossMap");
                    Texture eMap = null;
                    Texture reflec = null;
                    float cutoff = 0;

                    if (m.GetTexture("_EmissionMap") != null)
                    {
                        eMap = m.GetTexture("_EmissionMap");
                    }
                    if (m.GetTexture("_Reflection") != null)
                    {
                        reflec = m.GetTexture("_Reflection");
                    }
                    if (m.GetFloat("_Cutoff") > 0)
                    {
                        cutoff = m.GetFloat("_Cutoff");
                    }



                    m.shader = Shader.Find("HDRP/Lit");
                    m.SetTexture("_BaseColorMap", baseMap);
                    m.SetTexture("_NormalMap", nMap);
                    m.SetTexture("_MaskMap", rMap);
                    if (eMap != null)
                    {
                        m.SetFloat("_UseEmissiveIntensity", 1);
                        m.SetTexture("_EmissiveColorMap", eMap);
                        m.SetInt("_EmissiveIntensity", 6);

                    }
                    if (cutoff > 0)
                    {
                        m.SetFloat("_AlphaCutoffEnable", 1);
                        m.SetFloat("_AlphaCutoff", cutoff);
                    }


                }

            }

            /* for (int j = 0; j < m.shader.GetPropertyCount(); j++)
             {
                 print(m.shader.GetPropertyName(j) + " index : " + m.shader.FindPropertyIndex(m.shader.GetPropertyName(j)));
             }
             print(m.shader.GetPropertyName(61) + " "+m.GetFloat("_AlphaCutoff"));*/
        }









    }
    /// <summary>
    /// 抓取選取的所有物件
    /// </summary>
    /// <returns></returns>
    static List<GameObject> GetObjs()
    {
        List<GameObject> temp = new List<GameObject>();
        var objs = Selection.objects;

        foreach (var o in objs)
        {
            var go = o as GameObject;
            //print(go.name);
            for (int i = 0; i < go.transform.childCount; i++) //確認子物件數量
            {

                var goc = go.transform.GetChild(i);    //新增子物件的欄位
                //print(goc.childCount);       
                if (goc.childCount > 0)                 //如果子物件還有子物件
                {
                    //print(goc.name);
                    for (int j = 0; j < goc.childCount; j++)
                    {
                        var gs = goc.GetChild(j).gameObject;
                        //print(gs.gameObject.name);
                        temp.Add(gs);
                    }
                }
                temp.Add(goc.gameObject);        //加入子物件
            }
            temp.Add(go as GameObject); //最後將父物件加入
        }
        /*foreach (var oj in objList)
        {
            print(oj.name);
        }*/
        return temp;
    }
}
#endif
