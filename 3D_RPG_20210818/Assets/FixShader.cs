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
        var objs = Selection.objects;

        foreach (var obj in objs)
        {
            var go = obj as GameObject;
            var rs = go.GetComponentInChildren<Renderer>();

            for (int i = 0; i < rs.materials.Length; i++)
            {
                Material m = rs.materials[i];

                if (m.shader != Shader.Find("HDRP/Lit") && m.shader != Shader.Find("HDRP/Unlit"))
                {
                    Texture baseMap = m.GetTexture("_MainTex");
                    Texture nMap = m.GetTexture("_BumpMap");
                    Texture rMap = m.GetTexture("_SpecGlossMap");
                    Texture eMap = null;
                    Texture reflec = null;

                    if (m.GetTexture("_EmissionMap") != null)
                    {
                        eMap = m.GetTexture("_EmissionMap");
                    }
                    if (m.GetTexture("_Reflection") != null)
                    {
                        reflec = m.GetTexture("_Reflection");
                    }
                    /* foreach (string p in m.GetTexturePropertyNames())
                     {
                         if (m.GetTexture(p) != null)
                         {
                             print(p + " : " + m.GetTexture(p).name);

                         }*/


                    m.shader = Shader.Find("HDRP/Lit");
                    m.SetTexture("_BaseColorMap", baseMap);
                    m.SetTexture("_NormalMap", nMap);
                    m.SetTexture("_MaskMap", rMap);
                    if (eMap != null)
                    {
                        //m
                        m.SetTexture("_EmissionColor", eMap);

                    }


                }

                /*for (int j = 0; j < m.shader.GetPropertyCount(); j++)
                {
                    print(m.shader.GetPropertyName(j) + "index : " + m.shader.FindPropertyIndex(m.shader.GetPropertyName(j)));
                }*/
                
                
            }



        }
    }
}
