using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomControler : MonoBehaviour
{
    private bool roomCompleted = false;
    public List<EnemyControler> enemys = new();
    public List<DoorControler> doors = new();

    private void CloseDoors()
    {
        foreach(DoorControler door in doors)
        {
            door.CloseDoor();
        }
    }

    private void OpenDoors()
    {
        foreach(DoorControler door in doors)
        {
            door.OpenDoor();
        }
    }

    public void OnTriggerEnter(Collider other) {
        // Debug.Log(other.tag);
        if(other.CompareTag("Player") && !roomCompleted)
        {
            CloseDoors();
        }
    }

    private void Start() {
        OpenDoors();
        enemys.AddRange(gameObject.GetComponentsInChildren<EnemyControler>());
    }

    private void Update()
    {
        List<EnemyControler> enemiesToRemove = new List<EnemyControler>();

        foreach (EnemyControler enemy in enemys)
        {
            if (enemy.health <= 0)
            {
                enemiesToRemove.Add(enemy);
            }
        }

        foreach (EnemyControler enemy in enemiesToRemove)
        {
            enemys.Remove(enemy);
        }

        if (enemys.Count == 0 && !roomCompleted)
        {
            roomCompleted = true;
            OpenDoors();
        }
    }
}