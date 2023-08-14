using System;
using UnityEngine;

public interface IInput
{
    float GetHorizontalInput();
    bool IsInput();
    Action OnTouch { get; set; }
    Action OnRelease { get; set; }
}

public class MouseInput : IInput
{
    private float _touchDelta;
    private float _touchStartPosition;
    private bool _isTouching = false;

    public Action OnRelease { get; set; }
    public Action OnTouch { get; set; }

    public bool IsInput()
    {
        return _isTouching;
    }

    public float GetHorizontalInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isTouching = true;
            _touchStartPosition = Input.mousePosition.x;
            OnTouch?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            if (_isTouching)
            {
                _touchDelta = Input.mousePosition.x - _touchStartPosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isTouching = false;
            _touchDelta = 0f;
            OnRelease?.Invoke();
        }

        return _touchDelta;
    }
}