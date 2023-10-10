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

    public Image HealthProgeressBar;

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





}
