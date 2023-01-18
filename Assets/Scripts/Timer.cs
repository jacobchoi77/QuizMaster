using UnityEngine;

public class Timer : MonoBehaviour{
    [SerializeField] private float timeToCompleteQuestion = 30f;
    [SerializeField] private float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion;

    private float _timerValue;

    private void Update(){
        UpdateTimer();
    }

    public void CancelTimer(){
        _timerValue = 0;
    }

    private void UpdateTimer(){
        _timerValue -= Time.deltaTime;

        if (isAnsweringQuestion){
            if (_timerValue > 0){
                fillFraction = _timerValue / timeToCompleteQuestion;
            }
            else{
                isAnsweringQuestion = false;
                _timerValue = timeToShowCorrectAnswer;
            }
        }
        else{
            if (_timerValue > 0){
                fillFraction = _timerValue / timeToShowCorrectAnswer;
            }
            else{
                isAnsweringQuestion = true;
                _timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}