using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviourSingleton<DifficultyManager>
{
    [SerializeField] private TextMeshProUGUI difficulty;

    private DifficultyType _difficulty;

    public DifficultyType Difficulty {
        get { 
            return _difficulty; 
        }
        private set { 
            difficulty.text = (_difficulty = value).ToString();
            AppsFlyerManager.Instance.UserCompletesLevel(_difficulty);
        }
    }
    
    private void Start()
    {
        Difficulty = DifficultyType.EASY;
        difficulty.text = Difficulty.ToString();
        Spawner.Instance.OnWallSpawn += UpdateDifficulty;
    }

    private void UpdateDifficulty(int wallsAmount)
    {
        switch (wallsAmount)
        {
            case int wa when wa == 10:
                Difficulty = DifficultyType.MEDIUM;
                break;
            case int wa when wa == 20:
                Difficulty = DifficultyType.HARD;
                break;
            case int wa when wa == 30:
                Difficulty = DifficultyType.INSANE;
                Spawner.Instance.OnWallSpawn -= UpdateDifficulty;
                break;
        }
    }
}
