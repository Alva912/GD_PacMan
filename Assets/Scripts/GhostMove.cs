using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    public float speed = 0.3f;
    public Transform[] waypoints;
    int current = 0;

    void FixedUpdate()
    {
        // Waypoint not reached yet? then move closer
        if (transform.position != waypoints[current].position)
        {
            Vector2 p = Vector2.MoveTowards(transform.position, waypoints[current].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
        // Waypoint reached, select next one
        else
        {
            current = (current + 1) % waypoints.Length;
        }
        // Animation
        Vector2 dir = waypoints[current].position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "pacman")
        {

            if (other.gameObject.GetComponent<PacmanMove>().hp == 0)
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<PacmanMove>().hp -= 1;
            }
        }
    }
}
