using System;
using UnityEngine;

public class Timer : MonoBehaviour{
    [SerializeField]
    private float timeToCompleteQuestion = 30f;

    [SerializeField]
    private float timeToShowCorrectAnswer = 10f;

    [NonSerialized]
    public bool LoadNextQuestion;

    [NonSerialized]
    public float FillFraction;

    [NonSerialized]
    public bool IsAnsweringQuestion;

    private float _timerValue;

    private void Update(){
        UpdateTimer();
    }

    public void CancelTimer(){
        _timerValue = 0;
    }

    private void UpdateTimer(){
        _timerValue -= Time.deltaTime;

        if (IsAnsweringQuestion){
            if (_timerValue > 0){
                FillFraction = _timerValue / timeToCompleteQuestion;
            }
            else{
                IsAnsweringQuestion = false;
                _timerValue = timeToShowCorrectAnswer;
            }
        }
        else{
            if (_timerValue > 0){
                FillFraction = _timerValue / timeToShowCorrectAnswer;
            }
            else{
                IsAnsweringQuestion = true;
                _timerValue = timeToCompleteQuestion;
                LoadNextQuestion = true;
            }
        }
    }
}