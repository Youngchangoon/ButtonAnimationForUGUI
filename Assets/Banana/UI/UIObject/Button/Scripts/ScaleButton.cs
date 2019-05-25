using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Banana.UI.UIObject.Buttons
{
    public enum AnimationType
    {
        NonTween,
        Tween,
        Jelly,
    }
    
    public class ScaleButton : ButtonAniBase
    {
        [SerializeField] private float pressingScale = 0.8f;
        [SerializeField] private AnimationType animationType = AnimationType.NonTween;

        public RectTransform AnimationObject { get { Init(); return _animationObject; } }
        public float PressingScale
        {
            get { return pressingScale; }
            set
            {
                if (pressingScale != value)
                    pressingScale = value;
            }
        }

        private RectTransform _animationObject;
        private bool _isInit;

        private void Init()
        {
            if (_isInit)
                return;

            _isInit = true;
            _animationObject = _animationObject ?? transform.Find("AnimationObject") as RectTransform;
        }

        protected override void PlayButtonAni(bool isDown)
        {
            base.PlayButtonAni(isDown);

            AnimationObject.DOComplete();

            if (isDown)
            {
                switch (animationType)
                {
                    case AnimationType.NonTween:
                        AnimationObject.DOScale(pressingScale, 0.0f);
                        break;
                    case AnimationType.Tween:
                        AnimationObject.DOScale(pressingScale, 0.2f);
                        break;
                    case AnimationType.Jelly:
                        AnimationObject.DOScale(pressingScale, 0.2f);
                        break;
                }
            }
            else
            {
                switch (animationType)
                {
                    case AnimationType.NonTween:
                        AnimationObject.DOScale(1f, 0.0f);
                        break;
                    case AnimationType.Tween:
                        AnimationObject.DOScale(1f, 0.2f);
                        break;
                    case AnimationType.Jelly:
                        AnimationObject.localScale = Vector3.one;
                        AnimationObject.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.4f, 10, 5);
                        break;
                }
            }
        }
    }
}