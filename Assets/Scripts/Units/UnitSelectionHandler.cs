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
    private List <Unit> selectedUnits = new List<Unit>();
 
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            
            foreach (Unit selectedUnit in selectedUnits) 
            {
                selectedUnit.Deselect();
            }

            selectedUnits.Clear();
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

        selectedUnits.Add(unit); // Add to List

        foreach(Unit selectedUnit in selectedUnits) // Select Unit
        {
            selectedUnit.Select();
        }
    }
}
