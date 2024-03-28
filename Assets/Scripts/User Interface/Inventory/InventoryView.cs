using System;
using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
    public class InventoryView : Behaviour, ISlotSelector
    {
        [SerializeField] private SlotView[] _slotViews;
        [SerializeField] private Button _removeButton;

        [SerializeField] private GameObject _panelGameObject;

        [SerializeField] private SpriteDatabase _spriteDatabase;

        private SlotView _selectedSlotView;

        private IInteractable _interactable;
        private IReadableInventory _inventory;
        
        public void Construct(IReadableInventory inventory, IInteractable interactable)
        {
            inventory.Placed += OnPlaced;
            inventory.Removed += OnRemoved;
            
            _inventory = inventory;
            _interactable = interactable;
        }
        
        protected override void Awake()
        {
            base.Awake();

            foreach (var slotView in _slotViews)
            {
                slotView.Construct(this);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _removeButton.onClick.AddListener(RemoveItem);
        }

        protected override void Start()
        {
            base.Start();
            
            _removeButton.gameObject.Deactivate();
            _panelGameObject.Deactivate();
        }

        private void RemoveItem()
        {
            if (_interactable.RemoveItem(_selectedSlotView.Name))
            {
                Select(_selectedSlotView);
            }
        }

        private void OnPlaced(ISlot slot)
        {
            var slotView = _slotViews[slot.Index];

            if (!slotView.IsAssigned)
            {
                if (!_spriteDatabase.TryGetValue(slot.Name, out var sprite))
                {
                    throw new ArgumentNullException("Отсутствует спрайт");
                }
                
                slotView.Assign(slot.Name, sprite);
            }

            slotView.UpdateCount(slot.Count);
        }
        
        private void OnRemoved(ISlot slot)
        {
            var slotView = _slotViews[slot.Index];
            slotView.Clear();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _removeButton.onClick.RemoveListener(RemoveItem);
        }
        
        public void Select(SlotView slotView)
        {
            if (_selectedSlotView != null)
            {
                if (_selectedSlotView == slotView)
                {
                    _selectedSlotView = null;
                    
                    _removeButton.gameObject.Deactivate();

                    return;
                }
            }

            _selectedSlotView = slotView;
            
            _removeButton.gameObject.Activate();
        }

        public void ToggleActive()
        {
            if (_panelGameObject.activeSelf)
            {
                _removeButton.gameObject.Deactivate();
                _selectedSlotView = null;
                
                _panelGameObject.Deactivate();
            }
            else
            {
                _panelGameObject.Activate();
            }
        }

        public void Clear()
        {
            _inventory.Placed -= OnPlaced;
            _inventory.Removed -= OnRemoved;
        }
    }
}