using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class ControllerInputHandler : MonoBehaviour
{
    /*[Header("Input Action Asset")]
    [SerializeField] private InputActionAsset inputSystem; // The input actions asset created in Unity (InputSystem)

    [Header("Action Map Name Reference")]
    [SerializeField] private string actionMapName = "Controller"; // Name of the action map in use (PlayerInputs>Player)

     // These MUST match the action names inside the input action asset EXACTLY
    // This is also case-sensitive. Used to find and bind input actions at runtime
    [Header("Action Name References")]
    [SerializeField] private string rotation = "Rotation";
    [SerializeField] private string rotationAD = "RotationAD";

    // Interal references to the actual InputAction objects retrieved from the asset
    private InputAction rotationAction;
    private InputAction rotationADAction;

    // Public read only so FirstPersonController can read them but not change them
    public Vector2 RotationInput { get; private set; }
    public Vector2 RotationADInput { get; private set; }

    private void Awake()
    {
        // Find the action map by name from the input action asset
        InputActionMap mapReference = inputSystem.FindActionMap(actionMapName);

        // Find each action inside the action map
        rotationAction = mapReference.FindAction(rotation);
        rotationADAction = mapReference.FindAction(rotationAD);
    }

    /*
    Subscribes the input actions to events so this class
    always stores the current movement, rotation, jump,
    and sprint input values
    */
    /*
    private void SubscribeActionValuesToInputEvents()
    {
        rotationAction.performed += rotationInfo => RotationInput = rotationInfo.ReadValue<Vector2>();
        rotationAction.canceled += rotationInfo => RotationInput = Vector2.zero;

        rotationADAction.performed += rotationADInfo => RotationADInput = rotationADInfo.ReadValue<Vector2>();
        rotationADAction.canceled += rotationADInfo => RotationADInput = Vector2.zero;
    }

    private void OnEnable()
    {
        /* 
        Called automatically by Unity when this component or GameObject is enabled
        WE DO NOT CALL THIS MANUALLY

        This enables the Player action map, which is the collection of input actions
        (movement, rotation, jump, sprint)
        When enabled, Unity starts listening for those inputs and updating our values

        Input can be toggled indirectly by enabling/disbaleing this componet
        or directly by enabling/disableing the action map itself
        

        inputSystem.FindActionMap(actionMapName).Enable();
    }

    private void OnDisable()
    {
        /*
        Called automatically by Unity when this component or GameObject is disabled
        WE DO NOT CALL THIS MANUALLY

        This disables the Player action map, stopping all input from being read.
        Useful for pausing the game or opening menus
        
        inputSystem.FindActionMap(actionMapName).Disable();
    }*/
}
