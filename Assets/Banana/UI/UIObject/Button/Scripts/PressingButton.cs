using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace Banana.UI.UIObject.Buttons
{
    public class PressingButton : ButtonAniBase
    {
        [SerializeField] private int pressingDown = 20;
        [SerializeField] private Sprite buttonSprite;
        [SerializeField] private Color shadowColor = new Color(0, 0, 0, 0.5f);
        [SerializeField] private bool isSmooth = true;

        public int PressingDown
        {
            get { return pressingDown; }
            set
            {
                if (pressingDown != value)
                {
                    pressingDown = value;
                    AcceptImages();
                }
            }
        }

        public Sprite ButtonSprite
        {
            get { return buttonSprite; }
            set
            {
                if (buttonSprite != value)
                {
                    buttonSprite = value;
                    AcceptImages();
                }
            }
        }

        public Color ShadowColor
        {
            get { return shadowColor; }
            set
            {
                if (shadowColor != value)
                {
                    shadowColor = value;
                    AcceptImages();
                }
            }
        }

        public RectTransform MainTrs { get { Init(); return _mainTrs; } }
        public RectTransform ShadowTrs { get { Init(); return _shadowTrs; } }
        public Image MainImage { get { Init(); return _mainImage; } }
        public Image ShadowImage { get { Init(); return _shadowImage; } }
        public bool IsInit { get; set; }

        private RectTransform _mainTrs;
        private RectTransform _shadowTrs;
        private Image _mainImage;
        private Image _shadowImage;

        

        private void Init()
        {
            if (IsInit)
                return;

            IsInit = true;
            _mainTrs = transform.Find("MainImage").GetComponent<RectTransform>();
            _shadowTrs = transform.Find("ShadowImage").GetComponent<RectTransform>();

            _mainImage = _mainTrs.GetComponent<Image>();
            _shadowImage = _shadowTrs.GetComponent<Image>();

            _shadowImage.color = ShadowColor;
        }

        public void AcceptImages()
        {
            var isSliced = false;
            if (buttonSprite != null)
            {
                var border = buttonSprite.border;
                isSliced = border.x > 0 || border.y > 0 || border.z > 0 || border.w > 0;
            }

            MainImage.sprite = buttonSprite;
            ShadowImage.sprite = buttonSprite;

            if (isSliced)
            {
                ShadowTrs.sizeDelta = MainTrs.sizeDelta;
                SetAllDirty();
            }
            else
            {
                MainImage.SetNativeSize();
                ShadowImage.SetNativeSize();
            }

            ShadowImage.color = shadowColor;

            ShadowTrs.localPosition = new Vector2(MainTrs.localPosition.x, MainTrs.localPosition.y - PressingDown);
        }

        protected override void PlayButtonAni(bool isDown)
        {
            base.PlayButtonAni(isDown);
            MainTrs.DOKill();

            if (isDown)
                MainTrs.DOLocalMoveY(ShadowTrs.localPosition.y, isSmooth ? 0.1f : 0f);
            else
                MainTrs.DOLocalMoveY(0f, isSmooth ? 0.1f : 0f);
        }
    }
}