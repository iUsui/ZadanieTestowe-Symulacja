using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    public static AgentManager instance;
    [SerializeField] private GameObject agentPrefab = null;
    [Header("Spawn points")]
    [SerializeField] private Transform bottomLeftCornerPoint = null;
    [SerializeField] private Transform topRightCornerPoint = null;
    [Header("Agent spawn frequency")]
    [SerializeField] private float minTimeToSpawn = 2.0f;
    [SerializeField] private float maxTimeToSpawn = 10.0f;
    [Header("Agent statistics")]
    [SerializeField] private TMP_Text spawnedAgentsText = null;
    [SerializeField] private TMP_Text diedAgentsText = null;
    [SerializeField] private TMP_Text aliveAgentsText = null;

    [Header("Other properties")]
    [SerializeField] private int maxAgentsOnTheField = 30;
    private List<Agent> spawnedAgents = new List<Agent>();
    private bool canSpawn = true;
    private int spawnedAgentCounter = 0;
    private int diedAgentsCounter = 0;

    private void OnEnable() {
        Agent.OnAgentSpawned += HandleOnAgentSpawned;
        Agent.OnAgentDespawned += HandleOnAgentDespawned;
    }
    private void OnDisable() {
        Agent.OnAgentSpawned -= HandleOnAgentSpawned;
        Agent.OnAgentDespawned -= HandleOnAgentDespawned;
    }

    private void HandleOnAgentSpawned(Agent agent) {
        spawnedAgentCounter++;
        agent.SetAgentName($"Agent {spawnedAgentCounter}");
        spawnedAgents.Add(agent);
        spawnedAgentsText.text = $"Spawned agents: {spawnedAgentCounter}";
        aliveAgentsText.text = $"Alive agents: {spawnedAgents.Count}";
    }
    private void HandleOnAgentDespawned(Agent agent) {
        diedAgentsCounter++;
        spawnedAgents.Remove(agent);
        diedAgentsText.text = $"Died agents: {diedAgentsCounter}";
        aliveAgentsText.text = $"Alive agents: {spawnedAgents.Count}";
    }

    private void Awake() {
        instance = this;
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
            float borderOffset = 5.0f;
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
