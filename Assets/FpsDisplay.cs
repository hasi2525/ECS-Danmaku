using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsDisplay : MonoBehaviour
{

    // ïœêî
    int frameCount;
    float prevTime;
    float fps;

    // èâä˙âªèàóù
    void Start()
    {
        // ïœêîÇÃèâä˙âª
        frameCount = 0;
        prevTime = 0f;
    }

    // çXêVèàóù
    void Update()
    {
        frameCount++;
        float time = Time.realtimeSinceStartup - prevTime;

        if (time >= 0.5f)
        {
            fps = frameCount / time;
            Debug.Log(fps);

            frameCount = 0;
            prevTime = Time.realtimeSinceStartup;
        }
    }

    // ï\é¶èàóù
    private void OnGUI()
    {
        GUI.skin.label.fontSize = 100;
        GUILayout.Label("FPS :" + fps.ToString("F0"));
    }
}