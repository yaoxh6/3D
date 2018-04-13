using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RoundController : MonoBehaviour
{
    private Color color;
    private Vector3 emissionPositon;
    private Vector3 emissionDiretion;
    private float speed;
    private SceneController scene;
    public int trial = 0;

    void Awake()
    {
        SceneController.getInstance().setRoundController(this);
        scene = SceneController.getInstance();
    }

    void Start()
    {
        scene.nextRound();
    }

    public void loadRoundData(int round)
    {
        trial = 0;
        switch (round)
        {
            case 1:    
                color = Color.green;
                emissionPositon = new Vector3(-2.5f, 0.2f, -5f);
                emissionDiretion = new Vector3(24.5f, 40.0f, 67f);
                speed = 2;
                SceneController.getInstance().getFirstController().setting(1, color, emissionPositon, emissionDiretion.normalized, speed, 1);
                break;
            case 2:    
                color = Color.red;
                emissionPositon = new Vector3(2.5f, 0.2f, -5f);
                emissionDiretion = new Vector3(-24.5f, 35.0f, 67f);
                speed = 4;
                SceneController.getInstance().getFirstController().setting(1, color, emissionPositon, emissionDiretion.normalized, speed, 2);
                break;
            case 3:
                color = Color.cyan;
                emissionPositon = new Vector3(2.5f, 0.2f, -5f);
                emissionDiretion = new Vector3(-24.5f, 35.0f, 67f);
                speed = 8;
                SceneController.getInstance().getFirstController().setting(1, color, emissionPositon, emissionDiretion.normalized, speed, 3);
                break;
        }
    }
}