using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [Header("Movement")]
    private Tweener tweener;
    private Animator animator;
    public float speed = 1f;
    private Vector2 lastInput, currentInput, endPos;
    public Vector2 CurrentInput
    {
        get { return currentInput; }
        set { currentInput = value; }
    }
    public Vector2 LastInput
    {
        get { return lastInput; }
        set { lastInput = value; }
    }
    public Vector2 EndPos
    {
        get { return endPos; }
        set { endPos = value; }
    }
    [Header("Teleport")]
    private bool teleporting = false;
    public bool Teleporting
    {
        get { return teleporting; }
        set { teleporting = value; }
    }

    // ANCHOR Game Loop
    private void Start()
    {
        EndPos = (Vector2)transform.position;
        tweener = GetComponent<Tweener>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // NOTE Once game is over, all inputs are disabled
        if (GameManager.IsInputEnabled)
        {
            // NOTE Get the aiming direction from key input
            if (Input.GetKeyDown(KeyCode.D))
            {
                LastInput = Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                LastInput = -Vector2.up;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                LastInput = -Vector2.right;
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                LastInput = Vector2.up;
            }
        }
    }
    private void FixedUpdate()
    {
        MovementController();
    }
    private void LateUpdate()
    {
        AnimationController();
    }

    // ANCHOR Controllers and functions
    private void MovementController()
    {
        Transform target = transform;
        Vector3 startPos = target.position;

        // NOTE When not moving
        if (Vector2.Distance(EndPos, transform.position) < 0.00001f)
        {
            // Check next of lastInput is walable or not
            if (NextIsWalkable(LastInput))
            {
                EndPos = (Vector2)transform.position + LastInput;
                CurrentInput = LastInput;
            }
            else
            {
                if (NextIsWalkable(CurrentInput))
                    EndPos = (Vector2)transform.position + CurrentInput;
            }
        }

        // NOTE Move by tweening, except teleport routine starts
        if (!teleporting)
            tweener.AddTween(target, startPos, EndPos, 1 / speed);
    }

    /*
        NOTE Because pacman is toucing wall collider most (if not all) the time
             It needs to use Raycast to detect a hit/collision
    */
    public RaycastHit2D HitDetect(Vector2 dir)
    {
        // NOTE Cast Line from 'adjacent grid position'(pos + dir) to 'Pac-Man' himself (pos)
        Vector2 pos = transform.position;
        dir += new Vector2(dir.x * 0.25f, dir.y * 0.25f);
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);

        return hit;
    }
    private bool NextIsWalkable(Vector2 dir)
    {
        if (HitDetect(dir))
        {
            RaycastHit2D hit = HitDetect(dir);
            return hit.collider.tag == "Pellet" || (hit.collider == GetComponent<Collider2D>());
        }
        else
        {
            return false;
        }
    }
    private void AnimationController()
    {
        Vector2 dir = EndPos - (Vector2)transform.position;
        animator.SetFloat("DirX", dir.x);
        animator.SetFloat("DirY", dir.y);
    }
}
