using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviourSingleton<Score>
{
    [SerializeField] private TextMeshProUGUI finalScore;
    [SerializeField] private TextMeshProUGUI bestScore;

    public delegate void ScoreCollectEvent(int score);
    public event ScoreCollectEvent OnScoreCollect;

    public delegate void NewBestScoreEvent(int score);
    public event NewBestScoreEvent OnBestScoreEvent;

    public UnityEvent OnScoreCollectUE;

    private int score = 0;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = score.ToString();

        GameManager.Instance.OnGameOver += SaveScore;
    }

    public void Collect()
    {
        score++;
        text.text = score.ToString();

        OnScoreCollect?.Invoke(score);
        OnScoreCollectUE?.Invoke();
    }

    private void SaveScore()
    {
        int best = PlayerPrefs.GetInt("BEST_SCORE", 0);

        if (best < score)
        {
            PlayerPrefs.SetInt("BEST_SCORE", best = score);
            OnBestScoreEvent?.Invoke(score);
        }

        finalScore.text = score.ToString();
        bestScore.text = best.ToString();
    }
}
