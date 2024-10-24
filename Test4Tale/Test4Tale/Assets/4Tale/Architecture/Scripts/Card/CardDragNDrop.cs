using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _4Tale
{
    public class CardDragNDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private GameObject playArrow;
        private HandVisual _handVisual;
        private RectTransform _rectTransform;
        [SerializeField]private Vector3 _startPosition;
        [SerializeField]private Vector3 _originalScale;
        [SerializeField]private Quaternion _startRotation;
        private CardView _cardView;
        private CardPlayEvent _cardPlayEvent = new();
        private DeckController _deckController;
        private PlayerCharacteristics _playerCharacteristics;
        private int _originalSiblingIndex;
        private float _selectedScale = 1.1f;
        private bool _isCardClicked;
        
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
            _handVisual = FindObjectOfType<HandVisual>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _rectTransform.position = new Vector3(_rectTransform.position.x, _rectTransform.position.y + 150f,
                _rectTransform.position.z);
            _rectTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            _rectTransform.localScale *= _selectedScale;
            _rectTransform.SetAsLastSibling();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            RestoreCardParameters();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_playerCharacteristics.Energy < _cardView.GetSelectedCard().cardCost) return;
            if (_cardView.CardType == CardType.NonTarget)
            {
                switch (_cardView.CardState)
                {
                    case CardState.Defense:
                        _playerCharacteristics.ArmorHP += _cardView.CardValue;
                        break;
                    case CardState.Heal:
                        _playerCharacteristics.HP += _cardView.CardValue;
                        break;
                    default:
                        throw new NotImplementedException();
                }
                _cardPlayEvent.InvokeEvent(_cardView.GetSelectedCard());
                _playerCharacteristics.SetEnergy(_playerCharacteristics.Energy - _cardView.GetSelectedCard().cardCost);
                _handVisual.UpdateHandVisuals();
                _handVisual.GetDragNDrop().Remove(this);
                _handVisual.GetCardsList().Remove(gameObject);
                Destroy(gameObject);
            }

            if (_cardView.CardType == CardType.Target)
            {
                switch (_cardView.CardState)
                {
                    case CardState.Attack:
                        RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                        if (ray.collider)
                        {
                            if (ray.collider.TryGetComponent(out EnemyCharacteristics enemyCharacteristics))
                            {
                                enemyCharacteristics.TakeDamage(_cardView.CardValue);
                                _cardPlayEvent.InvokeEvent(_cardView.GetSelectedCard());
                                _playerCharacteristics.SetEnergy(_playerCharacteristics.Energy - _cardView.GetSelectedCard().cardCost);
                                _handVisual.UpdateHandVisuals();
                                _handVisual.GetDragNDrop().Remove(this);
                                _handVisual.GetCardsList().Remove(gameObject);
                                Destroy(gameObject);
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            playArrow.SetActive(false);
            RestoreCardParameters();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_cardView.CardType != CardType.Target || _playerCharacteristics.Energy < _cardView.GetSelectedCard().cardCost) return;
            _isCardClicked = !_isCardClicked;
            playArrow.SetActive(_isCardClicked);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_playerCharacteristics.Energy < _cardView.GetSelectedCard().cardCost) return;
            if (_cardView.CardType == CardType.Target)
            {
                playArrow.SetActive(true);
                return;
            }
            _rectTransform.localScale = _originalScale;
            _rectTransform.position = eventData.position;
        }

        public void FoldCard()
        {
            _cardPlayEvent.InvokeEvent(_cardView.GetSelectedCard());
            Destroy(gameObject);
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
