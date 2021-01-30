using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    // ANCHOR Player Stats
    private static int healthPoint = 3;
    public static int HealthPoint
    {
        get { return healthPoint; }
        set { healthPoint = value; }
    }
    private static int score = 0;
    public static int Score
    {
        get { return score; }
        set { score = value; }
    }
    private static bool powerUp = false;
    public static bool PowerUp
    {
        get { return powerUp; }
        set { powerUp = value; }
    }
    private static float powerTimer;
    public static float PowerTimer
    {
        get { return powerTimer; }
        set { powerTimer = value; }
    }
    private IEnumerator statsRoutine;
    public CharacterMove characterMove;

    private void Awake()
    {
        healthPoint = 3;
        score = 0;
    }

    // ANCHOR Stats Control
    void OnTriggerEnter2D(Collider2D other)
    {
        // NOTE Count Score
        if (other.tag == "Pellet")
        {
            Destroy(other.gameObject);
            Score += (other.name == "BonusCherry(Clone)") ? 100 : 10;

            // NOTE Power UP
            if (other.name == "PowerPellet(Clone)" && statsRoutine == null)
            {
                statsRoutine = PacmanPowerUp();
                StartCoroutine(statsRoutine);
            }
        }
        // NOTE Teleport
        if (!characterMove.Teleporting && other.name == "LeftPortal")
        {
            Vector3 right = new Vector3(27f, 16.5f, 0);
            StartCoroutine(PacmanTeleport(right));
        }
        if (!characterMove.Teleporting && other.name == "RightPortal")
        {
            Vector3 left = new Vector3(2f, 16.5f, 0);
            StartCoroutine(PacmanTeleport(left));
        }
        // NOTE Die and spawn
        if (!characterMove.Teleporting && !PowerUp && HealthPoint > 1 && other.tag == "Ghost")
        {
            Vector3 spawn = new Vector3(2f, 30f, 0);
            StartCoroutine(PacmanTeleport(spawn));
        }
    }
    IEnumerator PacmanPowerUp()
    {
        PowerUp = true;
        PowerTimer = 10.0f;
        while (PowerTimer > 1)
        {
            yield return new WaitForSeconds(1.0f);
            PowerTimer -= 1.0f;
            // Debug.Log(PowerTimer);
        }
        PowerTimer = 0;
        PowerUp = false;
        statsRoutine = null;
    }
    IEnumerator PacmanTeleport(Vector3 pos)
    {
        Debug.Log("Teleporting...");
        characterMove.Teleporting = true;
        yield return new WaitForSeconds(.5f);
        transform.position = pos;
        characterMove.EndPos = pos;
        yield return new WaitForSeconds(1.0f);
        characterMove.Teleporting = false;
    }
}
