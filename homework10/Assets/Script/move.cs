using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    private float moveSpeed = 20;

    int moveStatus;//0静止,1移动到中间,2移动到终点
    Vector3 Destination;
    Vector3 Middle;

    private void Update()
    {
        if(moveStatus == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, Middle, moveSpeed * Time.deltaTime);
            if (transform.position == Middle)
            {
                moveStatus = 2;
            }
        }
        else if(moveStatus == 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, moveSpeed * Time.deltaTime);
            if(transform.position == Destination)
            {
                moveStatus = 0;
            }
        }


    }

    public void setDestination(Vector3 input)
    {
        Middle = input;
        Destination = input;
        if(transform.position.y == input.y)
        {
            moveStatus = 2;
        }
        else if(transform.position.y > input.y)
        {
            Middle.y = transform.position.y;
            moveStatus = 1;
        }
        else
        {
            Middle.x = transform.position.x;
            moveStatus = 1;
        }
    }

    public void reset()
    {
        moveStatus = 0;
    }
}
