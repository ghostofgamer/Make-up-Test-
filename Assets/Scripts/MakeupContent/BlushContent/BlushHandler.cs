using System.Collections;
using MakeupContent;
using Tools;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace BlushContent
{
    public class BlushHandler : MakeupHandler
    {
        [SerializeField] private GameObject[] _blushes;
        [SerializeField] private BlushColorButton[] _blushButtons;
        [SerializeField] private Transform _brushTransform;
        [SerializeField] private Image _brushTipRenderer;
        [SerializeField] private HandController _handController;
        [SerializeField] private Transform brushDefaultPosition;
        [SerializeField] private BlushTool _tool;
        [SerializeField] private Vector3 _offset;

        private Color _currentColor;
        private Color _defaultColor = Color.white;
        private bool _isWorking = false;

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
            _tool.SetIndex(index);
            StartCoroutine(HandSequence(colorButtonPosition));
        }

        public void ApplyBlush(int index)
        {
            Debug.Log("ApplyBlush" + index);

            Cleaning();

            _blushes[index].SetActive(true);
            StartCoroutine(ReturnDefault());
        }

        protected override void Cleaning()
        {
            foreach (var blush in _blushes)
                blush.SetActive(false);
        }

        private void Init()
        {
            for (int i = 0; i < _blushButtons.Length; i++)
                _blushButtons[i].SetIndex(i);
        }

        private IEnumerator HandSequence(Vector3 colorButtonPosition)
        {
            yield return _handController.MoveHandTo(brushDefaultPosition.position,
                () => { _handController.PickUpObject(_brushTransform, _tool, _offset); });

            yield return _handController.MoveHandTo(colorButtonPosition,
                () =>
                {
                    _handController.PlayApplyAnimation(colorButtonPosition - new Vector3(0, -50f, 0),
                        () => { _brushTipRenderer.color = _currentColor; });
                });
        }

        private IEnumerator ReturnDefault()
        {
            yield return _handController.MoveHandTo(brushDefaultPosition.position, () =>
            {
                _brushTipRenderer.color = _defaultColor;
                _handController.DropItem(brushDefaultPosition);
            });

            yield return _handController.ReturnHand(() => { _isWorking = false; });
        }
    }
}