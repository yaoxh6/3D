using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coast {

    private GameObject coast;
    public int TypeOfCoast;//-1代表左边,1代表右边
    Vector3 LeftPos = new Vector3(-9,1,0);
    Vector3 RightPos = new Vector3(9, 1, 0);
    Vector3[] positions;
    Character[] charactersOnCoast;
    public Coast(int input)
    {
        positions = new Vector3[] {new Vector3(6.5F,2.25F,0), new Vector3(7.5F,2.25F,0), new Vector3(8.5F,2.25F,0),
                new Vector3(9.5F,2.25F,0), new Vector3(10.5F,2.25F,0), new Vector3(11.5F,2.25F,0)};
        charactersOnCoast = new Character[6];
        if (input == -1)
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)),LeftPos, Quaternion.identity, null) as GameObject;
            TypeOfCoast = -1;
        }
        else
        {
            coast = Object.Instantiate(Resources.Load("Perfabs/Stone", typeof(GameObject)), RightPos, Quaternion.identity, null) as GameObject;
            TypeOfCoast = 1;
        }
    }

    public int getEmptyIndex()
    {
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getEmptyPosition()
    {
        Vector3 pos = positions[getEmptyIndex()];
        pos.x *= TypeOfCoast;
        return pos;
    }

    public void getOnCoast(Character input)
    {
        int index = getEmptyIndex();
        charactersOnCoast[index] = input;
    }

    public Character getOffCoast(string input)
    {   
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] != null && charactersOnCoast[i].getName() == input)
            {
                Character charactorLeaveCoast = charactersOnCoast[i];
                charactersOnCoast[i] = null;
                return charactorLeaveCoast;
            }
        }
        return null;
    }

    public void reset()
    {
        charactersOnCoast = new Character[6];
    }

    public int[] getCharacterNum()
    {
        int[] count = { 0, 0 };
        for (int i = 0; i < charactersOnCoast.Length; i++)
        {
            if (charactersOnCoast[i] == null)
                continue;
            if (charactersOnCoast[i].TypeOfCharacter == 0)
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
