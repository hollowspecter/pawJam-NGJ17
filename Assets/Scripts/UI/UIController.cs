using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    [SerializeField]
    private GameObject catWin;

    [SerializeField]
    private GameObject mouseWin;

    [SerializeField]
    private GameObject pauseScreen;

    [SerializeField]
    private Text timer;

    public Text countdownTimer;

	void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        catWin.SetActive(false);
        mouseWin.SetActive(false);


    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameManager.Instance.animator.SetTrigger(GameManager.Instance.animID_PauseScreen);
        }
    }

    public IEnumerator CatWin() {
        catWin.SetActive(true);
        Text txt = catWin.GetComponentInChildren<Text>();
        Color on = new Color(txt.color.r, txt.color.g, txt.color.b, 0.0f);
        Color off = new Color(txt.color.r, txt.color.g, txt.color.b, 1.0f);

        txt.color = on;
        float timer = 4.0f;
        while(timer >= 0.0f) {
            if (timer <= 0.5f)
                txt.color = Color.Lerp(on, off, timer / 0.5f);
            int tmp = (int)(timer * 10);
            float txtTimer = (float)(timer / 10.0f);
            txt.text = txtTimer.ToString();
            yield return null;
        }
        catWin.SetActive(false);
    }

    public IEnumerator MouseWin(int playerNumber = -1) {
        mouseWin.SetActive(true);
        Text txt = catWin.GetComponentInChildren<Text>();
        Color on = new Color(txt.color.r, txt.color.g, txt.color.b, 0.0f);
        Color off = new Color(txt.color.r, txt.color.g, txt.color.b, 1.0f);
        float timer = 4.0f;
        while (timer >= 0.0f) {
            if (timer <= 0.5f)
                txt.color = Color.Lerp(on, off, timer / 0.5f);
            int tmp = (int)(timer * 10);
            float txtTimer = (float)(timer / 10.0f);
            txt.text = txtTimer.ToString();
            yield return null;
        }
        mouseWin.SetActive(false);

    }

    public void Pause() {

    }

    public void UpdateClock(int type, float time) {
        string result = MakeTime(time);

        switch (type) {
            case 0: {
                    countdownTimer.enabled = false;
                    timer.text = result;
                    timer.enabled = true;
                    break;
                }
            case 1: {
                    timer.enabled = false;
                    countdownTimer.text = result;
                    countdownTimer.enabled = true;
                    break;
                }
            case 2: {
                    timer.enabled = false;
                    countdownTimer.enabled = false;
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
        return result;
    }

    public void ResetClocks() {
        countdownTimer.text = "";
        timer.text = "";
    }
}
