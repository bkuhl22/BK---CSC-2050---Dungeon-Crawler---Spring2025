using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] theDoors;
    public GameObject mmRoomPrefab;
    private Dungeon theDungeon;
    private Vector3 miniMapPosition = Vector3.zero;

    void Start()
    {
        Core.thePlayer = new Player("Mike");
        this.theDungeon = new Dungeon();
        this.setupRoom();

        // Initialize minimap position inside Start()
        miniMapPosition = new Vector3(30, 0, 5);

        // Create the initial minimap room at the starting position
        Instantiate(mmRoomPrefab, miniMapPosition, Quaternion.identity);
    }

    private void resetRoom()
    {
        this.theDoors[0].SetActive(false);
        this.theDoors[1].SetActive(false);
        this.theDoors[2].SetActive(false);
        this.theDoors[3].SetActive(false);
    }

    private void setupRoom()
    {
        Room currentRoom = Core.thePlayer.getCurrentRoom();
        this.theDoors[0].SetActive(currentRoom.hasExit("north"));
        this.theDoors[1].SetActive(currentRoom.hasExit("south"));
        this.theDoors[2].SetActive(currentRoom.hasExit("east"));
        this.theDoors[3].SetActive(currentRoom.hasExit("west"));
    }

    void Update()
    {
        bool didChangeRoom = false;
        Vector3 movementOffset = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            didChangeRoom = Core.thePlayer.getCurrentRoom().tryToTakeExit("north");
            movementOffset = new Vector3(0, 0, 3.5f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            didChangeRoom = Core.thePlayer.getCurrentRoom().tryToTakeExit("west");
            movementOffset = new Vector3(-3.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            didChangeRoom = Core.thePlayer.getCurrentRoom().tryToTakeExit("east");
            movementOffset = new Vector3(3.5f, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            didChangeRoom = Core.thePlayer.getCurrentRoom().tryToTakeExit("south");
            movementOffset = new Vector3(0, 0, -3.5f);
        }

        if (didChangeRoom)
        {
            miniMapPosition += movementOffset; 
            GameObject newMMRoom = Instantiate(mmRoomPrefab);
            newMMRoom.transform.position = miniMapPosition;
            this.setupRoom();
        }
    }
}
