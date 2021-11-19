using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    [SerializeField]
    [Tooltip("Delay in seconds before next scene starts loading")]
    private float loadSceneDelay = 0;

    public UnityEvent OnStartEventUE;

    private void Start()
    {
        OnStartEventUE?.Invoke();
    }

    public void ReloadScene(float delay)
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().name, delay));
    }

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex, loadSceneDelay));
    }

    public void LoadScene(int sceneIndex, float delay)
    {
        StartCoroutine(LoadLevel(sceneIndex, delay));
    }

    public void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex, loadSceneDelay));
    }

    public void LoadSceneAsync(int sceneIndex, float delay)
    {
        StartCoroutine(LoadLevelAsync(sceneIndex, delay));
    }

    IEnumerator LoadLevelAsync(int sceneIndex, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);

        while (!operation.isDone)
        {
            //Debug.Log(operation.progress);

            yield return null;
        }
    }

    IEnumerator LoadLevel(int sceneIndex, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    IEnumerator LoadLevel(string sceneName, float delay = 0)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
