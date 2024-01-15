using Infrastructure;
using Services.Input;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BomberMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private CharacterController _controller;
    private IInputService _inputService;
    private UnityEngine.Camera _camera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputService = Game.InputService;
    }

    private void Start()
    {
        _camera = UnityEngine.Camera.main;
    }

    private void Update()
    {
        var movement = Vector3.zero;
        if (_inputService.Axis.magnitude > 0.001f)
        {
            movement = _camera.transform.TransformDirection(_inputService.Axis);
            movement.y = 0f;
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.z))
            {
                movement.z = 0f;
            }
            else
            {
                movement.x = 0f;
            }

            movement.Normalize();
            transform.forward = movement;
        }

        movement += Physics.gravity;
        _controller.Move(_speed * UnityEngine.Time.deltaTime * movement);
    }
}