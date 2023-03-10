using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour{
    [Header("Questions")]
    [SerializeField]
    private TextMeshProUGUI questionText;

    [SerializeField]
    private List<QuestionSo> questions = new();

    [Header("Answers")]
    [SerializeField]
    private GameObject[] answerButtons;

    [Header("Button Colors")]
    [SerializeField]
    private Sprite defaultAnswerSprite;

    [SerializeField]
    private Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField]
    private Image timerImage;

    [Header("Scoring")]
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [Header("ProgressBar")]
    [SerializeField]
    private Slider progressBar;

    [NonSerialized]
    public bool IsComplete;

    private QuestionSo _currentQuestion;
    private int _correctAnswerIndex;
    private bool _hasAnsweredEarly = true;
    private Timer _timer;
    private ScoreKeeper _scoreKeeper;

    private void Awake(){
        _timer = FindObjectOfType<Timer>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update(){
        timerImage.fillAmount = _timer.FillFraction;
        if (_timer.LoadNextQuestion){
            if (Math.Abs(progressBar.value - progressBar.maxValue) < 0.001f){
                IsComplete = true;
                return;
            }

            _hasAnsweredEarly = false;
            GetNextQuestion();
            _timer.LoadNextQuestion = false;
        }
        else if (!_hasAnsweredEarly && !_timer.IsAnsweringQuestion){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index){
        _hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        _timer.CancelTimer();
        scoreText.text = "Score: " + _scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index){
        Image buttonImage;
        if (index == _currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            _scoreKeeper.IncrementCorrectAnswers();
        }
        else{
            _correctAnswerIndex = _currentQuestion.GetCorrectAnswerIndex();
            var correctAnswer = _currentQuestion.GetAnswer(_correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            buttonImage = answerButtons[_correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    private void GetNextQuestion(){
        if (questions.Count <= 0) return;
        SetButtonState(true);
        SetDefaultButtonSprites();
        GetRandomQuestion();
        DisplayQuestion();
        progressBar.value++;
        _scoreKeeper.IncrementQuestionsSeen();
    }

    private void GetRandomQuestion(){
        var index = Random.Range(0, questions.Count);
        _currentQuestion = questions[index];

        if (questions.Contains(_currentQuestion)){
            questions.Remove(_currentQuestion);
        }
    }

    private void DisplayQuestion(){
        questionText.text = _currentQuestion.GetQuestion();

        for (var i = 0; i < answerButtons.Length; i++){
            var buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = _currentQuestion.GetAnswer(i);
        }
    }

    private void SetButtonState(bool state){
        foreach (var answerButton in answerButtons){
            var button = answerButton.GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites(){
        foreach (var answerButton in answerButtons){
            var buttonImage = answerButton.GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}