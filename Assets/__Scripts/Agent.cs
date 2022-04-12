using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5.0f;

    public static event Action<Agent> OnAgentSpawned;
    public static event Action<Agent> OnAgentDespawned;

    private void Start() {
        OnAgentSpawned?.Invoke(this);
    }

    private void OnDestroy() {
        OnAgentDespawned?.Invoke(this);
    }
}
