using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    private int show_time = 8;
    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(205, 179, 128, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(3,101,100,1);
        score_style.fontSize = 16;
        over_style.fontSize = 25;
        //好的方法实现一个时间差，StartCoroutine函数和yield return成对出现。
        StartCoroutine(ShowTip());
    }

    void Update()
    {
        //人物的动作和移动，得到水平两个方向的偏移量来控制移动，通过按键控制动作。
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.MovePlayer(translationX, translationZ);
        action.Attack();
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", text_style);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), score_style);
        if(action.GetGameover() && action.GetScore() != 20)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                //重新开始
                action.Restart();
                return;
            }
        }
        else if(action.GetScore() == 20)
        {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "恭喜胜利！", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                //重新开始
                action.Restart();
                return;
            }
        }
        if(show_time > 0)
        {
            GUI.Label(new Rect(Screen.width / 2 - 87 ,10, 100, 100), "按WSAD或方向键移动", text_style);
            GUI.Label(new Rect(Screen.width / 2 - 87, 30, 100, 100), "成功躲避巡逻兵追捕加1分", text_style);
            GUI.Label(new Rect(Screen.width / 2 - 87, 50, 100, 100), "获得十分就胜利", text_style);
        }
    }

    public IEnumerator ShowTip()
    {
        while (show_time >= 0)
        {
            yield return new WaitForSeconds(1);
            show_time--;
        }
    }
}
