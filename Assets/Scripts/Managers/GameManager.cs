using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    public delegate void GameOverEvent();
    public event GameOverEvent OnGameOver;
    public delegate void RestartEvent();
    public event RestartEvent OnRestart;

    public UnityEvent OnGameOverUE, OnRestartUE;

    private void Start()
    {
        PlayerController.Instance.OnPlayerHitWall += GameOver;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        OnGameOverUE?.Invoke();

        PlayerController.Instance.OnPlayerHitWall -= GameOver;
    }

    public void Share()
    {
        AppsFlyerManager.Instance.UserPostsShare();
    }

    public void Restart()
    {
        OnRestart?.Invoke();
        OnRestartUE?.Invoke();

        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
    public void Menu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}

public enum DifficultyType
{
    EASY,
    MEDIUM,
    HARD,
    INSANE
}