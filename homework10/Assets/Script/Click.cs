using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

    public ClickAction action;
    public Character character;
    public main controller;
    public simpleGUI SimpleGUI;
    public void setGameObj(Character input)
    {
        character = input;
    }
    
    void Start()
    {
        action = Director.getInstance().currentSceneController as ClickAction;
        controller = Director.getInstance().currentSceneController as main;
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
        if (gameObject.name == "boat")
        {
            action.ClickBoat();

            int rightPriest = controller.RightCoast.getCharacterNum()[0];
            int rightDevil = controller.RightCoast.getCharacterNum()[1];
            int leftPriest = controller.LeftCoast.getCharacterNum()[0];
            int leftDevil = controller.LeftCoast.getCharacterNum()[1];
            bool location = controller.boat.BoatPosStatus == -1 ? true : false;
            int pcount = controller.boat.getCharacterNum()[0];
            int dcount = controller.boat.getCharacterNum()[1];
            if (location)
            {
                leftPriest += pcount;
                leftDevil += dcount;
            }
            else
            {
                rightPriest += pcount;
                rightDevil += dcount;
            }
            Debug.Log("测试AI");
            Debug.Log(leftPriest);
            Debug.Log(leftDevil);
            Debug.Log(rightPriest);
            Debug.Log(rightDevil);

            controller.simplegui.state = new AI(leftPriest, leftDevil, rightPriest, rightDevil, location, null);
        }
        else
        {
            action.ClickCharacter(character);
        }
    }
}
