using System;
using Cysharp.Threading.Tasks;
using HandContent;
using MakeupContent.SpongeContent;
using Tools;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace MakeupContent
{
    public abstract class MakeupHandler : MonoBehaviour
    {
        [SerializeField] private MakeUpTool _applyTool;
        [SerializeField] protected GameObject[] Targets;
        [SerializeField] private SpongeHandler _spongeHandler;
        [SerializeField] private DraggableHandler _draggableHandler;
        [SerializeField] protected HandController HandController;
        [SerializeField] private MakeUpButton[] _makeUpButtons;
        [SerializeField] private Transform _waitingPosition;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private bool _targetDefaultValue;

        private bool _isWorking;

        public MakeUpTool ApplyTool => _applyTool;

        private void Awake()
        {
            if (_makeUpButtons.Length > 0)
                Init(_makeUpButtons);
        }

        private void OnEnable()
        {
            _spongeHandler.Cleaning += Cleaning;
        }

        private void OnDisable()
        {
            _spongeHandler.Cleaning -= Cleaning;
        }

        public abstract UniTask ApplyMakeUpAsync();

        protected bool TryStartAction()
        {
            if (_isWorking || HandController.IsWorking) return false;
            _isWorking = true;
            return true;
        }

        private void EndAction() => _isWorking = false;

        protected async UniTask UseToolAsync(Transform toolPos, Transform toolTransform, MakeUpTool applyTool,
            Vector3? applyTargetPos = null, Image brushRenderer = null, Color? brushColor = null)
        {
            try
            {
                await HandController.MoveHandTo(toolPos.position,
                    () => { HandController.PickUpObject(toolTransform, applyTool, _offset); });

                if (applyTargetPos.HasValue)
                {
                    await HandController.MoveHandTo(applyTargetPos.Value - new Vector3(0, 65f, 0));
                    await HandController.PlayApplyAnimation(applyTargetPos.Value - new Vector3(0, 100f, 0));

                    if (brushRenderer != null && brushColor.HasValue)
                        brushRenderer.color = brushColor.Value;
                }

                await HandController.MoveHandTo(_waitingPosition.position);
                _draggableHandler.SetValue(true);
            }
            finally
            {
                EndAction();
            }
        }

        protected async UniTask ApplyToolEffect(Transform faceTarget, GameObject appliedObject, Transform defaultParent,
            bool enableObject = true, Action onFinish = null)
        {
            _draggableHandler.SetValue(false);

            await HandController.PlayApplyAnimation(faceTarget.position - new Vector3(0, 100f, 0));

            Cleaning();
            appliedObject.SetActive(enableObject);

            await ReturnTool(defaultParent, defaultParent,
                (() => onFinish?.Invoke()));
        }

        private async UniTask ReturnTool(Transform defaultPos, Transform toolParent, Action onAction = null)
        {
            await HandController.MoveHandTo(defaultPos.position, () =>
            {
                HandController.DropItem(toolParent);
                onAction?.Invoke();
            });
            await HandController.ReturnHand(() => { EndAction(); });
        }

        private void Init(MakeUpButton[] buttons)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].SetIndex(i);
        }

        private void Cleaning()
        {
            foreach (var target in Targets)
                target.SetActive(_targetDefaultValue);
        }
    }
}