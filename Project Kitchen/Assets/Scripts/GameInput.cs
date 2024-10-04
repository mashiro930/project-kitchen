using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInterAction;
    public event EventHandler OnOperateAction;
    
    private GameControl gameControl;

    private void Awake()
    {
        gameControl = new GameControl();
        gameControl.Player.Enable();

        gameControl.Player.Interact.performed += Interact_Performed;
        gameControl.Player.Operate.performed += Operate_Performed;

    }
    private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInterAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetMovementDirectionNormalized()
    {
        Vector2 inputVector2 = gameControl.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(inputVector2.x, 0, inputVector2.y);

        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0, vertical);

        direction = direction.normalized;

        return direction;
    }
}
