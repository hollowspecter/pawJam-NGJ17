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
    private Text timer;

	void Awake() {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        catWin.SetActive(false);
        mouseWin.SetActive(false);


    }

    public void CatWin() {
        catWin.SetActive(true);
    }

    public void MouseWin(int playerNumber = -1) {
        mouseWin.SetActive(true);

    }

    public void UpdateTimer(float time) {
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
        result += seconds + ".";


        if (milliSecs < 10) {
            result += "0";
        }
        result += milliSecs.ToString();
        timer.text = result;
    }
}
