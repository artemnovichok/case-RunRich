using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    public float CoefficientResult => _coefficientResult;

    //[SerializeField] private SoundController _soundController;
   // [SerializeField] private LevelComplete _levelComplete;
    [SerializeField] private List<GameObject> _coefficientPrefabs = new List<GameObject>();
    [SerializeField] private List<float> _coefficients = new List<float> { 1.5f, 2f, 3f, 4f, 3f, 2f, 1.5f };
    [SerializeField] private float _animationDuration = 0.2f;

    [SerializeField] private Text _countText;
    [SerializeField] private GameObject _pointer;

    private int _currentCoefficientIndex;
    private int _animationDirection = 1;
    private float _coefficientResult;
    private bool _isPlay = true;

    private readonly List<GameObject> _coefficientObjects = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < _coefficients.Count; i++)
        {
            GameObject newCoefficientObject = Instantiate(_coefficientPrefabs[i], transform);
            newCoefficientObject.GetComponentInChildren<Text>().text = "x" + _coefficients[i].ToString();
            _coefficientObjects.Add(newCoefficientObject);
        }

        _currentCoefficientIndex = 0;
    }

    private async void OnEnable()
    {
       // _soundController.PlayRandomSound();
        _pointer.transform.position = new Vector3(_coefficientObjects[0].transform.position.x, _pointer.transform.position.y, _pointer.transform.position.z);
        _isPlay = true;
        _currentCoefficientIndex = 0;
        _animationDirection = 1;
        await UniTask.Delay(TimeSpan.FromSeconds(_animationDuration));
        await AnimateRoulette();
    }

    private void OnDisable()
    {
        _isPlay = false;
        _coefficientObjects[_currentCoefficientIndex].transform.localScale = Vector3.one;
        _currentCoefficientIndex = 0;
        _animationDirection = 1;
    }

    private async UniTask AnimateRoulette()
    {
        while (_isPlay)
        {
            _coefficientObjects[_currentCoefficientIndex].transform.DOScale(Vector3.one * 1.2f, _animationDuration).SetEase(Ease.InOutQuad).WaitForCompletion();

            float pointerTargetPositionX = _coefficientObjects[_currentCoefficientIndex].transform.position.x;
            _pointer.transform.DOMoveX(pointerTargetPositionX, _animationDuration).SetEase(Ease.InOutQuad).WaitForCompletion();

            await UniTask.Delay(TimeSpan.FromSeconds(_animationDuration));

            _coefficientObjects[_currentCoefficientIndex].transform.DOScale(Vector3.one, _animationDuration).SetEase(Ease.InOutQuad).WaitForCompletion();

            await UniTask.Delay(TimeSpan.FromSeconds(_animationDuration));

            _currentCoefficientIndex += _animationDirection;

            if (_currentCoefficientIndex >= _coefficients.Count - 1)
            {
                _animationDirection = -1;
            }
            else if (_currentCoefficientIndex <= 0)
            {
                _animationDirection = 1;
            }
           // _countText.text = (_levelComplete.LastScore * _coefficients[_currentCoefficientIndex]).ToString();
        }
    }

    public float GetCurrentCoefficient()
    {
        _coefficientResult = _coefficients[_currentCoefficientIndex];
        _isPlay = false;
        return _coefficientResult;
    }
}