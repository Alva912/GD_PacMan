using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PacmanMove : MonoBehaviour
{
    Text scoreText;
    public int scoreCounter = 0;
    public int hp = 3;
    public float speed = 0.4f;
    Vector2 dest = Vector2.zero;

    bool valid(Vector2 dir)
    {
        // Cast Line from 'next to Pac-Man'(pos + dir) to 'Pac-Man' himself (pos)
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    void Start()
    {
        dest = transform.position;
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        // Check for Input if not moving
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
            {
                dest = (Vector2)transform.position - Vector2.up;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
            {
                dest = (Vector2)transform.position - Vector2.right;
            }
        }

        // Move closer to Destination
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        // Animation Parameters
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);

        scoreText.text = "HP:" + hp.ToString() + "     Scores: " + scoreCounter.ToString();
    }
}
