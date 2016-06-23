using UnityEngine;
using System.Collections;

public class MettoolBehaviour : Behaviour
{
    public float Direction { get; set; }
    private bool rethink = true;

    protected override void Awake()
    {
        base.Awake();
        Direction = -1;
    }

    private void FixedUpdate()
    {
        if (rethink)
        {
            _entity.StartCoroutine(Think());
        }
    }

    private IEnumerator Think()
    {
        yield return new WaitForSeconds(1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            Direction *= -1;
        }
    }
}
