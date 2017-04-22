using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public Animator animator;
    private List<int> animID_stateHashes;

    public int animID_MiceCount     =    Animator.StringToHash ( "MiceCount"     );
    public int animID_MiceAlive     =    Animator.StringToHash ( "MiceAlive"     );
    public int animID_Timer         =    Animator.StringToHash ( "Timer"         );
    public int animID_GameCountDown =    Animator.StringToHash ( "GameCountdown" );
    public int animID_StartGame     =    Animator.StringToHash ( "StartGame"     );
    public int animID_PauseScreen   =    Animator.StringToHash ( "PauseScreen"   );
    public int animID_ScoreScreen   =    Animator.StringToHash ( "ScoreScreen"   );

    public float[] gameTimer;

    private int m_playerCount;
    private List<int> m_points;

    [SerializeField]
    private float[] m_countdown;




    //Init Variables
    public bool WaitingForPlayersInit, PlayersReadyInit, DefaultGameStateInit, PauseInit, MiceWinInit, CatWinInit, ScoreScreenInit, LastMouseStandingInit;
    

    void Awake() {
        if (GameManager.Instance == null)
            GameManager.Instance = this;
        else
            Destroy(this);

        m_points = new List<int>();

        animID_stateHashes = new List<int>();
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.WaitingForPlayers") );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.PlayersReady")      );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.DefaultGameState")  );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.Pause")             );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.MiceWin")           );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.CatWin")            );
        animID_stateHashes.Add(   Animator.StringToHash("Base Layer.LastMouseStanding") );
        animator = this.GetComponent<Animator>();
    }
    
    public void MouseDied() {
        int mouseAlive = animator.GetInteger(animID_MiceAlive);
        Debug.Log(mouseAlive);
        mouseAlive = mouseAlive > 0 ? mouseAlive-1 : 0;
        Debug.Log(mouseAlive);
        animator.SetInteger(animID_MiceAlive, mouseAlive);
    }

    public void StartGame(int playerAmount) {
        //animatorstateinfo state = animator.getcurrentanimatorstateinfo(0);
        //if (state.fullpathhash != animid_statehashes[0] || state.fullpathhash)
        //    return;

        
        if(playerAmount != m_playerCount) {
            m_playerCount = playerAmount;
            for(int i = 0; i < m_playerCount; ++i) {
                m_points.Add(0);
            }
        }

        SceneManager.LoadScene(1); // simply loads the scene after this
        animator.SetInteger(animID_MiceCount, playerAmount);
        animator.SetInteger(animID_MiceAlive, playerAmount);
        StartCoroutine(Starting());
    }

    public IEnumerator Starting() {
        m_countdown[0] = m_countdown[1];
        animator.SetFloat(animID_GameCountDown, m_countdown[0]);
        animator.SetTrigger(animID_StartGame);
        UIController.Instance.countdownTimer.enabled = true;
        while (m_countdown[0] >= 0.0f) {
            yield return null;
            m_countdown[0] = Mathf.Clamp(m_countdown[0] - Time.deltaTime, 0.0f, m_countdown[1]);
            animator.SetFloat(animID_GameCountDown, m_countdown[0]);
            UIController.Instance.UpdateClock(1, m_countdown[0]);
        }
        UIController.Instance.countdownTimer.enabled = false;
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
                    MouseWin();
                    break;
                }
            case 5: {
                    CatWin();
                    break;
                }
            case 6: {
                    LastMouseStanding();
                    break;
                }
             
        }
    }

    private void WaitingForPlayers() {
        if (!WaitingForPlayersInit) {
            WaitingForPlayersInit = true;
        }

    }

    private void PlayersReady() {
        if (!PlayersReadyInit) {
            PlayersReadyInit = true;
        }

    }

    private void DefaultGameState() {
        if (!DefaultGameStateInit) {
            UIController.Instance.ResetClocks();
            DefaultGameStateInit = true;
            gameTimer[0] = gameTimer[1];
            animator.SetFloat(animID_Timer, gameTimer[0]);
            WaitingForPlayersInit = PlayersReadyInit = DefaultGameStateInit = 
                PauseInit = MiceWinInit = CatWinInit = ScoreScreenInit = LastMouseStandingInit = false;
            UIController.Instance.UpdateClock(2, 0.0f);
        }

        
    }

    private void LastMouseStanding() {
        if (!LastMouseStandingInit) {
            LastMouseStandingInit = true;
        }
        gameTimer[0] = Mathf.Clamp(gameTimer[0] - Time.deltaTime, 0.0f, Mathf.Infinity);
        animator.SetFloat(animID_Timer, gameTimer[0]);
        UIController.Instance.UpdateClock(0, gameTimer[0]);
    }

    private void Pause() {
        if (!PauseInit) {
            PauseInit = true;
        }
    }

    private void MouseWin(int playerNumber = -1) {
        if (!MiceWinInit) {
            StartCoroutine(UIController.Instance.MouseWin());
            StartGame(m_playerCount);
            MiceWinInit = true;
        }
    }

    private void CatWin() {
        if (!CatWinInit) {
            StartCoroutine(UIController.Instance.CatWin());
            StartGame(m_playerCount);
            CatWinInit = true;
        }
    }
}
