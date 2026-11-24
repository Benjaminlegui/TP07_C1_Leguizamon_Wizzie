using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { Playing, GameOver }

    [SerializeField] private Player player;
    // Error: En SampleScene.unity:2712 spawners tiene un único elemento en None y EnemySpawner no está en la escena.
    // La aceleración de spawn de EnemySpawner (única modificación de jugabilidad) nunca corre, y no hay power ups: el requisito queda sin cumplir.
    [SerializeField] private SpawnerBase[] spawners;

    private Health playerHealth;

    public GameState State { get; private set; } = GameState.Playing;
    public bool IsPlaying => State == GameState.Playing;
    public event Action Changed;

    private void Awake() => playerHealth = player.GetComponent<Health>();

    private void OnEnable() => playerHealth.Died += EndGame;
    private void OnDisable() => playerHealth.Died -= EndGame;

    public void EndGame()
    {
        if (!IsPlaying) return;
        State = GameState.GameOver;
        foreach (SpawnerBase spawner in spawners)
            if (spawner != null) spawner.StopSpawning();
        player.enabled = false;
        Time.timeScale = 0f;
        Changed?.Invoke();
    }

    public void RestartGame() => SceneLoader.LoadGameplay();
}
