using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickOld : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBackgound;
    [SerializeField] private Image _joystick;
    [SerializeField] private Image _joysctickArea;


    public event UnityAction<Vector3> DirectionChanged;
    public event UnityAction<Image> ClickChanged;
    public event UnityAction PositionChanged;

    private Vector3 _direction;
    private Vector2 _inputVector;
    private Vector2 _startBackgoundPosition;


    private void Start()
    {
        ClickChanged?.Invoke(_joystick);

        _startBackgoundPosition = _joystickBackgound.rectTransform.anchoredPosition;
    }

    private void Update()
    {
        GetDirection();
    }

    private void GetDirection()
    {
        var direction = new Vector2(SimpleInput.GetAxis("Horizontal"), SimpleInput.GetAxis("Vertical"));
        DirectionChanged?.Invoke(direction);
        /*
        if (_inputVector.x != 0 || _inputVector.y != 0)
        {
            _direction = new Vector3(_inputVector.x, 0, _inputVector.y);
            DirectionChanged?.Invoke(_direction);
            PositionChanged?.Invoke();
        }
        else
        {
            _direction = Vector3.zero; 
            DirectionChanged?.Invoke(Vector3.zero);
        }
    */
    }

    public Vector3 GetLastPosition()
    {
        return new Vector3(_inputVector.x, 0, _inputVector.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackgound.rectTransform,
            eventData.position, null, out joystickPosition))
        {
            PositionChanged?.Invoke();
            
            joystickPosition.x = (joystickPosition.x * 2 / _joystickBackgound.rectTransform.sizeDelta.x);
            joystickPosition.y = (joystickPosition.y * 2 / _joystickBackgound.rectTransform.sizeDelta.y);

            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);

            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _joystick.rectTransform.anchoredPosition = new Vector2(
                _inputVector.x * (_joystickBackgound.rectTransform.sizeDelta.x / 2),
                _inputVector.y * (_joystickBackgound.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickChanged?.Invoke(_joystick);


        Vector2 joystickBackgroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joysctickArea.rectTransform, eventData.position,
            null, out joystickBackgroundPosition))
            _joystickBackgound.rectTransform.anchoredPosition =
                new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackgound.rectTransform.anchoredPosition = _startBackgoundPosition;

        ClickChanged?.Invoke(_joystick);

        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }
}