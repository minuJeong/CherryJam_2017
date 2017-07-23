using Underwater.Think;
using UnityEngine;

public class Waypoint
{
    public enum WaypointType
    {
        Position,
        Location,
        Target,
        Interaction
    }

    public WaypointType m_WaypointType;
    public Vector3 m_TargetPos;
    public Location m_TargetLocation;
    public Interactable m_TargetObject;
}
