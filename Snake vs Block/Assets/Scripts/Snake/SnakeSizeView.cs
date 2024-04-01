using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;

[RequireComponent(typeof(Snake))]
public class SnakeSizeView : MonoBehaviour
{
    [SerializeField] TMP_Text _view;

    private Snake _snake;
    private void Awake()
    {
        _snake = GetComponent<Snake>();
    }

    private void OnEnable()
    {
        _snake.SizeUpdated += OnSizeUpdated;
    }
    private void OnDisable()
    {
        _snake.SizeUpdated -= OnSizeUpdated;
    }
    private void OnSizeUpdated(int size)
    {
        _view.text = size.ToString();
    }
    
}
