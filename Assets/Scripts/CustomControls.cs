using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class CustomControls : MonoBehaviour
{
    public float speed = 1;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    //private float fallingSpeed;
    private XROrigin rig;
    private Vector2 inputAxis;

    public XRNode inputSourceRight;
    private bool triggerRight;
    private bool gripRight;
    private bool secondaryButtonRight;
    private bool primaryButtonRight;
    
    public XRNode inputSourceLeft;
    private bool triggerLeft;
    private bool gripLeft;

    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("CustomControls starting...");
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice deviceRight = InputDevices.GetDeviceAtXRNode(inputSourceRight);

        deviceRight.TryGetFeatureValue(CommonUsages.triggerButton, out triggerRight);
        deviceRight.TryGetFeatureValue(CommonUsages.gripButton, out gripRight);
        deviceRight.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonRight);
        deviceRight.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButtonRight);

        InputDevice deviceLeft = InputDevices.GetDeviceAtXRNode(inputSourceLeft);

        deviceLeft.TryGetFeatureValue(CommonUsages.triggerButton, out triggerLeft);
        deviceLeft.TryGetFeatureValue(CommonUsages.gripButton, out gripLeft);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        int dY;
        if (secondaryButtonRight)
            dY = 1;
        else if (primaryButtonRight)
            dY = -1;
        else
            dY = 0;

        if (triggerRight)
            Debug.Log("Right Trigger pressed");
        else if (gripRight)
            Debug.Log("Grip Button pressed");

        if (triggerLeft)
            Debug.Log("Left Trigger pressed");
        else if (gripLeft)
            Debug.Log("Grip Button pressed");

        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, dY, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        // Gravity
        //bool isGrounded = CheckIfGrounded();
        //if (isGrounded)
        //    fallingSpeed = 0;
        //else
        //    fallingSpeed += gravity * Time.fixedDeltaTime;

        //character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset()
    {
        //Debug.Log("CustomControls CapsuleFollowHeadset...");
        character.height = rig.CameraInOriginSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.Camera.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
