using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess : MonoBehaviour
{
    private int[,] game = new int[3, 3];
    private int next = 1;
    public Texture2D assassin;
    public Texture2D knight;
    public void Start()
    {
        Reset();
    }

    public void Reset()
    {
        next = 1;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                game[i, j] = 0;
            }
        }
    }

    int IsWin()
    {
        
        for(int i = 0; i < 3; i++)
        {
            //检查行
            if(game[i,0] == game[i,1] && game[i,1] == game[i, 2] && game[i,0] != 0)
            {
                return game[i,0];
            }//检查列
            else if(game[0,i] == game[1,i] && game[1,i] == game[2,i] && game[0,i] != 0)
            {
                return game[0,i];
            }

        }
        //检查对角线
        if(game[0,0] == game[1,1] && game[1,1] == game[2,2] && game[0,0] != 0)
        {
            return game[0, 0];
        }
        if(game[0,2] == game[1,1] && game[1,1] == game[2,0] && game[0,2] != 0)
        {
            return game[0, 2];
        }

        //检查游戏是否结束
        int count = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(game[i,j] != 0)
                {
                    count++;
                }
            }
        }
        if(count == 9)
        {
            return 3;
        }
        return 0;
    }


    public void OnGUI()
    {
        if (GUI.Button(new Rect(300, 200, 100, 50), "重新开始"))
        {
            Reset();
        }
        int result = IsWin();
        Debug.Log(result);
        if(result == 1)
        {
            GUI.Label(new Rect(300, 0, 50, 50), assassin);
            GUI.Label(new Rect(350, 20, 50, 50), "O 赢了");
        }
        else if(result == 2)
        {
            GUI.Label(new Rect(300, 0, 50, 50), knight);
            GUI.Label(new Rect(350, 20, 50, 50), "X 赢了");
        }
        else if(result == 3)
        {
            GUI.Label(new Rect(300, 20, 50, 50), "平局");
        }
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(game[i,j] == 1)
                    {
                        GUI.Button(new Rect(300 + i * 50, 50 + j * 50, 50, 50), assassin);
                    }
                    else if(game[i,j] == 2)
                    {
                        GUI.Button(new Rect(300 + i * 50, 50 + j * 50, 50, 50), knight);
                    }
                    if(GUI.Button(new Rect(300 + i * 50, 50 + j * 50, 50, 50), ""))
                    {
                        if(result == 0)
                        {
                            if (next % 2 == 1)
                            {
                                game[i, j] = 1;
                            }
                            else
                            {
                                game[i, j] = 2;
                            }
                        next++;
                        }
                    }
                }
            }
        
    }
    public void Update()
    {

    }
}