using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private ScoreSO scoreSO;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreSO.ResetScore();
    }
    private void Update()
    {
        scoreText.SetText("Score: " + scoreSO.CurrentScore);

    }

}
