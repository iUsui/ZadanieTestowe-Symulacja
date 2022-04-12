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
    private bool canSpawn = true;
    
    private void OnEnable() {
        Agent.OnAgentSpawned += HandleOnAgentSpawned;
        Agent.OnAgentDespawned += HandleOnAgentDespawned;
    }
    private void OnDisable() {
        Agent.OnAgentSpawned -= HandleOnAgentSpawned;
        Agent.OnAgentDespawned -= HandleOnAgentDespawned;
    }

    private void HandleOnAgentSpawned(Agent agent)
    {
        spawnedAgents.Add(agent);
        Debug.Log($"Agent {spawnedAgents.Count} spawned\n");
    }

    private void HandleOnAgentDespawned(Agent agent)
    {
        spawnedAgents.Remove(agent);
    }

    private void Start() {
        if (maxTimeToSpawn < minTimeToSpawn) { 
            this.enabled = false;
            throw new Exception("Max time to spawn has to be bigger than min time");
        }
    }

    private void Update() {
        if (canSpawn && spawnedAgents.Count < maxAgentsOnTheField) {
            StartCoroutine(SetCanSpawn());
            float borderOffset = 3.0f;
            Vector3 bottomLeftPoint = bottomLeftCornerPoint.position;
            Vector3 topRightPoint = topRightCornerPoint.position;
            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(bottomLeftPoint.x + borderOffset, topRightPoint.x - borderOffset),
                0f,
                UnityEngine.Random.Range(bottomLeftPoint.z + borderOffset, topRightPoint.z - borderOffset)
            );
            Instantiate(agentPrefab, spawnPosition, agentPrefab.transform.rotation);
        }
    }
    
    private IEnumerator SetCanSpawn() {
        canSpawn = false;
        float randomTime = UnityEngine.Random.Range(minTimeToSpawn, maxTimeToSpawn);
        yield return new WaitForSeconds(randomTime);
        canSpawn = true;
    }
}
