using UnityEngine;
using UnityEngine.Events;

public static class Inputs
{
    private static readonly InputActions InputAsset;

    public static Vector2 MoveInput => InputAsset.Player.Move.ReadValue<Vector2>();
    public static Vector2 AimTowardsInput => _lastAimTowardsDirection;
    public static Vector2 AimAtInput => InputAsset.Player.AimAt.ReadValue<Vector2>();
    public static bool CurrentAimStrategyIsTowards { get; private set; }

    public static UnityEvent OnFirePressed { get; } = new();

    private static Vector2 _lastAimTowardsDirection;

    static Inputs()
    {
        InputAsset = new();

        BindEvents();
        InputAsset.Enable();
    }

    private static void BindEvents()
    {
        InputAsset.Player.Fire.performed += _ => OnFirePressed.Invoke();
        InputAsset.Player.AimAt.performed += context => CurrentAimStrategyIsTowards = false;
        InputAsset.Player.AimTowards.performed += context =>
        {
            CurrentAimStrategyIsTowards = true;
            _lastAimTowardsDirection = context.ReadValue<Vector2>();
        };
    }
}