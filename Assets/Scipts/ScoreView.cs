using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private BoardPresenter presenter;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        presenter.OnScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        presenter.OnScoreChanged -= UpdateScore;
    }

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}



