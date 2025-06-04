using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _boostMult;
    [SerializeField] private float _boostDur;
    private PlayerAnimation _playerAnimation;


    private Rigidbody2D _rigidbody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    private bool _isBoosting = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !_isBoosting && _movementInput != Vector2.zero)
        {
            StartCoroutine(SpeedBoost());
        }
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
            0.1f
            );
        _rigidbody.linearVelocity = _movementInput * _speed;
        if (_playerAnimation != null)
        {
            _playerAnimation.SetDirection(_smoothedMovementInput, _isBoosting);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>();
    }

    private IEnumerator SpeedBoost()
    {
        _isBoosting = true;
        float originalSpeed = _speed;
        _speed *= _boostMult;

        yield return new WaitForSeconds(_boostDur);

        _speed = originalSpeed;
        _isBoosting = false;
    }

}
