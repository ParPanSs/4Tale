using UnityEngine;
using UnityEngine.EventSystems;

namespace _4Tale
{
    public class CardDragNDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEndDragHandler, IDragHandler
    {
        private RectTransform _rectTransform;
        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private Vector2 _originalLocalPointerPosition;
        private Vector3 _originalScale;
        private int _originalSiblingIndex;
        private float _selectedScale = 1.5f;
        
        public void CachePosition()
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.localPosition;
            _startRotation = _rectTransform.localRotation;
            _originalScale = _rectTransform.localScale;
            _originalSiblingIndex = _rectTransform.GetSiblingIndex();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.position = new Vector3(_rectTransform.position.x, _rectTransform.position.y + 90f,
                _rectTransform.position.z);
            _rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            _rectTransform.SetAsLastSibling();
            _rectTransform.localScale *= _selectedScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RestoreCardParameters();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag");
            RestoreCardParameters();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.localScale = _originalScale;
            _rectTransform.position = eventData.position;
        }

        private void RestoreCardParameters()
        {
            _rectTransform.localPosition = _startPosition;
            _rectTransform.localRotation = _startRotation;
            _rectTransform.localScale = _originalScale;
            _rectTransform.SetSiblingIndex(_originalSiblingIndex);
        }
    }
}
