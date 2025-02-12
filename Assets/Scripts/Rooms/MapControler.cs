using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    public Vector2Int position;
    public bool roomOnTop;
    public bool roomOnBottom;
    public bool roomOnLeft;
    public bool roomOnRight;
}

public class MapControler : MonoBehaviour
{
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject floor;
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

        foreach(Vector2Int roomPosition in rooms)
        {
            GameObject room = Instantiate(roomPrefab, transform);
            room.transform.position = new Vector3(60 * roomPosition.x, 0, 60 * roomPosition.y);
            Instantiate(floor, room.transform);

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
    }

    void Start()
    {
        GenerateFloor(20);
    }

}
