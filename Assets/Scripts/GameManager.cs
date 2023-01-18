using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    private Quiz _quiz;
    private EndScreen _endScreen;

    private void Awake(){
        _quiz = FindObjectOfType<Quiz>();
        _endScreen = FindObjectOfType<EndScreen>();
    }

    private void Start(){
        _quiz.gameObject.SetActive(true);
        _endScreen.gameObject.SetActive(false);
    }

    private void Update(){
        if (!_quiz.isComplete) return;
        _quiz.gameObject.SetActive(false);
        _endScreen.gameObject.SetActive(true);
        _endScreen.ShowFinalScore();
    }

    public void OnReplayLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}