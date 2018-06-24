
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {

    public bool isEnd;
	
	void Start () {
        isEnd = false;
	}

    public void OnGUI()
    {
        if (isEnd)
        {
            GUI.Label(new Rect(300,200, 200, 50), "游戏结束");
        }
    }
}
