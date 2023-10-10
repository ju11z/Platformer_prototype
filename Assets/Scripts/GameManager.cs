using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerSpawn;
    public GameObject PlayerPrefab;
    public Camera MainCamera;

    private GameObject player_go;
    private Player player { get { return player_go.GetComponent<Player>(); } }
    private PlayerController playerController { get { return player.GetComponent<PlayerController>(); } }

    //[SerializeField] private PlayerController PlayerController;
    //[SerializeField] private Player Player;
    [SerializeField] private UIManager UIManager;

    public enum state
    {
        running, success, fail
    }

    private state currentState;

    private void Start()
    {
        HandleGameRestart();
        //и отписать
        player.PlayerDamaged += UIManager.UpdateHealth;
        player.PlayerDied += UIManager.ShowFailScreen;
        
        UIManager.RestartCommand += HandleGameRestart;
    }

    private void HandePlayerDeath()
    {
        playerController.SetPlayerIsControllable(false);
        GameObject.Destroy(player);
    }

    private async void HandleGameRestart()
    {
        player_go = GameObject.Instantiate(PlayerPrefab, PlayerSpawn);
        MainCamera.GetComponent<FollowPlayer>().target = player_go.transform;

        currentState = state.running;
        //Player.SetDefaultHealth();
        //PlayerController.SetPlayerIsControllable(true);
    }



}
