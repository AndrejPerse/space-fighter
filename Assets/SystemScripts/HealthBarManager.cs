using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//manages health bar of the player
public class HealthBarManager : MonoBehaviour
{
    private Image healthBar;
    public float currentHealth;
    private float maxHealth = 100.0f;
    private PlayerController playerControllerScript;

    void Start()
    {
        healthBar = GetComponent<Image>();
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        currentHealth = playerControllerScript.shipHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
