using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : NetworkBehaviour
{
    // Unity Event for Selecting & Deselecting
    [SerializeField] private UnityEvent onSelected = null;
    [SerializeField] private UnityEvent onDeselected = null;

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
