using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private PathCreator _pathCreator;
    private IInput _input;

    [SerializeField] private Transform _cameraParent;
    [SerializeField] private float _camRotationModifire = 3f;
    [SerializeField] private float _curveFactorModifire = 3f;
    [SerializeField] private float _inputModifire = 0.004f;
    [SerializeField] private float _inputAmplitude = 1.5f;

    [SerializeField, Header("Fire Settings")] private Transform _bulletTransform;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireAnimDuration = 2f;
    [SerializeField] private Ease _fireEase;

    private bool _enable = false;
    private bool _canInput = false;
    private float _inputValue = 0f;

    private void Awake()
    {
        Instance = this;
        _pathCreator = GetComponent<PathCreator>();
        _input = new MouseInput();
        _input.OnTouch += SubscribeOnTouch;
    }

    private void Update()
    {
        if (!_enable) return;
        if (!_canInput) return;

        Input();
        RotateCamera();
        CreateTrajectoryLine();
    }

    public async void Fire()
    {
        _input.OnRelease -= Fire;

        DisableInput();

        var pathSequence = DOTween.Sequence();
        Quaternion endRot = _pathCreator.GetEndAngle();
        _pathCreator.ResetEndAngle();

        pathSequence.Join(_bulletTransform.DOPath(_pathCreator.GetPositions().ToArray(), _fireAnimDuration).SetEase(_fireEase))
            .Join(_bulletTransform.DOLocalRotate(Vector3.up * (360 * 10), _fireAnimDuration, RotateMode.FastBeyond360))
            .OnComplete(() => _bulletTransform.rotation = endRot);

        await pathSequence.Play().AsyncWaitForCompletion();

        EnableInput();
    }

    private void Input()
    {
        _inputValue = _input.GetHorizontalInput() * _inputModifire;
        _inputValue = Mathf.Clamp(_inputValue, -_inputAmplitude, _inputAmplitude);
    }

    private void CreateTrajectoryLine()
    {
        if (_input.IsInput())
        {
            _pathCreator.EnableLine(); 
            _bulletTransform.position = _firePoint.position;
            _bulletTransform.rotation = Quaternion.identity;
        }
            
        else 
            _pathCreator.DisableLine();

        _pathCreator.UpdateLine(_inputValue * _curveFactorModifire);
    }

    private void RotateCamera()
    {
        Quaternion oldRot = _cameraParent.rotation;
        Quaternion newRot = Quaternion.AngleAxis(_inputValue * _camRotationModifire, Vector3.up);
        _cameraParent.rotation = Quaternion.RotateTowards(oldRot, newRot, 1f + (_inputValue * _inputValue) * _camRotationModifire * Time.deltaTime);
    }

    private void SubscribeOnTouch()
    {
        _input.OnRelease += Fire;
    }

    public void EnableInput()
    {
        _canInput = true;
    }

    public void DisableInput()
    {
        _canInput = false;
    }

    public void Enable()
    {
        _enable = true;
    }

    public void Disable()
    {
        _enable = false;
    }
}
