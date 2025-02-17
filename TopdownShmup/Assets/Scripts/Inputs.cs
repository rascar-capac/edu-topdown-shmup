using UnityEngine;
using UnityEngine.Events;

public static class Inputs
{
    private static readonly InputActions InputAsset;

    public static Vector2 MoveInput => InputAsset.Player.Move.ReadValue<Vector2>();
    public static Vector2 AimAtInput => InputAsset.Player.AimAt.ReadValue<Vector2>();
    public static Vector2 AimTowardsInput => InputAsset.Player.AimTowards.ReadValue<Vector2>();

    public static UnityEvent OnFirePressed { get; } = new();

    static Inputs()
    {
        InputAsset = new();

        BindEvents();
        InputAsset.Enable();
    }

    private static void BindEvents()
    {
        InputAsset.Player.Fire.performed += _ => OnFirePressed.Invoke();
    }
}
