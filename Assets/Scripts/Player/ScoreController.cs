using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    public int Score { get { return score; } }

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "" + score;
    }

    public void Increment(int value = 1)
    {
        score += value;
    }

}
