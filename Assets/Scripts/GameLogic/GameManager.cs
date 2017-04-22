using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    private Animator m_anim;
    private List<int> animID_parameterHashes;
    private List<int> animID_stateHashes;

    //Init Variables
    private bool WaitingForPlayersInit, PlayersReadyInit, DefaultGameStateInit, PauseInit, MiceWinInit, CatWinInit, ScoreScreenInit;
    

    void Awake() {
        GameManager.Instance = this;
        animID_parameterHashes = new List<int>();
        animID_parameterHashes.Add(   Animator.StringToHash("MiceCount")    );
        animID_parameterHashes.Add(   Animator.StringToHash("MiceAlive")    );
        animID_parameterHashes.Add(   Animator.StringToHash("StartGame")    );
        animID_parameterHashes.Add(   Animator.StringToHash("TimesUp")      );
        animID_parameterHashes.Add(   Animator.StringToHash("PauseScreen")  );
        animID_parameterHashes.Add(   Animator.StringToHash("ScoreScreen")  );

        animID_stateHashes = new List<int>();
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.WaitingForPlayers") );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.PlayersReady")      );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.DefaultGameState")  );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.Pause")             );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.MiceWin")           );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.CatWin")            );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.ScoreScreen")       );
        m_anim = GetComponent<Animator>();
    }

	public void StartGame() {
        
    }


    void Update() {
        AnimatorStateInfo state = m_anim.GetCurrentAnimatorStateInfo(0);
        int index = animID_stateHashes.IndexOf(state.fullPathHash);
        switch (index) {
            case 0: {
                    WaitingForPlayers();
                    break;
                }
            case 1: {
                    PlayersReady();
                    break;
                }
            case 2: {
                    Pause();
                    break;
                }
            case 3: {
                    MiceWin();
                    break;
                }
            case 4: {
                    CatWin();
                    break;
                }
            case 5: {
                    ScoreScreen();
                    break;
                }
            
        }
    }

    private void WaitingForPlayers() {
        if (!WaitingForPlayersInit) {

        }
        WaitingForPlayersInit = true;

    }

    private void PlayersReady() {
        if (!PlayersReadyInit) {

        }
        PlayersReadyInit = true;

    }

    private void DefaultGameState() {
        if (!DefaultGameStateInit) {

        }
        DefaultGameStateInit = true;

    }

    private void Pause() {
        if (!PauseInit) {

        }
        PauseInit = true;

    }

    private void ScoreScreen() {
        if (!ScoreScreenInit) {

        }
        ScoreScreenInit = true;

    }

    private void MiceWin() {
        if (!MiceWinInit) {

        }
        MiceWinInit = true;

    }

    private void CatWin() {
        if (!CatWinInit) {

        }
        CatWinInit = true;

    }
}
