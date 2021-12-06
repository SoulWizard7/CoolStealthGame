using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnitHandler : MonoBehaviour
{
    public static PlayerUnitHandler instance { get; private set; }
    
    public List<UnitBase> currentUnits;

    public float findNavmeshFromClickDistance = 0.2f;

    [SerializeField] private LayerMask groundLayerMask;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    
    private void Update()
    {
        ClickToMove();
        DeselectUnit();
    }
    
    private void OnGUI()
    {
        if(currentUnits.Count != 0) GUI.TextArea(new Rect(0, 0, 200, 50), "Unit Selected: " + currentUnits[0].name);
    }


    private void ClickToMove()
    {
        if (currentUnits.Count == 0) return;
        
        if (Input.GetMouseButtonDown(1)) 
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, groundLayerMask);

            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, findNavmeshFromClickDistance, NavMesh.AllAreas))
            {
                foreach (UnitBase unit in currentUnits)
                {
                    unit.MoveUnit(hit.point);
                }
            }
        }
    }
    private void DeselectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentUnits.Clear();
        }
    }
}
