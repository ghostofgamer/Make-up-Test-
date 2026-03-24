using System;
using Cysharp.Threading.Tasks;
using HandContent;
using SpongeContent;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace MakeupContent
{
    public abstract class MakeupHandler : MonoBehaviour
    {
        [SerializeField] private SpongeHandler _spongeHandler;

        [SerializeField] protected HandController HandController;

        protected bool IsWorking { get; private set; }

        private void OnEnable()
        {
            _spongeHandler.Cleaning += Cleaning;
        }

        private void OnDisable()
        {
            _spongeHandler.Cleaning -= Cleaning;
        }

        protected abstract void Cleaning();

        protected bool TryStartAction()
        {
            if (IsWorking || HandController.IsWorking) return false;
            IsWorking = true;
            return true;
        }

        protected void EndAction() => IsWorking = false;

        protected async UniTask MoveHandPickApply(
            Transform toolPos,
            Transform toolTransform,
            Tool tool,
            Vector3 offset,
            Transform waitPos,
            DraggableHandler draggable,
            Vector3? applyTargetPos = null, // если есть позиция применения
            Image brushRenderer = null,
            Color? brushColor = null)
        {
            try
            {
                // 1. Берём инструмент
                await HandController.MoveHandTo(toolPos.position,
                    () => { HandController.PickUpObject(toolTransform, tool, offset); });

                // 2. Если есть applyTargetPos → идём к нему и применяем
                if (applyTargetPos.HasValue)
                {
                    await HandController.MoveHandTo(applyTargetPos.Value - new Vector3(0, 65f, 0));
                    await HandController.PlayApplyAnimation(applyTargetPos.Value - new Vector3(0, 100f, 0));

                    if (brushRenderer != null && brushColor.HasValue)
                        brushRenderer.color = brushColor.Value;
                }

                // 3. Возврат в ожидание
                await HandController.MoveHandTo(waitPos.position);
                draggable.SetValue(true);
            }
            finally
            {
                EndAction();
            }
        }

        protected async UniTask ReturnTool(Transform defaultPos, Transform toolParent, Action onAction = null)
        {
            await HandController.MoveHandTo(defaultPos.position, () =>
            {
                HandController.DropItem(toolParent); 
                onAction?.Invoke();
            });
            await HandController.ReturnHand(() => { EndAction(); });
        }
    }
}