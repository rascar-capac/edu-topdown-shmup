using UnityEngine;

[ExecuteInEditMode]
public class TargetFollowingWithAngle : MonoBehaviour
{
    [SerializeField] private TargetHolder _targetHolder;
    [SerializeField] private float _height = 20f;
    [SerializeField] private float _pitchAngle = 60f;

    private void UpdatePosition()
    {
        if (_targetHolder.Target == null)
        {
            return;
        }

        float zOffset = _height / Mathf.Tan(Mathf.Deg2Rad * _pitchAngle); // trigonometry: tangent A = opposite / adjacent => adjacent = opposite / tangent
        transform.position = new Vector3(_targetHolder.Target.position.x, _height, _targetHolder.Target.position.z - zOffset);
        transform.eulerAngles = new Vector3(_pitchAngle, 0f, 0f);
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }

    private void OnValidate()
    {
        if (_targetHolder.IsEmpty)
        {
            Debug.LogWarning("No target provided.", this);
        }
    }
}
