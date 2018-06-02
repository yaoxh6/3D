using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMGUI : MonoBehaviour {

    public float bloodValue = 0.0f;
    private float ResultValue;
    private Rect rctBloodBar;
    private Rect rctUpButton;
    private Rect rctDownButton;
    private bool onoff;

    void Start()
    {
        //血条横向  
        rctBloodBar = new Rect(20, 20, 200, 20);  
        //加血-按钮  
        rctUpButton = new Rect(20, 50, 40, 20);  
        //减血-按钮  
        rctDownButton = new Rect(70, 50, 40, 20);  
        ResultValue = bloodValue;
    }

    void OnGUI()
    {
        if (GUI.Button(rctUpButton, "加血"))
        {
            ResultValue += 0.1f;
        }
        if (GUI.Button(rctDownButton, "减血"))
        {
            ResultValue -= 0.1f;
        }
        if (ResultValue > 1.0f)
        {
            ResultValue = 1.0f;
        }
        if (ResultValue < 0.0f)
        {
            ResultValue = 0.0f;
        }
        //插值计算HP值

        bloodValue = Mathf.Lerp(bloodValue, ResultValue, 0.05f);

        GUI.HorizontalScrollbar(rctBloodBar, 0.0f, bloodValue, 0.0f, 1.0f, GUI.skin.GetStyle("horizontalscrollbar"));  
    }
}