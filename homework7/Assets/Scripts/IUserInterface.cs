using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserAction
{
    void MovePlayer(float translationX, float translationZ);

    int GetScore();

    bool GetGameover();

    void Restart();

    void Attack();
}
