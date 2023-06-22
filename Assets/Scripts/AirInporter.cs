using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AirInporter : MonoBehaviour
{
    bool pushFrag = false;
    float pushTime;
    [SerializeField]float maxPushTime;

    float airAmount;
    [SerializeField] float maxAirAmount;
    float airAmountDifference;

    [SerializeField] Image ukiwa;

    public static float score;
    [SerializeField] Text scoreText;
    [SerializeField] float okScore;
    [SerializeField] float goodScore;
    [SerializeField] float perfectScore;

    float timeLimit;
    [SerializeField]Text timeLimitText;
    [SerializeField]float maxTimeLimit;
    

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        timeLimit = maxTimeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (pushFrag)
        {
            pushTime += Time.deltaTime;
            if (pushTime >= maxPushTime)
            {
                pushFrag = false;
                pushTime = 0;
            }
            airAmount += Time.deltaTime;
        }
        ukiwa.fillAmount = airAmount / maxAirAmount;

        
        timeLimit -= Time.deltaTime;
        timeLimitText.text = timeLimit.ToString("F1");

        if (timeLimit < 0)
        {
            SceneManager.LoadScene("Result");
        }

    }

    public void PushDown()
    {
        pushFrag = true;
    }

    public void PushUp()
    {
        pushFrag = false;
        pushTime = 0;
    }

    public void Complete()
    {
        airAmountDifference = airAmount/maxAirAmount;
        Debug.Log(airAmountDifference);

        //空気8割以上でOK
        if (airAmountDifference >= 0.8 && airAmountDifference < 0.9)
        {
            score += okScore;
        }
        else if (airAmountDifference >= 0.9 && airAmountDifference < 0.99)
        {
            score += goodScore;
        }
        else if (airAmountDifference >= 0.99&&airAmountDifference<=1.0)
        {
            score += perfectScore;
        }
        else if (airAmountDifference > 1.0)
        {
            score += 0;
        }

        scoreText.text = score.ToString();

        pushFrag = false;
        pushTime = 0;
        airAmount = 0;
    }
}
