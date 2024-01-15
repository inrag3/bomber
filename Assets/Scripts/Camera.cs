using UnityEngine;

public class Camera : MonoBehaviour
{

    [SerializeField] private Bomber _bomber;
    
    private UnityEngine.Camera _camera;

    private void Awake()
    {
        _camera = UnityEngine.Camera.main;
    }

    private void LateUpdate()
    {
        var position = _camera.transform.position;
        position = new Vector3(_bomber.transform.position.x, position.y, position.z);
        // ReSharper disable once Unity.InefficientPropertyAccess
        _camera.transform.position = position;
    }
}
