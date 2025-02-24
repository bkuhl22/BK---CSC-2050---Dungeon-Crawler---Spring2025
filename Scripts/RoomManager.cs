using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] theDoors;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theDoors[0].SetActive(Core.DoorNorth);
        //MeshRenderer mr = this.theDoors[0].GetComponent<MeshRenderer>();
        //mr.enabled = false;

        theDoors[1].SetActive(Core.DoorEast);
        theDoors[2].SetActive(Core.DoorWest);
        theDoors[3].SetActive(Core.DoorSouth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}