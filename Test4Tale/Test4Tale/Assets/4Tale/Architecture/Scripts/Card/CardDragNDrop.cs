using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _4Tale
{
    public class CardDragNDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GameObject playArrow;
        private RectTransform _rectTransform;
        private Vector2 _originalLocalPointerPosition;
        private Vector3 _startPosition;
        private Vector3 _originalScale;
        private Quaternion _startRotation;
        private CardView _cardView;
        private CardPlayEvent _cardPlayEvent = new();
        private DeckController _deckController;
        private PlayerCharacteristics _playerCharacteristics;
        private int _originalSiblingIndex;
        private float _selectedScale = 1.1f;
        
        public void Construct(DeckController deckController)
        {
            _rectTransform = GetComponent<RectTransform>();
            _startPosition = _rectTransform.localPosition;
            _startRotation = _rectTransform.localRotation;
            _originalScale = _rectTransform.localScale;
            _originalSiblingIndex = _rectTransform.GetSiblingIndex();
            _cardView = GetComponent<CardView>();
            _deckController = deckController;
            _cardPlayEvent.OnCardPlayed += _deckController.FoldCardEvent;
            _playerCharacteristics = FindObjectOfType<PlayerCharacteristics>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.position = new Vector3(_rectTransform.position.x, _rectTransform.position.y + 150f,
                _rectTransform.position.z);
            _rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            _rectTransform.SetAsLastSibling();
            _rectTransform.localScale *= _selectedScale;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RestoreCardParameters();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag");
            if (_cardView.CardType == CardType.NonTarget)
            {
                switch (_cardView.CardState)
                {
                    case CardState.Defense:
                        Debug.Log("Def");
                        _playerCharacteristics.ArmorHP += _cardView.CardValue;
                        break;
                    case CardState.Heal:
                        Debug.Log("Heal");
                        _playerCharacteristics.HP += _cardView.CardValue;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                _cardPlayEvent.InvokeEvent(_cardView.GetSelectedCard());
                Destroy(gameObject);
            }
            playArrow.SetActive(false);
            /*if (...)
            {
                _cardPlayEvent.InvokeEvent(_cardView.GetSelectedCard());
            }*/
            RestoreCardParameters();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_cardView.CardType != CardType.Target) return;
            playArrow.SetActive(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_cardView.CardType == CardType.Target)
            {
                playArrow.SetActive(true);
                return;
            }
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

        private void OnDestroy()
        {
            if (_deckController != null) _cardPlayEvent.OnCardPlayed -= _deckController.FoldCardEvent;
        }
    }
}
