using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Slider slider = null;
    private int currentHealth;
    public event Action OnDie;

    private void Start() {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int value) {
        if (currentHealth <= 0) { return; }
        currentHealth -= value;
        slider.value = currentHealth;
        if (currentHealth > 0) { return; }
        OnDie?.Invoke();
    }
}
