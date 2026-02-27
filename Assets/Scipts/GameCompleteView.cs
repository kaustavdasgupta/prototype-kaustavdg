using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCompleteView : MonoBehaviour
{
    [SerializeField] private BoardPresenter presenter;
    [SerializeField] private TMP_Text finalScoreText;

    private void Awake()
    {
        finalScoreText.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        presenter.OnGameComplete += ShowFinalScore;
        presenter.OnGameStarted += HidePanel;
    }

    private void OnDisable()
    {
        presenter.OnGameComplete -= ShowFinalScore;
        presenter.OnGameStarted -= HidePanel;
    }

    private void ShowFinalScore(int score)
    {
        finalScoreText.gameObject.SetActive(true);
        finalScoreText.text = $"Total Score: {score}";
    }

    private void HidePanel()
    {
        finalScoreText.gameObject.SetActive(false);
    }
}


