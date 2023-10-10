using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerSpawn;
    public GameObject PlayerPrefab;
    public Camera MainCamera;
    public StartLine startLine;

    private GameObject player_go;

    private float timer;
    private Player player { get { return player_go.GetComponent<Player>(); } }
    private PlayerController playerController{ get { return player.GetComponent<PlayerController>(); } }

    //[SerializeField] private PlayerController PlayerController;
    //[SerializeField] private Player Player;
    [SerializeField] private UIManager UIManager;

    public enum state
    {
        running, sleeping, success, fail
    }

    private state currentState=state.sleeping;

    private void Start()
    {
        HandleRespawn();
        MainCamera.GetComponent<FollowPlayer>().target = player_go.transform;
        //и отписать
        player.PlayerDamaged += UIManager.UpdateHealth;
        player.PlayerDied += HandlePlayerDeath;

        startLine.PlayerCrossedStartLine += StartGame;
        
        UIManager.RestartCommand += HandleRespawn;

        
    }

    private void Update()
    {
        if (currentState == state.running)
        {
            Debug.Log("abobus");
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);

            UIManager.UpdateTimer(minutes, seconds);
            //TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
    }

    private void HandlePlayerDeath()
    {
        //playerController.SetPlayerIsControllable(false);
        //GameObject.Destroy(player);
        UIManager.ShowFailScreen();
        GameObject.Destroy(player_go);
    }

    private async void HandleRespawn()
    {
        player_go = GameObject.Instantiate(PlayerPrefab, PlayerSpawn);
        //player = player_go.GetComponent<Player>();
        //playerController= player_go.GetComponent<PlayerController>();
        //player_go.transform.position=PlayerSpawn.position;
        player.SetDefaultHealth();
        playerController.SetPlayerIsControllable(true);
        //player = player_go.GetComponent<Player>();
        //playerController = player_go.GetComponent<PlayerController>();

        Debug.Log(player_go.GetInstanceID());
        MainCamera.GetComponent<FollowPlayer>().target = player_go.transform;
        UIManager.HideFailScreen();
        UIManager.UpdateHealth(player.MaxHealth, 0, player.MaxHealth, 0);

        //currentState = state.running;
        //Player.SetDefaultHealth();
        
    }

    private void StartGame()
    {
        Debug.Log("start game");
        timer = 0f;
        currentState = state.running;
    }

    private void FinishGame()
    {
        currentState = state.success;
    }



}
