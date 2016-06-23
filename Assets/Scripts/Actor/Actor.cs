using UnityEngine;
using System.Collections;

public abstract class Actor : MonoBehaviour
{
    public IAvatar Avatar { get; protected set; }
    public float Speed = 9f;
    public float JumpVelocity = 28f;

    protected ActorStateController _stateController;

    protected virtual void Awake()
    {
        Avatar = GetComponent<Avatar>();
        InitStateController();
    }

    protected virtual void InitStateController()
    {
        _stateController = new ActorStateController(this);
    }

    public virtual void Hurt(int damage) { }

    public virtual void Die()
    {
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }
        Destroy(gameObject, 0.1f);
    }

    public virtual void FixedUpdate()
    {
        _stateController.FixedUpdate();
    }

    public virtual void Update()
    {
        _stateController.Update();
    }

    public virtual void LateUpdate()
    {
        _stateController.LateUpdate();
    }
}