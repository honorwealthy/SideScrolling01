using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rd2d;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
    }
}
