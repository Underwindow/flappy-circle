using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviourSingleton<Spawner>, IPauseEventListener
{
    [SerializeField] private GameObject[] wallPrefabs;
    [SerializeField] private float wallsOffset;

    private Coroutine spawningCoroutine;
    private Wall currentWall;
    private int totalWalls = 0;
    private float startOffset;

    public delegate void WallSpawnEvent(int totalSpawned);
    public event WallSpawnEvent OnWallSpawn;

    private void Start()
    {
        startOffset = transform.position.x;
        GameManager.Instance.OnGameOver += StopSpawn;

        spawningCoroutine = StartCoroutine(SpawnWalls(1));
    }

    private void Update()
    {
        transform.position = new Vector3(PlayerController.Instance.PlayerPos.x + startOffset, 0, 0);
    }

    public void Spawn()
    {
        float holeSize;
        GameObject prefab = wallPrefabs[0];

        switch (DifficultyManager.Instance.Difficulty)
        {
            case DifficultyType.MEDIUM:
                holeSize = 2.25f;
                break;
            case DifficultyType.HARD:
                holeSize = 2f;
                prefab = wallPrefabs[Random.Range(0, 2)];
                break;
            case DifficultyType.INSANE:
                holeSize = 2f;
                prefab = wallPrefabs[Random.Range(0, wallPrefabs.Length)];
                break;
            default:
                holeSize = 3f;
                break;
        }

        currentWall = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<Wall>();
        currentWall.transform.position += Vector3.up * Random.Range(-2f, 2f);
        currentWall.SetHoleSize(holeSize);
        
        OnWallSpawn?.Invoke(++totalWalls);
    }

    IEnumerator SpawnWalls(float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("SpawnWalls");
        Spawn();

        while (true)
        {
            if (Vector3.Distance(currentWall.transform.position, transform.position) > wallsOffset)
                Spawn();

            yield return null;
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(spawningCoroutine);
        enabled = false;
    }

    public void Pause()
    {
    }

    public void Resume()
    {
    }
}