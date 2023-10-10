using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Player Player;
    [SerializeField] private UIManager UIManager;

    public enum state
    {
        running, success, fail, sleep
    }

    private void Start()
    {
        Player.PlayerDamaged += UIManager.UpdateHealth;
    }

    private void HandePlayerDeath()
    {
        PlayerController.SetPlayerIsControllable(false);
    }

    private async void HandleGameRestart()
    {
        PlayerController.SetPlayerIsControllable(true);
    }

}
