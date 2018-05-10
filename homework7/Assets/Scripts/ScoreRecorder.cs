using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    public int score = 0;                            //分数
    public void AddScore()
    {
        score++;
    }
}

