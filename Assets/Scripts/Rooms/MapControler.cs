using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapControler : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject staringRoom;
    [SerializeField] private GameObject endingRoom;
    [SerializeField] private List<GameObject> hospitalRooms;
    [SerializeField] private GameObject normalWall;
    [SerializeField] private GameObject wallWithDoor;

    private List<Vector2Int> GetPosibleRooms(HashSet<Vector2Int> rooms)
    {
        HashSet<Vector2Int> posibleRooms = new();

        foreach(Vector2Int room in rooms)
        {
            if(!rooms.Contains(new Vector2Int(room.x, room.y + 1))) posibleRooms.Add(new Vector2Int(room.x, room.y + 1));
            if(!rooms.Contains(new Vector2Int(room.x, room.y - 1))) posibleRooms.Add(new Vector2Int(room.x, room.y - 1));
            if(!rooms.Contains(new Vector2Int(room.x + 1, room.y))) posibleRooms.Add(new Vector2Int(room.x + 1, room.y));
            if(!rooms.Contains(new Vector2Int(room.x - 1, room.y))) posibleRooms.Add(new Vector2Int(room.x - 1, room.y));
        }

        return posibleRooms.ToList();
    }

    private void GenerateWalls(GameObject room, Vector2Int roomPosition, HashSet<Vector2Int> rooms)
    {
        GameObject wall;
        if(rooms.Contains(new Vector2Int(roomPosition.x + 1, roomPosition.y)))
        {
            wall = Instantiate(wallWithDoor, room.transform);
            room.GetComponent<RoomControler>().doors.Add(wall.GetComponent<DoorControler>());
        } 
        else wall = Instantiate(normalWall, room.transform);
        wall.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        
        if(rooms.Contains(new Vector2Int(roomPosition.x - 1, roomPosition.y)))
        {
            wall = Instantiate(wallWithDoor, room.transform);
            room.GetComponent<RoomControler>().doors.Add(wall.GetComponent<DoorControler>());
        } 
        else wall = Instantiate(normalWall, room.transform);
        wall.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        if(rooms.Contains(new Vector2Int(roomPosition.x, roomPosition.y + 1)))
        {
            wall = Instantiate(wallWithDoor, room.transform);
            room.GetComponent<RoomControler>().doors.Add(wall.GetComponent<DoorControler>());
        } 
        else wall = Instantiate(normalWall, room.transform);
        wall.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));

        if(rooms.Contains(new Vector2Int(roomPosition.x, roomPosition.y - 1)))
        {
            wall = Instantiate(wallWithDoor, room.transform);
            room.GetComponent<RoomControler>().doors.Add(wall.GetComponent<DoorControler>());
        } 
        else wall = Instantiate(normalWall, room.transform);
        wall.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 0));
    }

    private void GenerateFloor(int numberOfRooms)
    {
        HashSet<Vector2Int> rooms = new()
        {
            new Vector2Int(0, 0)
        };

        for(int i = 0; i < numberOfRooms - 1; i++)
        {
            List<Vector2Int> posibleRooms = GetPosibleRooms(rooms);
            Vector2Int room = posibleRooms[Random.Range(0, posibleRooms.Count)];
            rooms.Add(room);
        }

        List<Vector2Int> posibleEndingRooms = GetPosibleRooms(rooms);
        Vector2Int endingRoomPosition = posibleEndingRooms[Random.Range(0, posibleEndingRooms.Count)];
        GameObject endingRoomObject = Instantiate(roomPrefab, transform);
        endingRoomObject.transform.position = new Vector3(60 * endingRoomPosition.x, 0, 60 * endingRoomPosition.y);
        Instantiate(endingRoom, endingRoomObject.transform);
        GenerateWalls(endingRoomObject, endingRoomPosition, rooms);

        foreach(Vector2Int roomPosition in rooms)
        {
            GameObject room = Instantiate(roomPrefab, transform);
            room.transform.position = new Vector3(60 * roomPosition.x, 0, 60 * roomPosition.y);
            if(roomPosition == new Vector2Int(0, 0))
            {
                Instantiate(staringRoom, room.transform);
            }
            else
            {
                Instantiate(hospitalRooms[Random.Range(0, hospitalRooms.Count)], room.transform);
            }

            GenerateWalls(room, roomPosition, new HashSet<Vector2Int>(rooms) { endingRoomPosition });
        }
    }

    void Start()
    {
        GenerateFloor(5);
    }

}
