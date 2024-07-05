using UnityEngine;
using Fusion;

[RequireComponent(typeof(NetworkCharacterControllerCustom))]
public class PlayerController : NetworkBehaviour
{
    private float horizontalInput;
    public float speed;
    private NetworkCharacterControllerCustom _myCharacterController;

    private void Awake()
    {
        _myCharacterController = GetComponent<NetworkCharacterControllerCustom>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData networkInputData)) return;

        //MOVIMIENTO

        horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = Vector3.right * horizontalInput * Runner.DeltaTime * speed;
        _myCharacterController.Move(movement);

        //SALTO

        if (networkInputData.networkButtons.IsSet(MyButtons.Jump))
        {
            _myCharacterController.Jump();
        }

        //DISPARO

        if (networkInputData.isFirePressed)
        {
           // _myWeaponHandler.Fire();
        }
    }

}
