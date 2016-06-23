﻿using UnityEngine;
using System.Collections;

public class Mettool : Actor
{
    public float Direction { get; set; }
    public GameObject Buster;

    protected override void InitStateController()
    {
        base.InitStateController();
        _stateController.AddState(new MettoolMoveState(_stateController));
        _stateController.AddState(new MettoolHideState(_stateController));
        _stateController.AddState(new MettoolShootState(_stateController));
    }

    private void Start()
    {
        _stateController.InitState("MettoolMoveState");
        Direction = -1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            Direction *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool fromRight = (transform.position.x > other.transform.position.x);
            other.gameObject.GetComponent<Player>().Hurt(1, fromRight);
        }
    }

    public void Shoot()
    {
        _stateController.GotoState("MettoolShootState");
    }

    public void Hide()
    {
        _stateController.GotoState("MettoolHideState");
    }

    public void Move()
    {
        _stateController.GotoState("MettoolMoveState");
    }

    public override void Hurt(int damage)
    {
        Die();
    }
}
