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
    public int animID_Cancel        =    Animator.StringToHash ( "Cancel"        );

    public float[] gameTimer;
    public float[] countdown;

    private int m_playerCount;
    private Dictionary<int, int> m_points;
    public Dictionary<int, int> Points { get {
            return m_points;
        } }

    private int m_catScore;
    public int CatScore { get {
            return m_catScore;
        } }

    private int m_miceScore;
    public int MiceScore { get {
            return m_miceScore;
        } }

    //Init Variables
    private bool WaitingForPlayersInit, PlayersReadyInit, DefaultGameStateInit, MiceWinInit, CatWinInit, ScoreScreenInit, LastMouseStandingInit;
    private bool m_cancel = false;

    void Awake() {
        if (GameManager.Instance == null)
            GameManager.Instance = this;
        else {
            Destroy(this.gameObject);
        }

        KillEvent.OnMouseDeath += MouseDied;
        m_points = new Dictionary<int, int>();

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

    void OnDisable() {
        KillEvent.OnMouseDeath -= MouseDied;
    }
    
    public void MouseDied(GameObject mouse) {
        int mouseAlive = animator.GetInteger(animID_MiceAlive);
        mouseAlive = mouseAlive > 0 ? mouseAlive-1 : 0;
        animator.SetInteger(animID_MiceAlive, mouseAlive);
        MouseController msc = mouse.GetComponent<MouseController>();
        int controllerNum = msc.ControllerNumber;

        var changes = new List<int>();
        foreach(int i in m_points.Keys) {
            if (i != controllerNum)
                changes.Add(i);
                //m_points[i]++;
        }
        foreach (int c in changes) m_points[c]++;
    }

    private void ResetForGame() {
        WaitingForPlayersInit = PlayersReadyInit = DefaultGameStateInit =
                MiceWinInit = CatWinInit = ScoreScreenInit = LastMouseStandingInit = false;
        countdown[0] = countdown[1];
        gameTimer[0] = gameTimer[1];
        animator.SetInteger(animID_MiceCount, m_playerCount);
        animator.SetInteger(animID_MiceAlive, m_playerCount);
    }

    public void StartGame(int[] playerMapping = null) {
        if(playerMapping != null) {
            m_playerCount = 0;
            for(int i = 0; i < playerMapping.Length; ++i) {
                if(playerMapping[i] != -1 && !m_points.ContainsKey(playerMapping[i])) {
                    m_playerCount++;
                    m_points.Add(playerMapping[i], 0);
                }
            }
        }
        else {
            m_playerCount = m_points.Keys.Count;
        }

        SceneManager.LoadScene(1); // simply loads the scene after this
        Debug.Log(m_playerCount);
        animator.SetTrigger(animID_StartGame);
        
    }

    void Update() {
        Debug.Log(this.gameObject.GetInstanceID() +"  "+ m_playerCount);
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
            m_miceScore = m_catScore = 0;
            m_points.Clear();
            WaitingForPlayersInit = true;
        }
    }

    private void PlayersReady() {
        if (!PlayersReadyInit) {
            ResetForGame();
            PlayersReadyInit = true;
            UIController.Instance.switchDisplay(CONSTANTS.UI_STATES.COUNTDOWN);
            CatController cat = GameObject.FindObjectOfType<CatController>();
            cat.HittingBlocked = true;
        }

        if (!UIController.Instance)
            return;
        animator.SetFloat(animID_GameCountDown, countdown[0]);
        if (countdown[0] >= 0.0f) {
            if (m_cancel) {
                m_cancel = false;
            }
            countdown[0] = Mathf.Clamp(countdown[0] - Time.deltaTime, 0.0f, countdown[1]);
            animator.SetFloat(animID_GameCountDown, countdown[0]);
        }

        /*
        Currently no going back, our game is awesome!
        if (Input.GetKeyDown(KeyCode.Escape)) {
            {
                animator.SetTrigger(animID_Cancel);
                m_cancel = true;
                SceneManager.LoadScene(0);
            }
        }
        */

    }

    private void DefaultGameState() {
        if (!DefaultGameStateInit) {
            DefaultGameStateInit = true;
            gameTimer[0] = gameTimer[1];
            animator.SetFloat(animID_Timer, gameTimer[0]);
            UIController.Instance.switchDisplay(CONSTANTS.UI_STATES.DEFAULT);
            CatController cat = GameObject.FindObjectOfType<CatController>();
            cat.HittingBlocked = false;
        }

    }

    private void LastMouseStanding() {
        if (!LastMouseStandingInit) {
            UIController.Instance.switchDisplay(CONSTANTS.UI_STATES.LAST_MOUSE_STANDING);
            LastMouseStandingInit = true;
        }
        gameTimer[0] = Mathf.Clamp(gameTimer[0] - Time.deltaTime, 0.0f, Mathf.Infinity);
        animator.SetFloat(animID_Timer, gameTimer[0]);
    }

    private void Pause() {
    }

    private void MouseWin(int playerNumber = -1) {
        if (!MiceWinInit) {
            PlayersReadyInit = false;
            StartGame();
            m_miceScore++;
            MiceWinInit = true;
        }
    }

    private void CatWin() {
        if (!CatWinInit) {
            PlayersReadyInit = false;
            StartGame();
            m_catScore++;
            CatWinInit = true;
        }
    }
}
