using UnityEngine;
using System.Collections;

public abstract class Behaviour : MonoBehaviour
{
    protected Actor _entity;
    protected ActorStateController _stateController;

    protected virtual void Awake()
    {
        _entity = gameObject.GetComponent<Actor>();
        _stateController = gameObject.GetComponent<ActorStateController>();
    }
}
