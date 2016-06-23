using UnityEngine;
using System.Collections;

public class MettoolBuster : MonoBehaviour
{
    public float Speed = 10f;

    private void Start()
    {
        Destroy(gameObject, 2);
    }

    public void BeginFly(float direction)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(direction * Speed, 0);
    }

    //private void OnTriggerEnter2D(Collider2D col)
    //{
    //    Destroy(gameObject);
    //}
}
