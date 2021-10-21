using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class API_LearnLerp : MonoBehaviour
{
    public float a = 150, b = 250;
    public Color colorA = Color.black, colorB = Color.red;
    public Vector3 v3A = Vector3.zero, v3B = Vector3.one * 100f;
    // Start is called before the first frame update
    void Start()
    {
        print("a¡Bbªº®t­È¬°" + Mathf.Lerp(a, b, 0.8f));
    }

    // Update is called once per frame
    void Update()
    {
        colorB = Color.Lerp(colorA, colorB, 0.5f);
        v3B = Vector3.Lerp(v3A, v3B, 0.35f);
    }
}
