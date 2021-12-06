using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : EnemyBase
{
    public Transform eyes;
    [Range(0, 180)] public float viewAngle; // Change values to match "ConeOfSightRenderer" values
    [Range(0, 50)] public float viewDistance; //

    public GameObject player;

    public bool gizmos;
    public bool dontPatrol;
    public GameObject playerIsStandingHitBox;
    public GameObject playerIsCrouchingHitBox;


    protected override void Update()
    {
        base.Update();
        DetectEnemies();
    }

    private void DetectEnemies()
    {
        Vector3 playerPos = (player.transform.position - eyes.position).normalized;
        float dotProduct = Vector3.Dot( eyes.transform.forward, playerPos);
        
        if (dotProduct < 0) return; // player is behind unit

        float distance = Vector3.Distance(eyes.position, player.transform.position);
        
        if(distance > viewDistance) return; // player is to far away
        
        float ang = Mathf.Acos(dotProduct) * Mathf.Rad2Deg;
        RaycastHit hit;

        if (ang < (viewAngle / 2))
        {
            print("inside FOV");


            float crouch = 0;
            UnitBase pl = player.GetComponent<UnitBase>();

            if (pl.isCrouching) crouch = 1f;
            
            
            Vector3 lookTowardsPlayer = (playerIsStandingHitBox.transform.position - eyes.position).normalized;
            
            Vector3 lookHeight = new Vector3(eyes.transform.position.x, eyes.transform.position.y - crouch, eyes.transform.position.z);
            Vector3 playerXZpos = new Vector3(lookTowardsPlayer.x, lookTowardsPlayer.y, lookTowardsPlayer.z);
            
            if (Physics.Raycast(lookHeight, playerXZpos, out hit, viewDistance))
            {
                print(hit.collider.name);
                
                if (hit.collider.CompareTag("Player"))
                {
                    
                }
            }
        }
    }
    
    
    private void OnDrawGizmos()
    {
        Vector3 lookTowardsPlayer = (playerIsStandingHitBox.transform.position - eyes.position).normalized;
        Vector3 temp = lookTowardsPlayer * viewDistance;
        
        Vector3 lookHeight = new Vector3(eyes.transform.position.x, eyes.transform.position.y - 1, eyes.transform.position.z);
        Vector3 playerXZpos = new Vector3(temp.x, temp.y, temp.z);
        
        Gizmos.DrawRay(lookHeight, playerXZpos * viewDistance);
        Gizmos.DrawRay(eyes.transform.position, playerXZpos * viewDistance);
        
        
        if (!gizmos) return;
        
         Vector3 rightDir;
         Vector3 leftDir;
        
        float radiance = (viewAngle / 2) * Mathf.Deg2Rad;
        
        // sin and cos switched out, alternatively subtract 180degrees
        rightDir = new Vector3(Mathf.Sin(radiance),  0,Mathf.Cos(radiance)).normalized * viewDistance + eyes.position;
        leftDir = new Vector3(-Mathf.Sin(radiance),  0,Mathf.Cos(radiance)).normalized * viewDistance + eyes.position;
        
        //Gizmos.DrawWireSphere(camera, fovLength);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(eyes.position, rightDir);
        Gizmos.DrawLine(eyes.position, leftDir);
    }


    protected override void Walk()
    {
        if (dontPatrol) return;
        
        if (_agent.pathPending || _agent.isPathStale) return;
        
        if (distanceToPoint > _agent.remainingDistance)
        {
            waitTimer = patrolPoints[currentPointIndex].WaitTime;
            _patrolState = PatrolState.waiting;
        }
    }
    
    protected override void Waiting()
    {
        if (dontPatrol) return;
        waitTimer -= Time.deltaTime;

        if (waitTimer > 0) { return; }

        currentPointIndex++;
        if (currentPointIndex > patrolPoints.Count - 1) currentPointIndex = 0;
        
        _agent.SetDestination(patrolPoints[currentPointIndex].point.position);
        _patrolState = PatrolState.walk;
    }

    protected override void Looking() { }

    protected override void WalkingTowards() { }

    protected override void GoToCover() { }

    protected override void ShootFromCover() { }
    
    
    
    
}
