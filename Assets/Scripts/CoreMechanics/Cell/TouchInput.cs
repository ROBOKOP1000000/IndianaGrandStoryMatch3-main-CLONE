using UnityEngine;
using UnityEngine.Events;

public class TouchInput : MonoBehaviour
{
    private readonly float HALF = 0.5f;

    public event UnityAction SwipeLeft;

    public event UnityAction SwipeRight;

    public event UnityAction SwipeUp;

    public event UnityAction SwipeDown;

    private Vector2 _beganTouchPosition;
    private Vector2 _endedTouchPosition;

    private void Update()
    {
        if (Input.touchCount <= 0 || GameManager.instance.isGameOver)
            return;

        var touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
            _beganTouchPosition = touch.position;

        if (touch.phase == TouchPhase.Ended)
        {
            _endedTouchPosition = new Vector3(touch.position.x - _beganTouchPosition.x, touch.position.y - _beganTouchPosition.y);

            _endedTouchPosition.Normalize();

            DirectSwipe();
        }
    }

    private void DirectSwipe()
    {
        if (_endedTouchPosition.y > 0 && _endedTouchPosition.x > -HALF && _endedTouchPosition.x < HALF)
        {
            SwipeUp.Invoke();
        }
        if (_endedTouchPosition.y < 0 && _endedTouchPosition.x > -HALF && _endedTouchPosition.x < HALF)
        {
            SwipeDown.Invoke();
        }
        if (_endedTouchPosition.x < 0 && _endedTouchPosition.y > -HALF && _endedTouchPosition.y < HALF)
        {
            SwipeLeft.Invoke();
        }
        if (_endedTouchPosition.x > 0 && _endedTouchPosition.y > -HALF && _endedTouchPosition.y < HALF)
        {
            SwipeRight.Invoke();
        }
    }
}