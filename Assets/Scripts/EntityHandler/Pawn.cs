﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Extension of NavMeshAgent
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public abstract class Pawn : Interactable
{
    public override float GetRadius()
    {
        return m_Agent.radius;
    }

    private NavMeshAgent _agent;
    public NavMeshAgent m_Agent
    {
        get
        {
            if (_agent == null)
            {
                _agent = GetComponent<NavMeshAgent>();
            }
            return _agent;
        }
    }

    private bool IsAgentReachedDestination()
    {
        return m_Agent.remainingDistance != Mathf.Infinity &&
               m_Agent.remainingDistance == 0 &&
               m_Agent.pathStatus == NavMeshPathStatus.PathComplete;
    }

    private Queue<Waypoint> _waypoint = new Queue<Waypoint>();
    private Coroutine _waypointReader = null;
    private bool _waypointReaderRunning = false;
    private IEnumerator ReadWaypoint()
    {
        Func<Interactable, Vector3> GetDestinationToTarget = (target) =>
        {
            float d = target.GetRadius() + GetRadius();
            var delta = target.transform.position - transform.position;
            var resultPos = target.transform.position - delta.normalized * d;

            Debug.DrawLine(transform.position, resultPos);
            return resultPos;
        };

        _waypointReaderRunning = true;
        while (_waypoint.Count > 0)
        {
            var nextWaypoint = _waypoint.Dequeue();
            switch (nextWaypoint.m_WaypointType)
            {
                case Waypoint.WaypointType.Position:
                    m_Agent.SetDestination(nextWaypoint.m_TargetPos);
                    break;

                case Waypoint.WaypointType.Location:
                    m_Agent.SetDestination(
                        nextWaypoint.m_TargetLocation.m_AreaDefinition.transform.position
                    );
                    break;

                case Waypoint.WaypointType.Target:
                    m_Agent.SetDestination(
                        GetDestinationToTarget(nextWaypoint.m_TargetObject)
                    );
                    break;
            }

            // wait for reach destination
            while (true)
            {
                yield return null;

                if (nextWaypoint.m_WaypointType == Waypoint.WaypointType.Target)
                {
                    m_Agent.SetDestination(
                        GetDestinationToTarget(nextWaypoint.m_TargetObject)
                    );
                }
                else
                {
                    if (IsAgentReachedDestination())
                    {
                        break;
                    }
                }
            }
        }

        _waypointReaderRunning = false;
    }

    public void MoveTo(Vector3 pos, bool cancelPreviousMove = true)
    {
        if (cancelPreviousMove)
        {
            _waypoint.Clear();
        }

        _waypoint.Enqueue(new Waypoint()
        {
            m_WaypointType = Waypoint.WaypointType.Position,
            m_TargetPos = pos
        });

        if (_waypointReaderRunning && _waypointReader != null)
        {
            StopCoroutine(_waypointReader);
        }
        _waypointReader = StartCoroutine(ReadWaypoint());
    }

    public void MoveTo(Interactable target, bool cancelPreviousMove = true)
    {
        if (cancelPreviousMove)
        {
            _waypoint.Clear();
        }

        _waypoint.Enqueue(new Waypoint()
        {
            m_WaypointType = Waypoint.WaypointType.Target,
            m_TargetObject = target
        });

        if (_waypointReaderRunning && _waypointReader != null)
        {
            StopCoroutine(_waypointReader);
        }
        _waypointReader = StartCoroutine(ReadWaypoint());
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        throw new NotImplementedException();
    }
}
