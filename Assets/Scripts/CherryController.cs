using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    public GameObject BonusCherry;
    public float speed = 1.0f;
    private Vector3 rightPos;
    private Vector3 leftPos;

    private Coroutine cherryRoutine;
    private void Awake()
    {
        // NOTE Main camera position z is -10， camera view x y between 0 and 1.
        rightPos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10.0f));
        leftPos = Camera.main.ViewportToWorldPoint(new Vector3(-0.1f, 0.5f, 10.0f));
    }

    private void Update()
    {
        if (cherryRoutine == null)
        {
            cherryRoutine = StartCoroutine(SpawnCherry());
        }
        if (GameObject.Find("BonusCherry(Clone)"))
        {
            Transform target = GameObject.Find("BonusCherry(Clone)").transform;
            target.position = Vector3.Lerp(target.position, leftPos, 1 / (speed * 60));

            // NOTE Destroy cherry when it (almost) reach the other side
            if (target.position.x - leftPos.x < 0.1f)
                Destroy(target.gameObject);
        }
    }

    IEnumerator SpawnCherry()
    {
        yield return new WaitForSeconds(30.0f);
        Instantiate(BonusCherry, rightPos, Quaternion.identity, transform);
        cherryRoutine = null;
    }
}
