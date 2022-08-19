using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private static TetrisMove tetrisMove;

    public static int scores = 0;

    public Text scoreText;
    public Text highScoreText;

    private int savedScore = 0;
    private string KeyString = "HighScore";
    // Start is called before the first frame update
    void Awake()
    {
        savedScore = PlayerPrefs.GetInt(KeyString, 0); //PlayerPrefs�Լ� = ���ڿ��� ���� �������Ϸ� �������ִ� ����. ���� �ѵ� �� ���� �����ȴ�.
        highScoreText.text = "High Score: " + savedScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scores.ToString("0");

        if(scores > savedScore)
        {
            PlayerPrefs.SetInt(KeyString, scores);
        }
    }
}
