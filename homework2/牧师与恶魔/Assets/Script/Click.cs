using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    ClickAction action;
    Character character;

    public void setGameObj(Character input)
    {
        character = input;
    }
    
    void Start()
    {
        action = Director.getInstance().currentSceneController as ClickAction;
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (gameObject.name == "boat")
        {
            action.ClickBoat();
        }
        else
        {
            action.ClickCharacter(character);
        }
    }
}
