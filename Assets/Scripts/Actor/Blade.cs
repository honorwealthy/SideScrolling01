﻿using UnityEngine;
using System.Collections;

namespace SeafoodStudio
{
    public class Blade : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
                other.GetComponent<Actor>().Hurt(1);
        }
    }
}