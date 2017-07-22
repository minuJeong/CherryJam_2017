using System.Collections;
using System.Collections.Generic;
using Underwater;
using UnityEngine;

// Non-playable pawn
public sealed class NPPawn : Pawn
{
    public Needs m_Needs = new Needs();

    public enum State
    {
        IDLE,

        SATISFY_NEED,
    }

    private State _state;
    public State m_State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
            OnStateChanged(value);
        }
    }

    private void Start()
    {
        m_State = State.IDLE;
    }

    private void PickNextTodo()
    {
        // select fuzzy need action
        NeedType nextNeed = m_Needs.GetNextNeedFuzzy();
    }

    private void OnStateChanged(State state)
    {
        switch (state)
        {
            case State.IDLE:
                PickNextTodo();
                break;
        }
    }
}
