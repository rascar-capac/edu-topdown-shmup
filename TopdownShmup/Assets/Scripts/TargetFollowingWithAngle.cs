using UnityEngine;

[ExecuteInEditMode]
public class TargetFollowingWithAngle : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _height = 20f;
    [SerializeField] private float _pitchAngle = 60f;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void UpdatePosition()
    {
        if (_target == null)
        {
            return;
        }

        float zOffset = _height / Mathf.Tan(Mathf.Deg2Rad * _pitchAngle); // trigonometry: tangent A = opposite / adjacent => adjacent = opposite / tangent
        transform.position = new Vector3(_target.position.x, _height, _target.position.z - zOffset);
        transform.eulerAngles = new Vector3(_pitchAngle, 0f, 0f);
    }

    private void LateUpdate()
    {
        UpdatePosition();
    }
}
