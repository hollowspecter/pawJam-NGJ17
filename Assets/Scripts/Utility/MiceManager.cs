using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// SINGLETON

public class MiceManager : MonoBehaviour {

    public static MiceManager s_instance = null;

    [SerializeField]
    private int m_iMaxNumPlayers = 4;
    [SerializeField]
    private Text m_debugText;

    private int[] playerMapping;
    private bool[] playerJoined;
    private int currPlayer = -1;

    void Awake()
    {
        // singleton
        if (s_instance == null) s_instance = this;
        else Destroy(this);

        // init arrays
        playerMapping = new int[m_iMaxNumPlayers];
        playerJoined = new bool[m_iMaxNumPlayers];
        //init with starting values
        for (int i=0; i<m_iMaxNumPlayers; ++i)
        {
            playerMapping[i] = -1;
            playerJoined[i] = false;
        }

    }

	// Update is called once per frame
	void Update () {
        for (int i = 1; i <= m_iMaxNumPlayers; ++i)
        {
            CheckPlayerInput(i);
        }
	}

    void CheckPlayerInput(int joystickNumber)
    {
        float h = Input.GetAxis("Horizontal_" + joystickNumber);
        float v = Input.GetAxis("Vertical_" + joystickNumber);
        bool joining = h != 0f || v != 0f;
        if (joining) AddPlayer(joystickNumber);
    }

    public void CheckToStartGame()
    {
        // enough players?
        if (currPlayer < 0) return;
        else
        {
            GameManager.Instance.StartGame(getPlayerMapping());
        }
    }

    void AddPlayer(int joystickNumber)
    {
        // check if player has been added before
        if (playerJoined[joystickNumber-1]) return;
        // add!
        currPlayer++;
        playerMapping[currPlayer] = joystickNumber;
        playerJoined[joystickNumber - 1] = true;
        // react to it
        if (m_debugText != null)
        {
            m_debugText.text += "\nPlayer " + joystickNumber + " joined the game!";
        }
    }

    public int[] getPlayerMapping()
    {
        return playerMapping;
    }

    public int getNumPlayers()
    {
        return currPlayer + 1;
    }
}
