using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("RectTransform/Move (Anchored)")]
    public class MoveAnchoredAnimationAsset : TweenAsset
    {
        public string m_RectTransform = DEFAULT_INJECT;

        public float m_duration = 0.2f;
        public EasingType m_easing = EasingType.Linear;
        public Vector2 m_from = Vector2.up * 50;
        public Vector2 m_to = Vector2.zero;
        public Mode m_mode = Mode.Relative;

        public enum Mode
        {
            Relative,
            Absolute,
        }

        public override string Title
        {
            get { return "Move (Anchored)"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            list.Add(new TweenAssetInjection(m_RectTransform, typeof(RectTransform)));
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var rect = (RectTransform)injector.GetInjection<RectTransform>(m_RectTransform);

            var from = m_from;
            var to = m_to;

            if (m_mode == Mode.Relative)
            {
                from += rect.anchoredPosition;
                to += rect.anchoredPosition;
            }

            return Tween.moveRectFromTo(rect, from, to, m_duration, m_easing.GetEase());
        }
    }
}