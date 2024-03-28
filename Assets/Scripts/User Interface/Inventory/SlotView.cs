using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Homework
{
    public class SlotView : Behaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _countText;

        private ISlotSelector _selector;
        
        public string Name { get; private set; }
        public bool IsAssigned { get; private set; }

        public void Construct(ISlotSelector selector)
        {
            _selector = selector;
        }

        public void Assign(string name, Sprite sprite)
        {
            Name = name;
            IsAssigned = true;
            
            _image.sprite = sprite;
            _image.preserveAspect = true;

            _button.interactable = true;
        }

        private void Select()
        {
            _selector.Select(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(Select);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _button.onClick.RemoveListener(Select);
        }

        public void UpdateCount(int count)
        {
            var value = count > 1 ? count.ToString() : string.Empty;
            _countText.text = value;
        }

        public void Clear()
        {
            Name = string.Empty;
            IsAssigned = false;

            _image.sprite = null;
            _countText.text = string.Empty;

            _button.interactable = false;
        }
    }
}