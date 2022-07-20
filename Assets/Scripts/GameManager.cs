using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    #region Singleton
    static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
    } 
    #endregion
    //----------------------------------------
    // Variable Declarations

    // static variables
    public static bool IsGamePaused;
    public static bool IsGameOver;

    // handles
    public GameObject GridPrefab;
    //public UIManager UI;
    private GridScript _grid;
    public ParticleSystem[] Explosions;
    //private ScoreManager _scoreManager;

    // private variables
    private Transform _gridtf;
    private GameSettings _settings;

    // score variables
    private float _startTime;   // needed for time counter
    private float _endTime;     // constitutes score
    private int _flagCount;

    //-----------------------------------------
    // Function Definitions

    // getters & setters
    public GameSettings Settings
    {
        get { return _settings; }
        set { _settings = value; }
    }


    // unity functions
    //void Awake()
    //{
    //    _settings = new GameSettings();
    //    _settings = GameSettings.Intermediate;
    //    //_scoreManager = GetComponent<ScoreManager>();

    //}

    void Start()
    {
        StartNewGame(GameSettings.Beginner);
    }
    
    private void Update()
    {
        //UI.UpdateFlagText(_flagCount);
        //if (PlayerInput.InitialClickIssued && !IsGamePaused && !IsGameOver)
        //{
        //    UI.UpdateTimeText((int)(Time.time - _startTime));
        //}
    }

    // member functions
    public void StartNewGame(GameSettings settings)
    {
        // delete current grid in the scene & instantiate new grid
        // using the settings that are read from UI Input fields
        //Destroy(GameObject.Find("Grid(Clone)"));
        _gridtf = ((GameObject)Instantiate(GridPrefab, new Vector3(0, 0, 0), Quaternion.identity)).transform;
        _grid = _gridtf.GetComponent<GridScript>();

        _settings = settings;
        _grid.GenerateMap(_settings);    // grid manager "_grid" generates the map with given settings

        //// update handles in companion scripts
        //GetComponent<PlayerInput>().Grid = _grid;

        //ResetGameState();
        //UI.ResetHUD(_flagCount);

        //GameObject.Find("Skybox Camera").GetComponent<SkyboxScript>().rotation = GetRandomVector();


    }

    Vector3 GetRandomVector()
    {
        Vector3 v = new Vector3();
        int rnd = Random.Range(-3, 3);  // [-3, 2]

        if (rnd == -3)
            v.z = -1;
        if (rnd == -2)
            v.y = -1;
        if (rnd == -1)
            v.x = -1;
        if (rnd == 0)
            v.x = 1;
        if (rnd == 1)
            v.y = 1;
        if (rnd == 2)
            v.z = 1;

        v *= 0.025f;

        return v;
    }

    public void StartTimer()
    {
        _startTime = Time.time;
    }

    public void GameOver(bool win)
    {
        IsGameOver = true;

        //_grid.RevealMines();

        //// HUD
        //UI.HUD.GameStateText.enabled = true;
        //UI.HUD.GameStateText.text = "Game: " + (win ? " Won" : " Lost");
        //_endTime = Time.time - _startTime;
        //Debug.Log("GAME WON:" + win + " | GAME ENDED IN " + _endTime + " SECONDS.");

        //// score
        //IsGamePaused = true;
        //if (win && _settings.Name != "custom")
        //{
        //    _scoreManager.PlayerScore = new Score(_endTime, _settings.Name);

        //    // if score top 10 of its difficulty
        //    if (_scoreManager.PlayerScore.IsHighScore())
        //    {
        //        UI.EnableScoreCanvas(_scoreManager.PlayerScore);
        //    }
        //    else
        //    {
        //        Debug.Log("NOT HIGH SCORE: " + _scoreManager.PlayerScore.TimePassed);
        //    }
        //}

    }

    public void UpdateFlagCounter(bool condition)
    {
        _flagCount += condition ? -1 : 1;
    }   // true: increment | false: decrement

    private void ResetGameState()
    {
        //PlayerInput.InitialClickIssued = false;
        IsGamePaused = false;
        IsGameOver = false;
        _flagCount = _settings.Mines;
    }

    public void Detonate(Space space)
    {
        int index = Random.Range(0, Explosions.Length);
        Explosions[index].transform.position = space.transform.position + new Vector3(0, 1, 0);
        Explosions[index].Play();
    }
}

[Serializable]
public class GameSettings
{
    // static constant settings
    public static readonly GameSettings Testing = new GameSettings(3, 3, 3, 1, "testing");

    public static readonly GameSettings Beginner = new GameSettings(5, 5, 5, 14, "beginner");
    public static readonly GameSettings Intermediate = new GameSettings(8, 8, 8, 63, "intermediate");
    public static readonly GameSettings Expert = new GameSettings(10, 10, 10, 206, "expert");

    // fields
    [SerializeField] private int _height;
    [SerializeField] private int _width;
    [SerializeField] private int _depth;
    [SerializeField] private int _mines;
    [SerializeField] private string _name;  // setting name is a key in DB

    public int Height
    {
        get { return _height; }
    }

    public int Width
    {
        get { return _width; }
    }

    public int Depth
    {
        get { return _depth; }
    }

    public int Mines
    {
        get { return _mines; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }


    public GameSettings(int w, int h, int d, int m, string s)
    {
        _width = w;
        _height = h;
        _depth = d;
        _mines = m;
        _name = s;
    }

    public GameSettings()
    {
    }

    // member functions
    public void Set(int w, int h, int d, int m)
    {
        _width = w;
        _height = h;
        _depth = d;
        _mines = m;
    }

    public bool isValid()
    {
        if  // invalid conditions
            (
            (_width <= 0 || _height <= 0 || _depth <= 0 || _mines <= 0) || // no negative
            (_mines >= _width * _height * _depth) || // no impossible game ( m > w*h )
            
            //FIXME: ???
            (_height > 24 || _width > 35)    // no screen overflow 
            )

            return false;

        // if everything's ok, return true
        return true;
    }
}


