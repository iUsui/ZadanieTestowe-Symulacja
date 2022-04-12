using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Agent : MonoBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Health health = null;
    [SerializeField] private TMP_Text nameText = null;
    [SerializeField] private float movementSpeed = 5.0f;
    

    private string agentName = "Unnamed";
    public static event Action<Agent> OnAgentSpawned;
    public static event Action<Agent> OnAgentDespawned;
    [SerializeField] private UnityEvent OnSelected;
    [SerializeField] private UnityEvent OnDeselected;

    private void OnEnable() {
        health.OnDie += HandleOnDie;
    }
    private void OnDisable() {
        health.OnDie -= HandleOnDie;
    }

    public string GetAgentName() {
        return agentName;
    }
    public void SetAgentName(string newAgentName) {
        agentName = newAgentName;
    }

    private void Start() {
        OnAgentSpawned?.Invoke(this);
        nameText.text = agentName;
        ChangeRotation();
    }
    private void OnDestroy() {
        OnAgentDespawned?.Invoke(this);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Wall")) {
            ChangeRotation(true);
            return;
        }
        if (!other.gameObject.TryGetComponent<Health>(out Health enemyHealth)) { return; }
        ChangeRotation();
        health.TakeDamage(1);
    }

    private void ChangeRotation() {
        rb.freezeRotation = false;
        Vector3 euler = transform.eulerAngles;
        euler.y = UnityEngine.Random.Range(0f, 360f);
        transform.eulerAngles = euler;
        rb.freezeRotation = true;
        rb.velocity = transform.forward * movementSpeed;
    }
    private void ChangeRotation(bool wall) {
        rb.freezeRotation = false;
        Vector3 euler = transform.eulerAngles;
        euler.y -= 180f;
        transform.eulerAngles = euler;
        rb.freezeRotation = true;
        rb.velocity = transform.forward * movementSpeed;
    }

    private void HandleOnDie()
    {
        Debug.Log($"{gameObject.name} died..");
        Destroy(gameObject);
    }

    public void Select() {
        OnSelected?.Invoke();
    }
    public void Deselect() {
        OnDeselected?.Invoke();
    }

    private void Update() {
        if (rb.velocity.magnitude < movementSpeed) {
            rb.velocity = transform.forward * movementSpeed;
        }
    }
}
