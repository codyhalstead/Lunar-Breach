using UnityEngine;
using UnityEngine.InputSystem;

public class MouseReticle : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private Vector2 _currentPos;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        Cursor.visible = false; // Hides system cursor
    }

    private void LateUpdate()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 anchoredPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            mousePos,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out anchoredPos
        );

        _currentPos = Vector2.Lerp(_currentPos, anchoredPos, 0.15f); // Adjust smoothing factor
        _rectTransform.anchoredPosition = _currentPos;
    }
}