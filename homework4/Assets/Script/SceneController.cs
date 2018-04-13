using UnityEngine;
using System.Collections;


public interface GameInterface
{
    void MakeEmissionDiskable();
    bool isCounting();
    bool isShooting();
    int getRound();
    int getScore();
    int getTimeToNextEmissionTime();
    void nextRound();
    void setScore(int point);
    void setRound(int input);
    int getTrial();
    void setTrial(int input);
    void gameOver();
}

public class SceneController : System.Object, GameInterface
{
    private static SceneController _instance;
    private RoundController _roundController;
    private FirstController _firstController;
    private ScoreController _scoreController;
    private UserInterface _userInterface;

    private int _round;
    private int _score;
    private int _trial;
    public bool isGameOver;

    public static SceneController getInstance()
    {
        if (_instance == null)
        {
            _instance = new SceneController();
        }
        return _instance;
    }

    public void setFirstController(FirstController input)
    {
        _firstController = input;
    }
    internal FirstController getFirstController()
    {
        return _firstController;
    }

    public void setScoreController(ScoreController input)
    {
        _scoreController = input;
    }
    internal ScoreController getScoreController()
    {
        return _scoreController;
    }
    internal UserInterface getUserInterface()
    {
        return _userInterface;
    }
    public void setUserInterface(UserInterface input)
    {
        _userInterface = input;
    }
    public void setRoundController(RoundController input)
    {
        _roundController = input;
    }
    internal RoundController getRoundController()
    {
        return _roundController;
    }


    public void MakeEmissionDiskable()
    {
        _firstController.MakeEmissionDiskable();
    }


    public bool isCounting()
    {
        return _firstController.getIsCounting();
    }
    public bool isShooting()
    {
        return _firstController.getIsShooting();
    }
    public int getRound()
    {
        return _round;
    }
    public void setRound(int input)
    {
        _round = input;
    }
    public int getScore()
    {
        return _score;
    }
    public int getTimeToNextEmissionTime()
    {
        return (int)_firstController.timeToNextEmission + 1;
    }
    public void setScore(int input)
    {
        _score = input;
    }
    public void nextRound()
    {
        _roundController.loadRoundData(++_round);
    }
    public int getTrial()
    {
        return _roundController.trial;
    }
    public void setTrial(int input)
    {
        _roundController.trial = input;
    }
    public void gameOver()
    {
        isGameOver = true;
        _userInterface.gameOver();
    }
}