using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;


public class AI
{
    public int leftPriests;
    public int leftDevils;
    public int rightPriests;
    public int rightDevils;
    public bool boat;
    public AI parent;

    public AI()
    {
        this.leftDevils = 0;
        this.leftPriests = 0;
        this.rightDevils = 0;
        this.rightPriests = 0;
        this.parent = null;
    }

    public AI(int leftPriests, int leftDevils, int rightPriests, int rightDevils, bool boat, AI parent)
    {
        this.leftPriests = leftPriests;
        this.leftDevils = leftDevils;
        this.rightPriests = rightPriests;
        this.rightDevils = rightDevils;
        this.boat = boat;
        this.parent = parent;
    }

    public AI(AI another)
    {
        this.leftPriests = another.leftPriests;
        this.leftDevils = another.leftDevils;
        this.rightPriests = another.rightPriests;
        this.rightDevils = another.rightDevils;
        this.boat = another.boat;
        this.parent = another.parent;
    }

    
    public static bool operator ==(AI lhs, AI rhs)
    {
        return (lhs.leftPriests == rhs.leftPriests && lhs.leftDevils == rhs.leftDevils && lhs.rightPriests == rhs.rightPriests && lhs.rightDevils == rhs.rightDevils && lhs.boat == rhs.boat);
    }

    public static bool operator !=(AI lhs, AI rhs)
    {
        return !(lhs == rhs);
    }
    
    
    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj.GetType().Equals(this.GetType()) == false)
        {
            return false;
        }
        AI temp = null;
        temp = (AI)obj;
        return this.leftPriests.Equals(temp.leftPriests) && this.leftDevils.Equals(temp.leftDevils) && this.rightDevils.Equals(temp.rightDevils) && this.rightPriests.Equals(temp.rightPriests) && this.boat.Equals(temp.boat);
    }
    
    
    public override int GetHashCode()
    {
        return this.leftDevils.GetHashCode() + this.leftPriests.GetHashCode() + this.rightDevils.GetHashCode() + this.rightPriests.GetHashCode() + this.boat.GetHashCode();
    }
    
    public bool valid()
    {
        return ((this.leftPriests == 0 || this.leftPriests >= this.leftDevils) && (this.rightPriests == 0 || this.rightPriests >= this.rightDevils));
    }

    public static Stack<AI> BFS(AI start, AI end)
    {
        Stack<AI> result = new Stack<AI>();
        Queue<AI> found = new Queue<AI>();
        Queue<AI> visited = new Queue<AI>();
        AI temp = new AI(start.leftPriests, start.leftDevils, start.rightPriests, start.rightDevils, start.boat, null);
        found.Enqueue(temp);

        while (found.Count > 0)
        {
            temp = found.Peek();

            if (temp == end)
            {
                while (temp.parent != start)
                {
                    temp = temp.parent;
                    result.Push(temp);
                }
                return result;
            }

            found.Dequeue();
            visited.Enqueue(temp);


            if (temp.boat)
            {
                if (temp.leftPriests > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = false;
                    next.leftPriests--;
                    next.rightPriests++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.leftDevils > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = false;
                    next.leftDevils--;
                    next.rightDevils++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.leftDevils > 0 && temp.leftDevils > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = false;
                    next.leftPriests--;
                    next.leftDevils--;
                    next.rightPriests++;
                    next.rightDevils++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.leftDevils > 1)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = false;
                    next.leftDevils -= 2;
                    next.rightDevils += 2;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.leftPriests > 1)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = false;
                    next.leftPriests -= 2;
                    next.rightPriests += 2;
                    next.parent = new AI(temp);
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
            }
            else
            {
                if (temp.rightPriests > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = true;
                    next.rightPriests--;
                    next.leftPriests++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.rightDevils > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = true;
                    next.rightDevils--;
                    next.leftDevils++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.rightDevils > 0 && temp.rightDevils > 0)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = true;
                    next.rightPriests--;
                    next.rightDevils--;
                    next.leftPriests++;
                    next.leftDevils++;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.rightDevils > 1)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = true;
                    next.rightDevils -= 2;
                    next.leftDevils += 2;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
                if (temp.rightPriests > 1)
                {
                    AI next = new AI(temp);
                    next.parent = new AI(temp);
                    next.boat = true;
                    next.rightPriests -= 2;
                    next.leftPriests += 2;
                    if (next.valid() && !visited.Contains(next) && !found.Contains(next))
                    {
                        found.Enqueue(next);
                    }
                }
            }
        }
        return null;
    }
}
