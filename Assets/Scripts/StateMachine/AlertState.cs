using UnityEngine;
using System.Collections;
using System;

public class AlertState : IEnemyState {
    private StatePatternEnemy enemy;
    private float searchTime;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    public void ToAlertState()
    {
        Debug.Log("cant transition to same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
        searchTime = 0;
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
        searchTime = 0;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(enemy.eyes.transform.position, enemy.eyes.transform.forward, out hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    private void Search()
    {
        enemy.meshRendererFlag.material.color = Color.yellow;
        enemy.navMeshAgent.Stop();
        enemy.transform.Rotate(0, enemy.searchingTurnSpeed * Time.deltaTime, 0);
        searchTime += Time.deltaTime;

        if (searchTime >= enemy.searchingDuration)
        {
            ToPatrolState();
        }
    }
}
