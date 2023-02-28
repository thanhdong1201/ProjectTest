using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSO", menuName = "Score")]
public class ScoreSO : ScriptableObject
{
    [SerializeField] private int highestScore;
    [SerializeField] private int currentScore;

    public int HighestScore => highestScore;
    public int CurrentScore => currentScore;

    public void ResetScore()
    {
        currentScore = 0;
    }
    public void AddScore(int score)
    {
        currentScore += score;

        if (currentScore >= highestScore)
        {
            highestScore = currentScore;
        }
    }
}
