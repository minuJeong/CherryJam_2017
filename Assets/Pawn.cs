using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public abstract class Pawn : MonoBehaviour
{
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

    public void MoveTo(Vector3 pos)
    {
        m_Agent.SetDestination(pos);
    }
}
