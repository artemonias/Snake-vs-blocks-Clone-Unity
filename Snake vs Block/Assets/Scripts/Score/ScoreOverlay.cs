using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreOverlay : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    [SerializeField] private TMP_Text _highScoreTextAftertDeath;

    private int _highScore;
    private int _currentScore;

    private void Start()
    {
        // ��������� ����������� HighScore �� PlayerPrefs
        _highScore = PlayerPrefs.GetInt("HighScore");
        UpdateHighScore();

        UpdateScore(0); // ���������� ������� ���� � ������ ����
    }

    public void UpdateScore(int score)
    {
        _currentScore = score;
        _scoreText.text = score.ToString();
    }

    private void UpdateHighScore()
    {
        _highScoreText.text = _highScore.ToString();
        _highScoreTextAftertDeath.text = "High Score: " + _highScore.ToString();
    }

    // ���������� ��� ������ ������
    public void GameOver()
    {
        // ���������, �������� �� ������� ���� ���� ������������ HighScore
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            // ��������� ����� HighScore � PlayerPrefs
            PlayerPrefs.SetInt("HighScore", _highScore);
            UpdateHighScore();
        }
    }
}