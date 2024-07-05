using Fusion;

public struct NetworkInputData : INetworkInput
{
    public float movementInput;
    public NetworkButtons networkButtons;
    public NetworkBool isFirePressed;
    public NetworkBool isJumpPressed;
}

public enum MyButtons
{
    Jump,
    Dash
}
