using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public Animator animator;
    private List<int> animID_stateHashes;

    public int animID_MiceCount    =    Animator.StringToHash ( "MiceCount"   );
    public int animID_MiceAlive    =    Animator.StringToHash ( "MiceAlive"   );
    public int animID_StartGame    =    Animator.StringToHash ( "StartGame"   );
    public int animID_Timer        =    Animator.StringToHash ( "Timer"       );
    public int animID_PauseScreen  =    Animator.StringToHash ( "PauseScreen" );
    public int animID_ScoreScreen  =    Animator.StringToHash ( "ScoreScreen" );

    public float timer;



    //Init Variables
    private bool WaitingForPlayersInit, PlayersReadyInit, DefaultGameStateInit, PauseInit, MiceWinInit, CatWinInit, ScoreScreenInit;
    

    void Awake() {
        if (GameManager.Instance == null)
            GameManager.Instance = this;
        else
            Destroy(this);

        animID_stateHashes = new List<int>();
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.WaitingForPlayers") );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.PlayersReady")      );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.DefaultGameState")  );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.Pause")             );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.MiceWin")           );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.CatWin")            );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.ScoreScreen")       );
        animator = this.GetComponent<Animator>();
    }
    
    public void MouseDied() {
        animator.SetInteger(animID_MiceAlive, animator.GetInteger(animID_MiceAlive) - 1);
    }

    public void StartGame(int playerAmount) {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        if (state.fullPathHash != animID_stateHashes[0])
            return;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // simply loads the scene after this
        animator.SetInteger(animID_MiceCount, playerAmount);
        animator.SetInteger(animID_MiceAlive, playerAmount);
        animator.SetTrigger(animID_StartGame);
    }


    void Update() {
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
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
                    DefaultGameState();
                    break;
                }
            case 3: {
                    Pause();
                    break;
                }
            case 4: {
                    MiceWin();
                    break;
                }
            case 5: {
                    CatWin();
                    break;
                }
            case 6: {
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

        timer = Mathf.Clamp(timer - Time.deltaTime, 0.0f, Mathf.Infinity);
        animator.SetFloat(animID_Timer, timer);
        UIController.Instance.UpdateTimer(timer);
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
