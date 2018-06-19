using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat {

    private GameObject boat;
    Vector3 LeftPos = new Vector3(-5, 1, 0);
    Vector3 RightPos = new Vector3(5, 1, 0);
    Vector3[] characterOnLeftPos;
    Vector3[] characterOnRightPos;
    //move Bmove;
    public int BoatPosStatus;//-1在左边,1在右边
    Character[] characterOnBoat;
    Click click;
    public float movingSpeed = 20;

    public Boat()
    {
        characterOnBoat = new Character[2];
        characterOnRightPos = new Vector3[] { new Vector3(4.5F, 1.5F, 0), new Vector3(5.5F, 1.5F, 0) };
        characterOnLeftPos = new Vector3[] { new Vector3(-5.5F, 1.5F, 0), new Vector3(-4.5F, 1.5F, 0) };
        boat = Object.Instantiate(Resources.Load("Perfabs/Boat", typeof(GameObject)),RightPos , Quaternion.identity, null) as GameObject;
        boat.name = "boat";
        //Bmove = boat.AddComponent(typeof(move)) as move;
        BoatPosStatus = 1;
        click = boat.AddComponent(typeof(Click)) as Click;

    }
    /*
    public void Move()
    {
        Debug.Log("Move");
        if (BoatPosStatus == -1)
        {
            Bmove.setDestination(RightPos);
            BoatPosStatus = 1;
        }
        else
        {
            Bmove.setDestination(LeftPos);
            BoatPosStatus = -1;
        }
    }*/
    public void newMove()
    {
        Debug.Log("Move");
        if (BoatPosStatus == -1)
        {
            BoatPosStatus = 1;
        }
        else
        {
            BoatPosStatus = -1;
        }

    }
        public int getEmptyIndex()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public bool isEmpty()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public Vector3 getDestination()
    {
        if (BoatPosStatus == -1)
        {
            return RightPos;
        }
        else
        {
            return LeftPos;
        }
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos;
        int emptyIndex = getEmptyIndex();
        if (BoatPosStatus == -1)
        {
            pos = characterOnLeftPos[emptyIndex];
        }
        else
        {
            pos = characterOnRightPos[emptyIndex];
        }
        return pos;
    }

    public void GetOnBoat(Character input)
    {
        int index = getEmptyIndex();
        characterOnBoat[index] = input;
    }

    public Character GetOffBoat(string input)
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null && characterOnBoat[i].getName() == input)
            {
                Character character = characterOnBoat[i];
                characterOnBoat[i] = null;
                return character;
            }
        }
        return null;
    }

    public void reset()
    {
        /*
        if ( BoatPosStatus == -1)
        {
            Move();
        }*/
        if (BoatPosStatus == -1)
        {
            boat.transform.position = RightPos;
            newMove();
        }

        characterOnBoat = new Character[2];
    }

    public GameObject getGameobj()
    {
        return boat;
    }

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
                continue;
            if (characterOnBoat[i].TypeOfCharacter == 0)
            {
                count[0]++;
            }
            else
            {
                count[1]++;
            }
        }
        return count;
    }
}
