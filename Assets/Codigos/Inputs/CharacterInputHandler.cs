using UnityEngine;
using Fusion;

public class CharacterInputHandler : MonoBehaviour
{
    private NetworkInputData _inputData;

    private bool _isJumpPressed;
    private bool _isFirePressed;

    void Start()
    {
        _inputData = new NetworkInputData();
    }

    void Update()
    {
        _inputData.movementInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isJumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _isFirePressed = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _inputData.networkButtons.Set(MyButtons.Dash, true);
        }
    }

    public NetworkInputData GetLocalInputs()
    {
        _inputData.isFirePressed = _isFirePressed;
        _isFirePressed = false;

        _inputData.networkButtons.Set(MyButtons.Jump, _isJumpPressed);
        _isJumpPressed = false;

        return _inputData;
    }
}
