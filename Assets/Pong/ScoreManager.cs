using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI leftPlayerScoreText;
    public TextMeshProUGUI rightPlayerScoreText;

    public int leftPlayerScore;
    public int rightPlayerScore;

    public void IncrementLeftPlayerScore()
    {
        leftPlayerScore++;
        leftPlayerScoreText.text = leftPlayerScore.ToString();
    }

    public void IncrementRightPlayerScore()
    {
        rightPlayerScore++;
        rightPlayerScoreText.text = rightPlayerScore.ToString();
    }









    // Start is called before the first frame update
    void Start()
    {
        leftPlayerScore = 0;
        rightPlayerScore = 0;
        leftPlayerScoreText.text = leftPlayerScore.ToString();
        rightPlayerScoreText.text = rightPlayerScore.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

}
