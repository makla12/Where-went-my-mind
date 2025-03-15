using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomControler : MonoBehaviour
{
    public List<GameObject> enemys = new();
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

    private void Update() {
        foreach(GameObject enemy in enemys)
        {
            if(enemy == null) enemys.Remove(enemy);
        }
        if(enemys.Count == 0) OpenDoors();
        else CloseDoors();
    }
}
