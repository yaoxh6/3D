using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {
    private GameObject character;
    public int TypeOfCharacter;//1代表魔鬼,0代表牧师
    private move Cmove;
    public int characterStatus;//0代表在岸上,1代表在船上
    Coast coast;
    Click click;

    public Character(int input)
    {
        if(input == 0)
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            TypeOfCharacter = 0;
        }
        else
        {
            character = Object.Instantiate(Resources.Load("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
            TypeOfCharacter = 1;
        }

        Cmove = character.AddComponent(typeof(move)) as move;
        click = character.AddComponent(typeof(Click)) as Click;
        click.setGameObj(this);
    }
	
    public string getName()
    {
        return character.name;
    }

    public void setName(string input)
    {
        character.name = input;
    }

    public void setPosition(Vector3 input)
    {
        character.transform.position = input;
    }

    public void getOnBoat(Boat input)
    {
        character.transform.parent = input.getGameobj().transform;
        characterStatus = 1;
    }

    public void getOnCoast(Coast input)
    {
        character.transform.parent = null;
        characterStatus = 0;
        coast = input;
    }

    public void reset()
    {
        Cmove.reset();
        coast = (Director.getInstance().currentSceneController as main).RightCoast;
        getOnCoast(coast);
        setPosition(coast.getEmptyPosition());
        coast.getOnCoast(this);
    }

    public void moveToPosition(Vector3 input)
    {
        Cmove.setDestination(input);
    }

    public Coast getCoast()
    {
        return coast;
    }
}
