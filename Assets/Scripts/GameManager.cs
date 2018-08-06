using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { set; get; }
    public bool isGameStarted = false;
    private PlayerMotor player;

    //UI stuff
    public Text scoreText;
    public Text HPText;
    private float score;
    public float HP;
    public float HighScore;
    public Text HighScoreText;
    [SerializeField]
    private string BASEURL = "https://docs.google.com/forms/d/e/1FAIpQLScH9MI0AlXVW2tmMzJTbwJM3Q6ZmxyUJe4swsXG8KKXhDfG6A/formResponse";
    [SerializeField]
    private string GETURL = "https://docs.google.com/spreadsheets/d/1mQxa6nxH9y9Z3R4uUe5wP3mF6-CDNBG_u7NMmxpgDZQ/edit?usp=sharing";

    private void Awake()
    {
        HighScore = GetHighScore();
        Instance = this;
        score = 0;
        HP = 3;
        UpdateScore();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

    }
    private void Update()
    {
        HPText.text = "Lives: " + HP.ToString();
        if(MobileInput.Instance.TAP && !isGameStarted)
        {
            isGameStarted = true;
            player.StartRunningGame();
        }
        if(isGameStarted)
        {
            //time score & HP SCORE
            score += (Time.deltaTime);
            scoreText.text = "Score: " + score.ToString("0");
            if (score > HighScore)
                HighScore = score;
            
        }
        if (score > HighScore)
            HighScore = score;
        HighScoreText.text = "HighScore: " + HighScore.ToString("0");
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " +  score.ToString();
    }
    private float GetHighScore()
    {
        
        float _mOnlineHS = PlayerPrefs.GetFloat("highscore");
        return _mOnlineHS;

    }
    public void PostScore()
    {
        WWWForm _mForm = new WWWForm();
        _mForm.AddField("entry.47639752", HighScore.ToString());
        byte[] rawData = _mForm.data;
        WWW wWW = new WWW(BASEURL, rawData);

    }

}
