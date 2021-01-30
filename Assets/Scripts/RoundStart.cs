using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundStart : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private Text countdown;

    private void Awake()
    {
        Time.timeScale = 0; // NOTE Time.time and Time.deltaTime are not working now
        countdown = GetComponentInChildren<Text>();
    }
    private void Update()
    {
        timer += Time.unscaledDeltaTime;

        if (timer > 1.0f)
        {
            countdown.fontSize = 64;
            countdown.text = "3";
        }
        if (timer > 2.0f)
        {
            countdown.text = "2";
        }
        if (timer > 3.0f)
        {
            countdown.text = "1";
        }
        if (timer > 4.0f)
        {
            countdown.text = "Go!";
        }
        if (timer > 5.0f)
        {
            Time.timeScale = 1f;
            GhostController.Status = GhostController.GhostStatus.Normal;
            Destroy(this.gameObject);
        }
        else { Time.timeScale = 0; }
        // Debug.Log(Time.timeScale);
    }
}
