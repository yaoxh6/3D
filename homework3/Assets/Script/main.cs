using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class main : MonoBehaviour,SceneController,ClickAction {

    private Coast LeftCoast;
    public Coast RightCoast;
    private Boat boat;
    private Character[] characters;
    private Water water;
    public simpleGUI simplegui;
    private FirstSceneActionManager actionManager;

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        characters = new Character[6];
        loadResources();
        simplegui = gameObject.AddComponent<simpleGUI>() as simpleGUI;
        actionManager = GetComponent<FirstSceneActionManager>();
    }

    public void loadResources()
    {
        LeftCoast = new Coast(-1);
        RightCoast = new Coast(1);
        boat = new Boat();
        //water = new Water();
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("ok");
            Character newCharacter = new Character(0);
            newCharacter.setName("priest" + i);
            newCharacter.setPosition(RightCoast.getEmptyPosition());
            newCharacter.getOnCoast(RightCoast);
            RightCoast.getOnCoast(newCharacter);

            characters[i] = newCharacter;
        }
        for (int i = 0; i < 3; i++)
        {
            Character newCharacter = new Character(1);
            newCharacter.setName("devil" + i);
            newCharacter.setPosition(RightCoast.getEmptyPosition());
            newCharacter.getOnCoast(RightCoast);
            RightCoast.getOnCoast(newCharacter);

            characters[i+3] = newCharacter;
        }
    }

    public void ClickBoat()
    {
        if (boat.isEmpty())
        {
            return;
        }
        else
        {
            actionManager.moveBoat(boat);
            boat.newMove();
        }
        simplegui.status = gameStatus();
    }

    public void ClickCharacter(Character input)
    {
        if (input.characterStatus == 1)//0在岸上,1在船上
        {
            Coast coast;
            Debug.Log(boat.BoatPosStatus);
            if (boat.BoatPosStatus == -1)
            {
                coast = LeftCoast;
            }
            else
            {
                coast = RightCoast;
            }
            boat.GetOffBoat(input.getName());
            Debug.Log("test1");
            Debug.Log(coast.getEmptyPosition());
            //input.moveToPosition(coast.getEmptyPosition());
            actionManager.moveCharacter(input, coast.getEmptyPosition());
            input.getOnCoast(coast);
            coast.getOnCoast(input);

        }
        else
        {
            Coast coast = input.getCoast();

            if (boat.getEmptyIndex() == -1)
            {
                return;
            }
            if (coast.TypeOfCoast != boat.BoatPosStatus)
            {
                return;
            }
            coast.getOffCoast(input.getName());
            Debug.Log("NewTest");
            Debug.Log(input);
            Debug.Log(boat.getEmptyPosition());
            actionManager.moveCharacter(input, boat.getEmptyPosition());
            input.getOnBoat(boat);
            boat.GetOnBoat(input);
        }
        Debug.Log(gameStatus());
        simplegui.status = gameStatus();
    }
    public void ClickReset()
    {
        boat.reset();
        LeftCoast.reset();
        RightCoast.reset();
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].reset();
        }
    }

    public int gameStatus()
    {
        int from_priest = 0;
        int from_devil = 0;
        int to_priest = 0;
        int to_devil = 0;

        int[] fromCount = RightCoast.getCharacterNum();
        from_priest += fromCount[0];
        from_devil += fromCount[1];
        //Debug.Log("test1");
        //Debug.Log(from_priest);
        //Debug.Log(from_devil);
        //Debug.Log(to_priest);
        //Debug.Log(to_devil);
        int[] toCount = LeftCoast.getCharacterNum();
        to_priest += toCount[0];
        to_devil += toCount[1];

        if (to_priest + to_devil == 6)      
            return 2;

        int[] boatCount = boat.getCharacterNum();
        if (boat.BoatPosStatus == -1)
        {   
            to_priest += boatCount[0];
            to_devil += boatCount[1];
        }
        else
        {   
            from_priest += boatCount[0];
            from_devil += boatCount[1];
        }
        //Debug.Log("test2");
        //Debug.Log(from_priest);
        //Debug.Log(from_devil);
        //Debug.Log(to_priest);
        //Debug.Log(to_devil);
        if (from_priest < from_devil && from_priest > 0)
        {
            //Debug.Log("test3");
            //Debug.Log(from_priest);
            //Debug.Log(from_devil);
            //Debug.Log(to_priest);
            //Debug.Log(to_devil);
            return 1;
        }
        if (to_priest < to_devil && to_priest > 0)
        {
            //Debug.Log("test4");
            //Debug.Log(from_priest);
            //Debug.Log(from_devil);
            //Debug.Log(to_priest);
            //Debug.Log(to_devil);
            return 1;
        }
        return 0;
    }
}
