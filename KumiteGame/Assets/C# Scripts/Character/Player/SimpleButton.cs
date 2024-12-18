using UnityEngine;
using UnityEngine.EventSystems;

namespace CnControls
{
    public class SimpleButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        public string ButtonName = "Jump";
        private VirtualButton _virtualButton;
        private void OnEnable()
        {
            _virtualButton = _virtualButton ?? new VirtualButton(ButtonName);
            CnInputManager.RegisterVirtualButton(_virtualButton);
        }
        private void OnDisable()
        {
            CnInputManager.UnregisterVirtualButton(_virtualButton);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            _virtualButton.Release();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            _virtualButton.Press();
        }
    }
}