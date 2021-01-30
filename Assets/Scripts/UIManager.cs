using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private Scene scene;
    [SerializeField] private GameSave gameSave;
    [SerializeField] private GameObject startPanel, pausePanel, headUpDisplay;
    [SerializeField] private Image[] lifeIndicators;
    [SerializeField] private Text[] texts;
    private float inGameTimer, countdown;
    public float InGameTime { get { return inGameTimer; } }
    private bool isPaused = false;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();

        // NOTE In the Start scene, load priveous game save and display text
        if (scene.buildIndex == 0)
        {
            SaveData loadData = gameSave.LoadFromJson();
            float igt = loadData.pacmanData[0].inGameTime;
            var ts = TimeSpan.FromSeconds(igt);
            texts[0].text = "Previous score: " + loadData.pacmanData[0].score.ToString() + "\r\nPrevious time: " + string.Format("{0:00}:{1:00}:{2:00}", (int)ts.TotalHours, (int)ts.TotalMinutes, (int)ts.Seconds);
        }
    }
    private void Update()
    {
        // NOTE In the Level 1 scene
        if (scene.buildIndex == 1)
        {
            // NOTE Lives
            switch (PacStudentController.HealthPoint)
            {
                case 2:
                    Destroy(lifeIndicators[2]);
                    break;
                case 1:
                    Destroy(lifeIndicators[1]);
                    break;
                case 0:
                    Destroy(lifeIndicators[0]);
                    if (!isPaused) { OnPaused(); }
                    break;
            };

            // NOTE Texts
            texts[0].text = "Scores: " + PacStudentController.Score;

            inGameTimer += Time.deltaTime;
            texts[1].text = "Timer: " + inGameTimer.ToString("F");

            if (PacStudentController.PowerUp & GhostController.Status == GhostController.GhostStatus.Scared)
            {   // NOTE When Pacman is powered up, Ghosts are scared(not dead)
                countdown = PacStudentController.PowerTimer;
                texts[2].text = "Ghost Timer: " + countdown.ToString("F");
            }
            else
            {   // NOTE When Pacman is normal and Ghosts are not scared(normal/dead)
                texts[2].text = "";
            }

            if (GameObject.FindWithTag("Pellet") == null && !isPaused) { OnPaused(); }
        }
    }
    void OnPaused()
    {
        GameManager.IsGameOver = true;
        Instantiate(pausePanel, headUpDisplay.transform);

        // NOTE Invoke only once, not every frame update
        isPaused = true;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BGMManager.CurrentBGM = BGMManager.GameBGM.Intro;
        if (scene.buildIndex == 0)
        {
            // TODO
        }
        if (scene.buildIndex == 1)
        {
            headUpDisplay = GameObject.Find("HUD");
            inGameTimer = 0;
            Instantiate(startPanel, headUpDisplay.transform);
            texts = headUpDisplay.GetComponentsInChildren<Text>();
            lifeIndicators = headUpDisplay.GetComponentsInChildren<Image>();
        }
    }
}
