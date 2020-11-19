using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    // Unity Event for Selecting & Deselecting
    [SerializeField] private UnitMovement unitMovement = null;
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

    // Make getter so it can be used in UnitCommandGiver.cs
    public UnitMovement GetUnitMovement()
    {
        return unitMovement;
    }

    #region Client
    public void Select()
    {
        if (!hasAuthority) { return; }
        onSelected?.Invoke();
    }

    public void Deselect()
    {
        if (!hasAuthority) { return; }
        onDeselected?.Invoke();
    }

    #endregion
}
