using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    private const int SPACE_MINE = 1;

    private bool _flagged;
    private bool _revealed;
    private bool _isHighlighted;
    private int _spaceValue;
    private Vector3 _position;
    private List<Vector3> _neighborSpacePositions;

    public Material[] Materials;

    // getters and setters

    public Vector3 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public List<Vector3> NeighborSpacePositions
    {
        get { return _neighborSpacePositions; }
        set { _neighborSpacePositions = value; }
    }

    public int SpaceValue
    {
        get { return _spaceValue; }
        set
        {
            _spaceValue = value;
        }
    }

    public void PlaceMineOnSpace()
    {
        _spaceValue = SPACE_MINE;
        this.GetComponent<Renderer>().material = Materials[1];
    }

    public bool IsMine()
    {
        return _spaceValue == SPACE_MINE;
    }

    void Awake() { }
    // 	Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
    void Start() 
    {
        _flagged = false;
        _revealed = false;
    }
    /*    // Update is called every frame, if the MonoBehaviour is enabled.
        void Update() { }
        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        void FixedUpdate() { }
        // LateUpdate is called every frame, if the Behaviour is enabled.
        void LateUpdate() { }
        // OnGUI is called for rendering and handling GUI events.
        void OnGUI() { }
        // This function is called when the behaviour becomes disabled () or inactive.
        void OnDisable() { }
        // This function is called when the object becomes enabled and active.
        void OnEnabled() { }
        // This function is called when the MonoBehaviour will be destroyed.
        void OnDestroy() { }
        // Sent to all GameObjects when the player gets or loses focus.
        void OnApplicationFocus(bool hasFocus) { }
        // Sent to all GameObjects when the application pauses.
        void OnApplicationPause(bool pauseStatus) { }*/
    // member functions
    public void Reveal()
    {
        _revealed = true;

        // if clicked on mine
        //if (this.IsMine())
        //{
        //    //PutOutLights();
        //    // FIXME: add materials
        //    GetComponent<Renderer>().material = Materials[TILE_MINE_PRESSED];
        //    GM.Detonate(this);
        //    GM.GameOver(false); // end game with negative result
        //}
        //else
        //{
        //    GetComponent<Renderer>().material = Materials[_spaceValue];
        //    //StartCoroutine("LightUp");
        //    if (_spaceValue == 0) RevealNeighbors();
        //}

        // FIXME: add in gamemanager
        //if (_grid.AreAllTilesRevealed()) GM.GameOver(true);
    }

    /*    // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/
}
