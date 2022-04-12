using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask = new LayerMask();
    private Camera mainCamera;
    private Agent selectedAgent = null;

    private void Start() {
        mainCamera = Camera.main;
    }

    private void Update() {
        if (Mouse.current.leftButton.wasReleasedThisFrame) {
            ManageSelection();
        }
    }

    private void ManageSelection() {
        if (selectedAgent != null) {
            selectedAgent.Deselect();
            selectedAgent = null;
        }
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }
        if (!hit.collider.TryGetComponent<Agent>(out Agent agent)) { return; }
        selectedAgent = agent;
        selectedAgent.Select();
    }
}
