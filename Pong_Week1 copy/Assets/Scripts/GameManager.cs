using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Player P1;
    public Player P2;

    private Player[] Players = new Player[2];

    public float InitialBallSpeed;
    public float BallSpeedIncriment;
    public int PointsToVictory;

    public TextMeshProUGUI messagesGUI;
    public TextMeshProUGUI PONGTITLE;
    public TextMeshProUGUI Paused;
    public TextMeshProUGUI PressTab;

    public AudioClip GameOver;
    public AudioClip Serve;
    private AudioSource m_AudioSource;

    private string _state;
    public string State
    {
        get => _state;
        set
        {
            _state = value;
        }
    }

    public KeyCode PauseKey;
    public KeyCode TitleKey;
    public KeyCode StartKey;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        Players[0] = P1;
        Players[1] = P2;

        m_AudioSource = GetComponent<AudioSource>();
        Paused.enabled = false;


    }

    private void Start()
    {
        State = "GameOver";
    }

    private void Update()
    {
        if (State == "GameOver")
        {
            messagesGUI.enabled = false;
            PONGTITLE.enabled = true;
            PressTab.enabled = true;
        }
        if (State == "GameOver" && Input.GetKeyUp(TitleKey))
        {
            State = "Serve";
            PONGTITLE.enabled = false;
            messagesGUI.enabled = true;
            PressTab.enabled = false;
        }

        else if (State == "Serve" && Input.GetKeyUp(StartKey))
        {
            //messagesGUI.enabled = true;
            PONGTITLE.enabled = false;
            State = "Play";
            m_AudioSource.PlayOneShot(Serve);
            messagesGUI.enabled = false;
            PressTab.enabled = false;
        }

        else if (Input.GetKeyUp(PauseKey))
        {
            //State = State == "Play" ? "Pause": "Play";
            if (State == "Play")
            {
                State = "Pause";
                Paused.enabled = true;
            }

            else
            {
                State = "Play";
                Paused.enabled = false;
            }
        }
    }

    public void UpdateScore(int player)
    {
        //Debug.Log("1");
        Players[player - 1].Score++;

        foreach (Player p in Players)
        {
            if (p.Score >= PointsToVictory)
            {
                ResetGame();
                break;

            }
        }
    }
    private void ResetGame()
    {
        State = "GameOver";
        m_AudioSource.PlayOneShot(GameOver);

        foreach (Player p in Players)
        {
            p.Score = 0;
        }
    }
    public void PlaySound(AudioClip clip, float volume=1.0f)
    {
        m_AudioSource.volume = volume;
        m_AudioSource.PlayOneShot(clip);
    }
}