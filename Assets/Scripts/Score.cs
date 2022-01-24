using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Board _board;

    private int _score = 0;

    public void AddScore(int value)
    {
        _score += value;
        _scoreText.text = _score.ToString();
    }

    public void Restart()
    {
        _score = 0;
        _scoreText.text = _score.ToString();
    }
}
