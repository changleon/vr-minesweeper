using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;


//---------------
// Mouse logic:
// - Left&Right:    if(tile is revealed)        Reveal Neighbors
// - Left click:    if(not after Left&Right)    Reveal Tile
// - Right click:   if(not after Left&Right)    Flag Tile

public class PlayerInput : MonoBehaviour
{

    //public float speed = 1;
    public XRNode inputSource;
    //public float gravity = -9.81f;
    //public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    //private float fallingSpeed;
    private XROrigin rig;
    //private Vector2 inputAxis;
    private bool trigger;
    private bool grip;
    private CharacterController character;
    //-------------------------------------
    // Variable Declarations

    // static variables
    private static bool _rightAndLeftPressed;
    private static bool _revealAreaIssued;
    private static bool _initialClickIssued;    // used to track first-click-death and start-timer


    // private variables
    //private GridScript _grid;

    // handles
    //public UIManager UI;
    public Canvas PauseMenu;

    //--------------------------------------------------------
    // Function Definitions

    // getters & setters
    //public GridScript Grid
    //{
    //    get { return _grid; }
    //    set { _grid = value; }
    //}

    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }
    public static bool InitialClickIssued
    {
        get { return _initialClickIssued; }
        set
        {
            _initialClickIssued = value;
        }
    }

    // unity functions

    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        //device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.gripButton, out grip);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        //int dY = 0;
        if (trigger)
            Debug.Log("Left Trigger pressed");
        else if (grip)
            Debug.Log("Grip Button pressed");
        /*else
             dY = 0;*/
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        //Vector3 direction = headYaw * new Vector3(inputAxis.x, dY, inputAxis.y);

        //character.Move(direction * Time.fixedDeltaTime * speed);

        // Gravity
        //character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }


    void CapsuleFollowHeadset()
    {
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    // member functions
    //void ScanForKeyStroke()
    //{
    //    if (Input.GetKeyDown("escape")) TogglePauseMenu();
    //}

    //public void TogglePauseMenu()
    //{
    //    // Toggle pause menu and companion components
    //    //PauseMenu.GetComponentInChildren<Canvas>().enabled = !PauseMenu.enabled; 
    //    PauseMenu.enabled = !PauseMenu.enabled;
    //    GameObject.FindGameObjectWithTag("UILight").GetComponent<Light>().intensity = (PauseMenu.enabled ? 1 : 0);

    //    // update gamestate
    //    if (!GameManager.IsGameOver)
    //        GameManager.IsGamePaused = PauseMenu.enabled;

    //    Time.timeScale = System.Convert.ToSingle(!PauseMenu.enabled);
    //    //Debug.Log("PLAYERINPUT:: TimeScale: " + Time.timeScale);
    //}

    public void OnMouseOver(Space space)
    {

        //// RIGHT CLICK: FLAG
        //if (!_revealAreaIssued && Input.GetMouseButtonDown(1))
        //{
        //    if (!Input.GetMouseButton(0) && !tile.IsRevealed())
        //        tile.ToggleFlag();
        //}

        //// LEFT CLICK: HIGHLIGHT TILE
        //if (Input.GetMouseButton(0))
        //{
        //    if (!tile.IsRevealed() && !tile.IsFlagged())
        //        _grid.HighlightTile(tile.GridPosition);

        //    // LEFT & RIGHT CLICK: HIGHLIGHT AREA
        //    if (Input.GetMouseButton(1))
        //    {
        //        _rightAndLeftPressed = true;
        //    }
        //}

        //// LEFT & RIGHT RELEASE: REVEAL NEIGHBORS IF ENOUGH NEIGHBOR FLAGGED
        //if (_rightAndLeftPressed)
        //{
        //    _grid.HighlightArea(tile.GridPosition);
        //    if (!tile.IsRevealed() && !tile.IsFlagged()) tile.Highlight();
        //    if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        //    {
        //        _revealAreaIssued = true;
        //        if (tile.IsRevealed() && tile.IsNeighborsFlagged())
        //        {
        //            _grid.RevealArea(tile);
        //        }
        //        else
        //        {
        //            _grid.RevertHighlightArea(tile.GridPosition);
        //            _grid.RevertHighlightTile(tile.GridPosition);
        //        }
        //    }
        //}

        //// LEFT RELEASE: REVEAL
        //if (Input.GetMouseButtonUp(0) && !_revealAreaIssued)
        //{
        //    if (!tile.IsFlagged() && !tile.IsRevealed())
        //    {
        //        if (!_initialClickIssued)
        //        {
        //            if (tile.IsMine())
        //                _grid.SwapTileWithMineFreeTile(tile.GridPosition);

        //            _initialClickIssued = true;
        //            GetComponent<GameManager>().StartTimer();
        //            tile.Reveal();
        //        }

        //        else
        //        {
        //            if (!Input.GetMouseButton(1))
        //                tile.Reveal();
        //        }
        //    }

        //}



        //if (!Input.GetMouseButton(0) || !Input.GetMouseButton(1))
        //{
        //    _rightAndLeftPressed = false;
        //}

        //if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1))
        //{
        //    _revealAreaIssued = false;
        //}
    }

    //public void OnMouseExit(Space space)
    //{

    //    // revert highlighted tiles
    //    //if (!tile.IsRevealed() && !tile.IsFlagged())
    //    //    tile.RevertHighlight();

    //    //foreach (Vector2 pos in tile.NeighborTilePositions)
    //    //{
    //    //    Tile neighbor = _grid.Map[(int)pos.x][(int)pos.y];
    //    //    if (!neighbor.IsRevealed() && !neighbor.IsFlagged())
    //    //        neighbor.RevertHighlight();
    //    //}

    //}

}
