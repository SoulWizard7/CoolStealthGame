using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveUnit(Vector3 pos)
    {
        _agent.destination = pos;
    }

    private void OnMouseUp()
    {
        print("Clicked on unit");
        
        PlayerUnitHandler.instance.currentUnits.Clear();
        PlayerUnitHandler.instance.currentUnits.Add(this);
    }
}
