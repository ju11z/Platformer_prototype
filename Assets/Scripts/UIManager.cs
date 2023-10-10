using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public event Action StartCommand;
    public event Action ReplayCommand;

    public Button FailRestartButton;

    public event Action RestartCommand;

    public RectTransform FailPanel;

    public Image HealthProgeressBar;

    private void Start()
    {
        FailPanel.gameObject.SetActive(false);

        FailRestartButton.onClick.AddListener(HandleRestartButtonClicked);
    }

    public void HandleRestartButtonClicked()
    {
        RestartCommand.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCommand.Invoke();
        }
    }
    public void UpdateHealth(int previousHealth, int healthChange, int currentHealth, int maxHealth)
    {
        //Debug.Log((float)(currentHealth / maxHealth));
        HealthProgeressBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }

    public void ShowFailScreen()
    {
        FailPanel.gameObject.SetActive(true);
    }
    public void HideFailScreen()
    {
        FailPanel.gameObject.SetActive(false);
    }





}
