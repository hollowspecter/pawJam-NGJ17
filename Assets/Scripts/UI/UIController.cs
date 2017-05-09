using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    [SerializeField]
    private GameObject m_defaultScore;

    [SerializeField]
    private GameObject m_getReady;

    [SerializeField]
    private GameObject m_lastMouseStanding;

    [SerializeField]
    private Text text_lastMouseStandingTimer;

    [SerializeField]
    private Text text_countdownTimer;


    
    [SerializeField]
    private Text m_catMiceScore;
    [SerializeField]
    private Text[] scoreboard;

        
    
	void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        KillEvent.OnMouseDeath += writeScoreboard;

    }

    void OnDisable() {
        KillEvent.OnMouseDeath -= writeScoreboard;
    }

    void Update() {
        text_lastMouseStandingTimer.text = MakeTime(GameManager.Instance.gameTimer[0]);
        text_countdownTimer.text = MakeTime(GameManager.Instance.countdown[0]);

        m_catMiceScore.text = GameManager.Instance.CatScore + " : " + GameManager.Instance.MiceScore;

        writeScoreboard();
    }

    private void writeScoreboard(GameObject go = null) {
        foreach(Text txt in scoreboard) {
            txt.text = "";
        }

        for(int i = 1; i < 10; ++i) {
            Text txt = scoreboard[(i-1) / 3];
            if(GameManager.Instance.Points.ContainsKey(i)) {
                txt.text += GameManager.Instance.Points[i];
            }
            txt.text += "\n";
        }
    }

    public void switchDisplay(CONSTANTS.UI_STATES state) {
        switch (state) {
            case CONSTANTS.UI_STATES.DEFAULT: {
                    m_defaultScore.SetActive(true);
                    m_getReady.SetActive(false);
                    m_lastMouseStanding.SetActive(false);
                    break;
                }
            case CONSTANTS.UI_STATES.LAST_MOUSE_STANDING: {
                    m_defaultScore.SetActive(false);
                    m_getReady.SetActive(false);
                    m_lastMouseStanding.SetActive(true);
                    break;
                }
            case CONSTANTS.UI_STATES.COUNTDOWN: {
                    m_defaultScore.SetActive(false);
                    m_getReady.SetActive(true);
                    m_lastMouseStanding.SetActive(false);
                    break;
                }
        }
    }


    public string MakeTime(float time) {
        int seconds;

        seconds = (int)time;
        if (time < 0)
            time += seconds;
        else
            time -= seconds;

        time *= 100.0f;
        int milliSecs = (int)Mathf.Abs(time);

        int minutes = seconds / 60;
        seconds -= minutes * 60;

        string result = "";
        if (minutes > 0) {
            result += minutes.ToString() + ":";
        }
        else
            result += "   ";

        if (seconds < 10)
            result += "0";
        result += seconds + ":";


        if (milliSecs < 10) {
            result += "0";
        }
        result += milliSecs.ToString();
        return result.Trim();
    }
}
