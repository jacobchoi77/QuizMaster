using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    private ScoreKeeper _scoreKeeper;

    private void Awake(){
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        finalScoreText.text = "Congratulations!\nYou got a score of " + _scoreKeeper.CalculateScore() + "%";
    }
}