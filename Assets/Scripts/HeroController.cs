using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb2d;
    private bool facingRight = true;

    public float Speed = 5.0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    //private void Update()
    //{

    //}

    private void FixedUpdate()
    {
        var direction = Input.GetAxisRaw("Horizontal");
        var x = Mathf.Abs(Input.GetAxis("Horizontal"));
        anim.SetFloat("GroundSpeed", x);
        rb2d.velocity = new Vector2(direction * x * Speed, rb2d.velocity.y);

        if (direction != 0)
            SetDirection(direction > 0);
    }

    private void SetDirection(bool isRight)
    {
        if (facingRight != isRight)
        {
            facingRight = isRight;

            var theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
