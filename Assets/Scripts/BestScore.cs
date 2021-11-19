using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{
    private TextMeshProUGUI score;

    void Start()
    {
        score = GetComponent<TextMeshProUGUI>();

        var bestScore = PlayerPrefs.GetInt("BEST_SCORE", 0);

        if (bestScore == 0)
            gameObject.SetActive(false);
        else
            score.text = $"BEST SCORE\n<sprite=1 color=#FFC158> <b>{bestScore}</b> <sprite=1 color=#FFC158>";
    }
}
