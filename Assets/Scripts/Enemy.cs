using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public IAvatar Avatar { get; private set; }
    private string state = "up";
    private bool flag = false;
    private bool isRight = true;

    private void Awake()
    {
        this.Avatar = GetComponent<Avatar>();
    }

    private void Start()
    {
        StartCoroutine(Dice());
    }

    private void FixedUpdate()
    {
        if (flag)
        {
            StartCoroutine(Dice());
            flag = false;
            if (Random.Range(0, 5) < 1)
                UpDown();
        }

        var speed = 5;
        var direction = isRight ? 1 : -1;
        Avatar.rb2d.velocity = new Vector2(direction * speed, Avatar.rb2d.velocity.y);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("edge"))
        {
            isRight = !isRight;
            Avatar.SetDirection(isRight);
        }
    }

    private IEnumerator Dice()
    {
        yield return new WaitForSeconds(1);
        flag = true;
    }

    private void UpDown()
    {
        if (state == "up")
        {
            Avatar.anim.SetTrigger("Laydown");
            state = "down";
        }
        else if (state == "down")
        {
            Avatar.anim.SetTrigger("Riseup");
            state = "up";
        }
    }

    public void Hurt(int damage)
    {
        Destroy(gameObject, 0.1f);
    }
}
