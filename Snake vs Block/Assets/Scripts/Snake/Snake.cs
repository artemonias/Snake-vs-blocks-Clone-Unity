using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

[RequireComponent(typeof(TailGenerator))]
[RequireComponent(typeof(SnakeInput))]
public class Snake : MonoBehaviour
{
    [SerializeField] private SnakeHead _head;
    [SerializeField] private int _tailSize;
    [SerializeField] private float _speed;
    [SerializeField] private float _tailSpringiness;
    [SerializeField] private ScoreOverlay _scoreOverlay;

    private SnakeInput _input;
    private List<Segment> _tail;
    private TailGenerator _tailGenerator;
    private int _score;

    public event UnityAction<int> SizeUpdated;
    public DeathMenu _deathMenu;

    private void Awake()
    {
        _tailGenerator = GetComponent<TailGenerator>();

        _input = GetComponent<SnakeInput>();
        _tail = _tailGenerator.Generate(_tailSize);
        SizeUpdated?.Invoke(_tail.Count);
    }
    private void OnEnable()
    {
        _head.BlockCollided += OnBlockCollided;
        _head.BonusCollected += OnBonusCollected;
    }
    private void OnDisable()
    {
        _head.BlockCollided -= OnBlockCollided;
        _head.BonusCollected -= OnBonusCollected;
        _scoreOverlay.GameOver();
    }
    private void FixedUpdate()
    {
        Move(_head.transform.position + _head.transform.up * _speed * Time.fixedDeltaTime);
        _head.transform.up = _input.GetDirectionToClick(_head.transform.position);
    }
    private void Move(Vector3 nextPosition)
    {
        Vector3 previousPosition = _head.transform.position;
        foreach (var segment in _tail)
        {
            Vector3 tempPosition = segment.transform.position;
            segment.transform.position = Vector2.Lerp(segment.transform.position, previousPosition, _tailSpringiness * Time.deltaTime);
            previousPosition = tempPosition;
        }
        _head.Move(nextPosition);
    }
    private void OnBlockCollided()
    {
        Segment deletedSegment = _tail[_tail.Count - 1];
        _tail.Remove(deletedSegment);
        Destroy(deletedSegment.gameObject);
        SizeUpdated?.Invoke(_tail.Count);
        if (_tail.Count == 0)
        {
            Destroy(gameObject);
            _deathMenu.gameObject.SetActive(true);
        }

    }

    private void OnBonusCollected(int bonusSize)
    {
        _tail.AddRange(_tailGenerator.Generate(bonusSize));
        SizeUpdated?.Invoke(_tail.Count);
        _score += bonusSize; // Увеличиваем счет на размер бонуса
                             // Обновляем текстовое поле счета на оверлее в Canvas
        _scoreOverlay.UpdateScore(_score);
    }
}
