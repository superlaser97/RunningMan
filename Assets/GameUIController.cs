using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("KeyboardControls")]
    [SerializeField]
    private bool useKeyboardControls = false;
    [SerializeField]
    private KeyCode leftMovement = KeyCode.Minus;
    [SerializeField]
    private KeyCode rightMovement = KeyCode.Equals;

    [Header("UI Elements")]
    [SerializeField]
    private Image movementSpdBar;

    private float targetMovementSpd;

    private void Update()
    {
        if(useKeyboardControls)
        {
            if(Input.GetKeyDown(KeyCode.Minus) && PlayerController.instance)
                PlayerController.instance.OnMovementBtnClick(PlayerController.MovementDirection.LEFT);

            if (Input.GetKeyDown(KeyCode.Equals) && PlayerController.instance)
                PlayerController.instance.OnMovementBtnClick(PlayerController.MovementDirection.RIGHT);
        }

        UpdateMovementSpdBar();
    }

    private void UpdateMovementSpdBar()
    {
        if (movementSpdBar && PlayerController.instance)
            targetMovementSpd = PlayerController.instance.GetCurrentSpeed();

        movementSpdBar.fillAmount = Mathf.Lerp(movementSpdBar.fillAmount, targetMovementSpd, 10 * Time.deltaTime);
    }

    public void LeftMovementBtnClick()
    {
        if (PlayerController.instance)
            PlayerController.instance.OnMovementBtnClick(PlayerController.MovementDirection.LEFT);
    }

    public void RightMovementBtnClick()
    {
        if (PlayerController.instance)
            PlayerController.instance.OnMovementBtnClick(PlayerController.MovementDirection.RIGHT);
    }
}
