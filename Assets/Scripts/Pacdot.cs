using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacdot : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman")
        {
            other.gameObject.GetComponent<PacmanMove>().scoreCounter += 1;
            Destroy(gameObject);
        }
    }
}
