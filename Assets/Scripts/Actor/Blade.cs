using UnityEngine;
using System.Collections;

public class Blade : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().Hurt(1);
    }
}
