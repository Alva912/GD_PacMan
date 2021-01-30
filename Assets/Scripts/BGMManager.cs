using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField]
    private List<AudioSource> gameBGMList;
    private static int switcher;
    public static int Switcher
    {
        get { return switcher; }
    }
    public enum GameBGM
    {
        Intro = 0,
        Normal = 1,
        Scared = 2,
        Dead = 3,
    }
    // NOTE Static background music control
    private static GameBGM currentBGM = GameBGM.Intro;
    public static GameBGM CurrentBGM
    {
        get { return currentBGM; }
        set
        {
            currentBGM = value;
            switch (currentBGM)
            {
                case GameBGM.Intro:
                    switcher = (int)GameBGM.Intro;
                    break;
                case GameBGM.Normal:
                    switcher = (int)GameBGM.Normal;
                    break;
                case GameBGM.Scared:
                    switcher = (int)GameBGM.Scared;
                    break;
                case GameBGM.Dead:
                    switcher = (int)GameBGM.Dead;
                    break;
                default:
                    switcher = 0;
                    break;
            }
        }
    }
    private void Awake()
    {
        // NOTE All tracks are played at the same time but muted
        foreach (AudioSource bgm in gameBGMList)
        {
            bgm.Play();
            bgm.mute = true;
        }
    }
    private void Update()
    {
        // NOTE Mute all tracks
        foreach (AudioSource bgm in gameBGMList)
        {
            bgm.mute = true;
        }

        // NOTE Mute current when Pause panel shows up
        if (GameObject.FindWithTag("Panel"))
        {
            gameBGMList[switcher].mute = true;
        }
        else
        {
            gameBGMList[switcher].mute = false;
        }
    }
}
