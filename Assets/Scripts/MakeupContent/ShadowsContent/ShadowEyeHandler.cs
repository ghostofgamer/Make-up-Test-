using System.Collections;
using HandContent;
using MakeupContent;
using Tools;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

public class ShadowEyeHandler : MakeupHandler
{
    [SerializeField] private GameObject[] _shadows;
    [SerializeField] private ShadowEyeButton[] _shadowButtons;
    [SerializeField] private ShadowTool _shadowTool;
    [SerializeField] private HandController _handController;
    [SerializeField] private Transform _brushDefaultPosition;
    [SerializeField] private Image _brushTipRenderer;
    [SerializeField] private Transform _brushTransform;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _shadowFacePos;
    [SerializeField] private Transform _posWiting;
    [SerializeField] private DraggableHandler _draggableHandler;
    
    private bool _isWorking = false;
    private Color _currentColor;
    private Color _defaultColor = Color.white;

    private void Awake()
    {
        Init();
    }

    public void OnColorSelected(Color color, Vector3 colorButtonPosition, int index)
    {
        if (_isWorking || _handController.IsWorking)
            return;

        _isWorking = true;
        _currentColor = color;
        _shadowTool.SetIndex(index);
        StartCoroutine(HandSequence(colorButtonPosition));
    }

    public void ApplyShadow(int index)
    {
        _draggableHandler.SetValue(false);
        Debug.Log("ApplyShadow" + index);

        _handController.PlayApplyAnimation(_shadowFacePos.position - new Vector3(0, 100f, 0),
            () =>
            {
                Cleaning();

                _shadows[index].SetActive(true);
                StartCoroutine(ReturnDefault());
            });
    }

    private void Init()
    {
        for (int i = 0; i < _shadowButtons.Length; i++)
            _shadowButtons[i].SetIndex(i);
    }

    private IEnumerator HandSequence(Vector3 colorButtonPosition)
    {
        bool applyDone = false;

        yield return _handController.MoveHandTo(_brushDefaultPosition.position,
            () => { _handController.PickUpObject(_brushTransform, _shadowTool, _offset); });

        yield return _handController.MoveHandTo(colorButtonPosition,
            () =>
            {
                _handController.PlayApplyAnimation(colorButtonPosition - new Vector3(0, 100f, 0),
                    () =>
                    {
                        _brushTipRenderer.color = _currentColor;
                        applyDone = true;
                    });
            });
        
        yield return new WaitUntil(() => applyDone);
        yield return _handController.MoveHandTo(_posWiting.position);
        _draggableHandler.SetValue(true);
    }

    private IEnumerator ReturnDefault()
    {
        yield return _handController.MoveHandTo(_brushDefaultPosition.position, () =>
        {
            _brushTipRenderer.color = _defaultColor;
            _handController.DropItem(_brushDefaultPosition);
        });

        yield return _handController.ReturnHand(() => { _isWorking = false; });
    }

    protected override void Cleaning()
    {
        foreach (var shadow in _shadows)
            shadow.SetActive(false);
    }
}