using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitBase : MonoBehaviour
{
    private NavMeshAgent _agent;
    public bool isCrouching;
    public Transform body;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (isCrouching)
        {
            body.localScale = new Vector3(1, 0.5f, 1);
        }
        else body.localScale = new Vector3(1, 1, 1);
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
