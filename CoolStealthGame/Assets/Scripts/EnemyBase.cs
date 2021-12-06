using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent _agent;
    public virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    [Serializable]public class PatrolPoints
    {
        public Transform point;
        public float WaitTime;
    }
    
    [SerializeField] protected List<PatrolPoints> patrolPoints = new List<PatrolPoints>();
    [SerializeField] protected int currentPointIndex = 0;
    [SerializeField] protected float waitTimer = 3;
    [SerializeField] protected float distanceToPoint = .5f;

    protected EnemyState _enemyState = EnemyState.patrolling;
    protected PatrolState _patrolState = PatrolState.waiting;
    protected InvestigateState _investigateState = InvestigateState.looking;
    protected AttackState _attackState = AttackState.goToCover;
    
    public enum EnemyState
    {
        patrolling,
        investigating,
        attacking
    }

    public enum PatrolState
    {
        waiting,
        walk
    }
    
    public enum InvestigateState
    {
        looking,
        walkingTowards
    }
    
    public enum AttackState
    {
        goToCover,
        shootFromCover
    }

    public void CurrentEnemyState(EnemyState curState)
    {
        switch (curState)
        {
            //
            case EnemyState.patrolling:
                switch (_patrolState)
                {
                    case PatrolState.walk:
                    {
                        Walk();
                    }
                        break;
                    case PatrolState.waiting:
                    {
                        Waiting();
                    }
                        break;
                }
                break;
            //
            case EnemyState.investigating:
                switch (_investigateState)
                {
                    case InvestigateState.looking:
                    {
                        Looking();
                    }
                        break;
                    case InvestigateState.walkingTowards:
                    {
                        WalkingTowards();
                    }
                        break;
                }
                break;
            //
            case EnemyState.attacking:
                switch (_attackState)
                {
                    case AttackState.goToCover:
                    {
                        GoToCover();
                    }
                        break;
                    case AttackState.shootFromCover:
                    {
                        ShootFromCover();
                    }
                        break;
                }
                break;
        }
    }

    protected virtual void Update()
    {
        //print(_patrolState);
        CurrentEnemyState(_enemyState);
    }

    protected virtual void Walk() { }
    protected virtual void Waiting() { }
    protected virtual void Looking() { }
    protected virtual void WalkingTowards() { }
    protected virtual void GoToCover() { }
    protected virtual void ShootFromCover() { }
}
