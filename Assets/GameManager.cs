using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Scene scene;
    [Header("Managers")]
    public UIManager uI;
    [SerializeField] private BGMManager bGM;
    public GameSave gameSave;
    [Header("Data")]
    [SerializeField] private SaveData _data;
    private SaveData loadData;
    private Coroutine gameOverRoutine = null;
    private static bool isInputEnabled = true;
    public static bool IsInputEnabled
    {
        get { return isInputEnabled; }
    }
    private static bool isGameOver = false;
    public static bool IsGameOver
    {
        get { return isGameOver; }
        set
        {
            isGameOver = value;
            switch (isGameOver)
            {
                case false:
                    isInputEnabled = true;
                    break;
                case true:
                    isInputEnabled = false;
                    break;
            }
        }
    }
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (IsGameOver)
        {
            Time.timeScale = 0;
            // NOTE Start the routine once
            if (gameOverRoutine == null)
            {
                gameOverRoutine = StartCoroutine(WaitToLoadScene(0, 3));
            }
        }
        else
        {
            // NOTE Pause until panel is destroyed
            if (GameObject.FindWithTag("Panel") == null)
            {
                Time.timeScale = 1f;
            }
            else
            {
                Time.timeScale = 0;
            }
        }
        // Debug.Log(Time.timeScale);

        // NOTE Update pacman data
        if (scene.buildIndex == 1)
        {
            int current = this._data.pacmanData.Length - 1;
            this._data.pacmanData[current].score = PacStudentController.Score;
            this._data.pacmanData[current].inGameTime = uI.InGameTime;
        }
    }

    public IEnumerator WaitToLoadScene(int sceneIndex, float delay)
    {
        if (sceneIndex == 0)
        {
            loadData = gameSave.LoadFromJson();
            if (PacStudentController.Score > loadData.pacmanData[0].score || (PacStudentController.Score == loadData.pacmanData[0].score && uI.InGameTime < loadData.pacmanData[0].inGameTime))
            {
                gameSave.SaveIntoJson(this._data);
            }
        }
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single);
        IsGameOver = false;
        gameOverRoutine = null;
    }

    // NOTE Used by the buttons
    public void LoadFirstLevel()
    {
        StartCoroutine(WaitToLoadScene(1, 1.0f));
    }
    public void LoadStartScene()
    {
        StartCoroutine(WaitToLoadScene(0, 1.0f));
    }
}
