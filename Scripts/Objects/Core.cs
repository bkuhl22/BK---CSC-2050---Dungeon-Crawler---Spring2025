using UnityEngine;

public class Core
{
    public static bool DoorNorth { get => doorNorth; set => doorNorth = value; }
    public static bool DoorEast { get => doorEast; set => doorEast = value; }
    public static bool DoorWest { get => doorWest; set => doorWest = value; }
    public static bool DoorSouth { get => doorSouth; set => doorSouth = value; }

    private static bool doorNorth = false;
    private static bool doorEast = true;
    private static bool doorWest = false;
    private static bool doorSouth = false;

}