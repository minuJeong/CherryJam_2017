using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionHandler : MonoBehaviour
{
    Animator _anim;
    Vector3 _prevPos;

    // Use this for initialization
    void Start()
    {
        _anim = GetComponent<Animator>();
        _prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = transform.position - _prevPos;
        _anim.SetFloat("MoveSpeed", delta.magnitude / Time.deltaTime);
        _prevPos = transform.position;
    }
}
