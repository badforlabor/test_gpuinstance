using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMono : MonoBehaviour
{
    private const float OneSec = 1.0f;
    public int Fps;
    public int Min;
    public int Max;
    public float Avg;
    public int Count;
    private float mElapse;
    private int mFrame;

    void Awake()
    {
        // Application.targetFrameRate = 60;
    }

    public void Update()
    {
        try
        {
            UpdateImpl();
        }
        catch (Exception e)
        {
            
        }
    }
    void UpdateImpl()
    {
        mElapse += Time.unscaledDeltaTime;
        mFrame++;

        if (mElapse > OneSec) {
            Fps = mFrame;
                
            if (Fps < Min || Min == 0) { Min = Fps; }
            if (Fps > Max || Max == 0) { Max = Fps; }

            Count++;
            Avg = (Avg * (Count - 1) + Fps) / Count;

            mElapse -= OneSec;
            mFrame = 0;

            cachedDesc = GetDesc();
        }
    }

    private string cachedDesc = "";
    
    private void OnGUI()
    {
        var old = GUI.color;
        GUI.color = Color.red;
        GUI.Label(new Rect(10, Screen.height-50, 500, 50), cachedDesc);
        GUI.Label(new Rect(0, 0, 100, 50), DateTime.Now.ToString());
        GUI.Label(new Rect(Screen.width/2, Screen.height/2, 100, 50), DateTime.Now.ToString());
        GUI.color = old;
    }

    public string GetDesc()
    {
        return string.Format("fps:{0} min:{1} max:{2} avg:{3} count:{4}",
            Fps, Min, Max, Avg, Count);
    }
}
