using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleGUI : MonoBehaviour {

    private ClickAction action;
    public int status = 0;
    GUIStyle style;
    GUIStyle buttonStyle;
    private static simpleGUI _instance;
    public static simpleGUI GetFirstController()
    {
        return _instance ?? (_instance = new simpleGUI());
    }


    string hint = "HelloWorld";
    public AI state = new AI(0, 0, 3, 3, false, null);
    public AI endState = new AI(3, 3, 0, 0, true, null);
    void Start()
    {
        action = Director.getInstance().currentSceneController as ClickAction;

        style = new GUIStyle();
        style.fontSize = 40;
        style.alignment = TextAnchor.MiddleCenter;
        buttonStyle = new GUIStyle("button");
        buttonStyle.fontSize = 30;
    }
    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 400, Screen.height / 2 - 210, 200, 500),hint);
        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 90, 100, 50), "游戏指引"))
        {
            Debug.Log("AI0");
            Stack<AI> route = AI.BFS(state, endState);
            hint = "Hint:";
            int step = 1;
            while (route != null)
            {
                AI temp = route.Peek();
                hint+= "\n第" + step +"步" + "\nRight:  Devils: " + temp.rightDevils + "   Priests: " + temp.rightPriests + "\nLeft:  Devils: " + temp.leftDevils + "   Priests: " + temp.leftPriests;
                step++;
                route.Pop();
            }
        }
        if (status == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "Gameover!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.ClickReset();
            }
        }
        else if (status == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 85, 100, 50), "You win!", style);
            if (GUI.Button(new Rect(Screen.width / 2 - 70, Screen.height / 2, 140, 70), "Restart", buttonStyle))
            {
                status = 0;
                action.ClickReset();
            }
        }
    }
}
