
using System;
using System.Collections;
using System.Collections.Generic;
using Underwater.Think;
using UnityEngine;
using UnityEngine.AI;

// Non-playable pawn
public sealed class NPPawn : Pawn
{
    public Needs m_Needs = new Needs();
    public SkillKnowledge m_SkillKnowledge = new SkillKnowledge();
    public InfoKnowledge m_InfoKnowledge = new InfoKnowledge();
    // Personality
    // Interest

    public enum State
    {
        IDLE,

        SEARCH_FUZZY_NEED,
        UNSATISFIABLE_NEED,
        SEARCH_FUZZY_EXPLORE,
        PURSUE_NEED,
        STAND_STILL_FOREVER,
    }

    [SerializeField] private State _state;
    public State m_State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;
            OnStateChanged();
        }
    }

    private void Start()
    {
        OnStateChanged();
        StartCoroutine(DecayNeeds());
    }

    private IEnumerator DecayNeeds()
    {
        while (true)
        {
            yield return null;

            m_Needs.NeedsContainer.ForEach((need) =>
            {
                need.Consume(10);
            });
        }
    }

    private void PickNextTodo()
    {
        m_State = State.SEARCH_FUZZY_NEED;
    }

    private void SearchForFuzzySatisfaction()
    {
        // select fuzzy need action
        Need nextNeed = m_Needs.GetNextNeedFuzzy();
        List<Location> locations = m_InfoKnowledge.GetLocationToSatisfyNeed(nextNeed);
        if (locations.Count == 0)
        {
            m_State = State.UNSATISFIABLE_NEED;
        }
        else
        {
            m_State = State.PURSUE_NEED;
        }
    }

    private void SearchForFuzzyExplore()
    {
        // TODO: add more explore types
        m_State = State.SEARCH_FUZZY_EXPLORE;
    }

    private IEnumerator Explore()
    {
        Func<Vector3> SetNextExplorePoint = () =>
        {
            float angle = UnityEngine.Random.Range(0, Mathf.PI * 2.0F);
            float distance = UnityEngine.Random.Range(2.0F, 20.0F);
            return transform.position + new Vector3(
                    Mathf.Cos(angle) * distance,
                    transform.position.y,
                    Mathf.Sin(angle) * distance
                );
        };

        MoveTo(SetNextExplorePoint(), false);

        while (true)
        {
            if (m_Agent.remainingDistance != Mathf.Infinity &&
                m_Agent.remainingDistance == 0 &&
                m_Agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                MoveTo(SetNextExplorePoint(), false);
            }

            yield return null;
        }
    }

    private IEnumerator StandStill()
    {
        Vector3 targetPos = transform.position;
        while (true)
        {
            yield return null;

            float delta = (targetPos - transform.position).sqrMagnitude;
            if (delta > 0.1F)
            {
                MoveTo(targetPos);
            }
        }
    }

    private void OnStateChanged()
    {
        switch (m_State)
        {
            case State.IDLE:
                PickNextTodo();
                break;

            case State.SEARCH_FUZZY_NEED:
                SearchForFuzzySatisfaction();
                break;

            case State.UNSATISFIABLE_NEED:
                SearchForFuzzyExplore();
                break;

            case State.PURSUE_NEED:
                break;

            case State.SEARCH_FUZZY_EXPLORE:
                StartCoroutine(Explore());
                break;

            case State.STAND_STILL_FOREVER:
                MoveTo(transform.position);
                break;

            default:
                DebugReportUndefinedState();
                break;
        }
    }

    // Utility shortcut
    private void DebugReportUndefinedState()
    {
        Debug.Log(
            string.Format(
                "Undefined state: {0}",
                m_State
            )
        );
    }

    public override void Interact(Interactable interactee, Vector3 contact)
    {
        var player = interactee as Player;
        if (player != null)
        {
            player.StartFollowTarget(this, true);
        }
    }
}