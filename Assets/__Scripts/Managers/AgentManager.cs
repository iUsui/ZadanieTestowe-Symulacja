using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab = null;
    [Header("Spawn points")]
    [SerializeField] private Transform bottomLeftCornerPoint = null;
    [SerializeField] private Transform topRightCornerPoint = null;
    [Header("Agent spawn frequency")]
    [SerializeField] private float minTimeToSpawn = 2.0f;
    [SerializeField] private float maxTimeToSpawn = 10.0f;

    [Header("Other properties")]
    [SerializeField] private int maxAgentsOnTheField = 30;
    private List<Agent> spawnedAgents = new List<Agent>();
    

    private void Start() {
        if (maxTimeToSpawn < minTimeToSpawn) { 
            throw new Exception("Max time to spawn has to be bigger than min time"); 
        }
    }
}
