using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private GameObject[] _bugs;

    [HideInInspector] public GameState State = GameState.Start;
    [HideInInspector] public int Life = 3;
    [HideInInspector] public int Goals = 0;

    private int _score = 0;
    private int _highScore = 0;
    private AudioSource _audio;
    private bool _canLowerHealth = true;

    private void Update() {
        StartGame();
        ResetGame();
        ResetGoals();
    }

    public void StartGame() {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && State == GameState.Start) {
            State = GameState.Run;
            _score = 0;
            AddToScore(0);
            FindFirstObjectByType<FrogController>().PerformAllowMoving();
            InvokeRepeating("BugRotator", 0, 5);
        }
    }

    public void LowerHealth() {
        if (!_canLowerHealth) return;

        _canLowerHealth = false;
        Life--;
        if (Life <= 0)
            StopGame();

        Invoke("AllowLowerHealthAgain", 0.1f);
    }

    private void AllowLowerHealthAgain() => _canLowerHealth = true;

    public void AddToScore(int amount) {
        if (State != GameState.Run) return;

        _score += amount;
        UIController.Instance.SetScore(_score);
    }

    public void SetHighScore() {
        if (State != GameState.Stop) return;

        _highScore = _score > _highScore ? _score : _highScore;
        UIController.Instance.SetHighScore(_highScore);
    }

    public void StopGame() { 
        State = GameState.Stop;
        Life = 3;
        SetHighScore();
        CancelInvoke("BugRotator");
        foreach (var turtle in FindObjectsByType<TurtleFlipper>(FindObjectsSortMode.None))
            turtle.CancelInvoke();
    }

    public void ResetGame() {
        if (Input.GetKeyDown(KeyCode.R) && State == GameState.Stop) {
            State = GameState.Start;
            FindFirstObjectByType<FrogController>().ResetFrog();
            FindFirstObjectByType<FrogController>().CanMove = false;
            Goals = 3;
            foreach (var bug in _bugs)
                bug.SetActive(false);

            foreach (var row in FindObjectsByType<MoveController>(FindObjectsSortMode.None))
                row.ResetRow();

            foreach (var croc in FindObjectsByType<CrocFlipper>(FindObjectsSortMode.None))
                croc.ResetCroc();

            foreach (var turtle in FindObjectsByType<TurtleFlipper>(FindObjectsSortMode.None))
                turtle.ResetTurtle();
        }
    }

    private void ResetGoals() {
        if (Goals < 3) return;

        Goals = 0;
        foreach (var row in FindObjectsByType<ScoreTrigger>(FindObjectsSortMode.None))
            row.Goal.SetActive(false);
    }

    private void BugRotator() {
        if (_bugs.Length == 0) return;

        var bugId = Random.Range(0, _bugs.Length);
        _bugs[bugId].SetActive(!_bugs[bugId].activeSelf);
    }

    private void Awake() { 
        Instance ??= this;
        _audio = GetComponent<AudioSource>();
        _highScore = PlayerPrefs.GetInt("highscore", 0);
    }
}