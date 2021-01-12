using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanSFX : MonoBehaviour
{
    // NOTE Use pacman's characterMove component instance
    public CharacterMove characterMove;

    // NOTE Use Raycast2D to detect hit/collision
    private RaycastHit2D hit;
    private Vector2 endPos, currentInput;

    [Header("Audio")]
    public List<AudioClip> soundEffects;
    private AudioSource audioSource;

    [Header("Particle")]
    public ParticleSystem walkingDustPS;
    public GameObject hitWallPS;
    public GameObject diePS;
    private Coroutine particleRoutine;

    // ANCHOR Game Loop
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        // NOTE Get real-time movement info
        endPos = characterMove.EndPos;
        currentInput = characterMove.CurrentInput;

        hit = characterMove.HitDetect(currentInput);
    }
    private void FixedUpdate()
    {
        // NOTE Physics
        if (hit)
        {
            AudioController(hit);
            ParticleController(hit);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // NOTE Play particle effects once when collide with a nomal state ghost
        if (particleRoutine == null && other.tag == "Ghost" && GhostController.Status == GhostController.GhostStatus.Normal)
        {
            StartCoroutine(SpawnDiePS());
        }
    }

    // ANCHOR Controllers
    private void AudioController(RaycastHit2D hit)
    {
        // NOTE When pacman is moving
        if (Vector2.Distance(endPos, gameObject.transform.position) > 0.000001f)
        {
            switch (hit.collider.tag)
            {
                case "Ghost":
                    if (!characterMove.Teleporting&&GhostController.Status==GhostController.GhostStatus.Normal)
                        audioSource.PlayOneShot(soundEffects[4], 1f);
                    break;
                case "Portal":
                    if (!characterMove.Teleporting)
                        audioSource.PlayOneShot(soundEffects[3], .8f);
                    break;
                case "Wall":
                    audioSource.PlayOneShot(soundEffects[2], 1f);
                    break;
                case "Pellet":
                    audioSource.PlayOneShot(soundEffects[0], .8f);
                    break;
                default:
                    audioSource.PlayOneShot(soundEffects[1], .8f);
                    break;
            }
        }
    }
    private void ParticleController(RaycastHit2D hit)
    {
        // NOTE Play particle effects once when hiting wall in the aiming direction
        if (particleRoutine == null && hit.collider.tag == "Wall")
        {
            particleRoutine = StartCoroutine(SpawnHitWallPS(hit.point));
        }
        // NOTE Play particle effects when walking
        if (hit.collider.tag != "Wall")
        {
            walkingDustPS.transform.LookAt(endPos);
            walkingDustPS.Play();
        }
    }
    IEnumerator SpawnHitWallPS(Vector3 hitPoint)
    {
        hitWallPS.transform.LookAt(currentInput);
        Instantiate(hitWallPS, hitPoint, hitWallPS.transform.rotation, transform);
        yield return new WaitUntil(() => { return Vector2.Distance(endPos, transform.position) > 0.001f; });
        particleRoutine = null;
    }
    IEnumerator SpawnDiePS()
    {
        Instantiate(diePS, transform.position, diePS.transform.rotation, transform);
        yield return new WaitForSeconds(1.0f);
        particleRoutine = null;
    }
}
