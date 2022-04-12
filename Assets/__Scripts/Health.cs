using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;

public class Health : MonoBehaviour
{
    [Tooltip("Can go up to 3!")]
    [SerializeField] private List<Color> colorPalette = new List<Color>(3);
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private Slider slider = null;
    [SerializeField] private TMP_Text healthTextDisplay = null;
    [SerializeField] private List<Renderer> renderers = new List<Renderer>();
    private int currentHealth;
    public event Action OnDie;

    private void Start() {
        if (colorPalette.Count > 3) { throw new Exception("Color palette size has to be 3!"); }
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        currentHealth = maxHealth;
        SetRenderers();
        healthTextDisplay.text = $"Health {currentHealth}/{maxHealth}";
    }

    public void TakeDamage(int value) {
        if (currentHealth <= 0) { return; }
        currentHealth -= value;
        slider.value = currentHealth;
        if (currentHealth != 0) { SetRenderers(); }
        healthTextDisplay.text = $"Health {currentHealth}/{maxHealth}";
        if (currentHealth > 0) { return; }
        OnDie?.Invoke();
    }

    private void SetRenderers() {
        foreach (var renderer in renderers) {
            renderer.material.SetColor("_BaseColor", colorPalette[currentHealth-1]);
        }
    }
}
