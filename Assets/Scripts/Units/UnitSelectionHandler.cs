using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionHandler : MonoBehaviour
{
    // What we can hit or can't hit
    [SerializeField] private LayerMask layerMask = new LayerMask();
    // reference to Main Camera
    private Camera mainCamera;

    // Store all the unit currently selected 
    // Make List public so that UnitCommandGiver.cs can access
    public List<Unit> SelectedUnits { get; } = new List<Unit>(); // List of Units
 
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            foreach (Unit selectedUnit in SelectedUnits) 
            {
                selectedUnit.Deselect();
            }

            SelectedUnits.Clear();
        }
        else if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            ClearSelectionArea();
        }
    }

    private void ClearSelectionArea()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask)) { return; }

        if (!hit.collider.TryGetComponent<Unit>(out Unit unit)) { return; } // if not unit, return

        if (!unit.hasAuthority) { return; }

        SelectedUnits.Add(unit); // Add to List

        foreach(Unit selectedUnit in SelectedUnits) // Select Unit
        {
            selectedUnit.Select();
        }
    }
}
