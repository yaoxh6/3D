using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    //玩家躲开追踪时候触发
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    //玩家碰撞到巡逻兵的时候触发
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;

    public void PlayerEscape()
    {
        if (ScoreChange != null)
        {
            ScoreChange();
        }
    }
    public void PlayerGameover()
    {
        if (GameoverChange != null)
        {
            GameoverChange();
        }
    }
}
