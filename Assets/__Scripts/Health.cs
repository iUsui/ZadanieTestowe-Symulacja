using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public event Action OnDie;
    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int value) {
        if (currentHealth <= 0) { return; }
        currentHealth -= value;
        if (currentHealth > 0) { return; }
        OnDie?.Invoke();
    }
}
