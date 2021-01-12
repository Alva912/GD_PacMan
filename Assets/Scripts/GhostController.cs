using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator animator;
    private CharacterMove characterMove;
    private Transform target;
    private Coroutine scareRoutine = null;
    public enum GhostStatus
    {
        Pause,
        Normal,
        Scared,
        Dead,
    }
    private static GhostStatus status;
    public static GhostStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            switch (status)
            {
                case GhostStatus.Pause:
                    BGMManager.CurrentBGM = BGMManager.GameBGM.Intro;
                    break;
                case GhostStatus.Normal:
                    BGMManager.CurrentBGM = BGMManager.GameBGM.Normal;
                    break;
                case GhostStatus.Scared:
                    BGMManager.CurrentBGM = BGMManager.GameBGM.Scared;
                    break;
                case GhostStatus.Dead:
                    BGMManager.CurrentBGM = BGMManager.GameBGM.Dead;
                    break;
            }
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterMove = GetComponent<CharacterMove>();
    }
    private void Update()
    {
        // NOTE Simple AI chasing pacman
        target = GameObject.Find("Pacman").transform;
        if (GameManager.IsInputEnabled)
        {
            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
            characterMove.LastInput = Vector2.ClampMagnitude(dir, 1.0f);
            characterMove.CurrentInput = Random.insideUnitCircle;
        }

        // NOTE Don't switch BGM until pause panel is destroyed and ghosts start moving
        if (GameObject.FindWithTag("Panel") != null)
        {
            Status = GhostStatus.Pause;
        }

        // NOTE Once pacman eat a power pellet
        if (scareRoutine == null && PacStudentController.PowerUp)
        {
            scareRoutine = StartCoroutine(GhostScared());
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Pacman")
        {
            if (Status == GhostStatus.Scared)
            {
                // NOTE Collide with pacman when scared -> this ghost dies
                StopAllCoroutines();
                PacStudentController.Score += 300;
                Status = GhostStatus.Dead;
                animator.SetBool("Scared", false);
                animator.SetBool("Recover", false);
                StartCoroutine(GhostDead());
            }
            if (Status == GhostStatus.Normal)
            {
                // NOTE Collide with pacman when normal(not sared or dead)
                if (PacStudentController.HealthPoint > 0)
                {
                    PacStudentController.HealthPoint -= 1;
                }
            }
        }
    }

    IEnumerator GhostScared()
    {
        Status = GhostStatus.Scared;
        animator.SetBool("Scared", true);
        yield return new WaitForSeconds(7.0f);
        animator.SetBool("Recover", true);
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("Recover", false);
        animator.SetBool("Scared", false);
        Status = GhostStatus.Normal;
        scareRoutine = null;
    }

    IEnumerator GhostDead()
    {
        // NOTE Reset all the params and status
        animator.SetBool("Scared", false);
        animator.SetBool("Recover", false);
        Status = GhostStatus.Dead;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(5.0f);
        animator.SetBool("Dead", false);
        Status = GhostStatus.Normal;
        scareRoutine = null;
    }
}
