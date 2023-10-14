using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform PlayerSpawn;
    public GameObject PlayerPrefab;
    public Camera MainCamera;
    public StartLine startLine;
    public FinishLine finishLine;

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
        player_go = GameObject.Instantiate(PlayerPrefab, PlayerSpawn);

        player.SetDefaultHealth();
        playerController.SetPlayerIsControllable(true);


        Debug.Log(player_go.GetInstanceID());
        MainCamera.GetComponent<FollowPlayer>().target = player_go.transform;
        //и отписать
        player.PlayerDamaged += UIManager.UpdateHealth;
        player.PlayerDied += HandlePlayerDeath;

        startLine.PlayerCrossedStartLine += StartGame;
        finishLine.PlayerCrossedFinishLine += FinishGame;
        
        UIManager.RestartCommand += HandleRespawn;

        
    }

    

    private void Update()
    {
        if (currentState == state.running)
        {
            //Debug.Log("abobus");
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);

            UIManager.UpdateTimer(minutes, seconds);
            //TimerText.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
        }
    }

    private void SetGameState(state state)
    {
        this.currentState = state;
    }

    private void HandlePlayerDeath()
    {
        //playerController.SetPlayerIsControllable(false);
        //GameObject.Destroy(player);
        SetGameState(state.fail);
        UIManager.ShowFailScreen();

        playerController.SetPlayerIsControllable(false);
        //GameObject.Destroy(player_go);
    }

    private async void HandleRespawn()
    {
        timer = 0f;

        player.SetDefaultHealth();
        player_go.transform.position = PlayerSpawn.position;
        //player_go = null;

        player.ResetAllForces();
        

        UIManager.HideFailScreen();
        UIManager.HideFinishScreen();
        UIManager.UpdateHealth(player.MaxHealth, 0, player.MaxHealth, 0);
        UIManager.UpdateTimer(0, 0);

        playerController.SetPlayerIsControllable(true);


    }

    private void StartGame()
    {
        Debug.Log("start game");
        timer = 0f;
        SetGameState(state.running);
    }

    private void FinishGame()
    {
        SetGameState(state.success);

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);

        UIManager.ShowFinishScreen(minutes,seconds);

        playerController.SetPlayerIsControllable(false);
    }



}
