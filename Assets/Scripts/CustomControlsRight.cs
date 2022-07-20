using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class CustomControlsRight : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private XROrigin rig;
    private Vector2 inputAxis;
    private bool trigger;
    private bool grip;
    private bool secondaryButton;
    private bool primaryButton;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XROrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        device.TryGetFeatureValue(CommonUsages.triggerButton, out trigger);
        device.TryGetFeatureValue(CommonUsages.gripButton, out grip);
        device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButton);
        device.TryGetFeatureValue(CommonUsages.secondaryButton, out secondaryButton);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();
        int dY;
        if (secondaryButton)
            dY = 1;
        else if (primaryButton)
            dY = -1;
        else
            dY = 0;

        if (trigger)
            Debug.Log("Right Trigger pressed");
        else if (grip)
            Debug.Log("Grip Button pressed");
        /*else
             dY = 0;*/
        Quaternion headYaw = Quaternion.Euler(0, rig.Camera.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, dY, inputAxis.y);

        character.Move(direction * Time.fixedDeltaTime * speed);

        // Gravity
        bool isGrounded = CheckIfGrounded();
        if (isGrounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;

        //character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    void CapsuleFollowHeadset()
    {
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
