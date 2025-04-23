using UnityEngine;

public class DimensionColliderActivator : MonoBehaviour
{
    public enum DimensionMode { Only2D, Only3D }
    public DimensionMode activeInMode;

    public Collider targetCollider; // Use 3D Collider
    public DimensionManager dimensionManager; // Drag in your DimensionManager

    void Start()
    {
        if (targetCollider == null)
            targetCollider = GetComponent<Collider>();

        // Set initial state correctly at startup
        UpdateColliderState(DimensionManager.Is2DModeStatic);
    }

    void OnEnable()
    {
        DimensionManager.OnDimensionChange += UpdateColliderState;
    }

    void OnDisable()
    {
        DimensionManager.OnDimensionChange -= UpdateColliderState;
    }

    void UpdateColliderState(bool is2D)
    {
        if (targetCollider == null) return;

        bool shouldEnable =
            (activeInMode == DimensionMode.Only2D && is2D) ||
            (activeInMode == DimensionMode.Only3D && !is2D);

        targetCollider.enabled = shouldEnable;

        Debug.Log($"[DimensionActivator] {gameObject.name} → ActiveIn: {activeInMode}, Is2D: {is2D}, ColliderEnabled: {shouldEnable}");
    }
}