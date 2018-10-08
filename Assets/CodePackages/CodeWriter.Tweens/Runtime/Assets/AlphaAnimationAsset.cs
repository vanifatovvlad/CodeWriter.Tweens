using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("CanvasGroup/Alpha FromTo")]
    public class AlphaAnimationAsset : TweenAsset
    {
        public string m_CanvasGroup = DEFAULT_INJECT;

        public float m_duration = 0.2f;
        public EasingType m_easing = EasingType.Linear;
        public float m_from = 0f;
        public float m_to = 1f;

        public override string Title
        {
            get { return "Alpha FromTo"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            list.Add(new TweenAssetInjection(m_CanvasGroup, typeof(CanvasGroup)));
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var group = (CanvasGroup)injector.GetInjection<CanvasGroup>(m_CanvasGroup);
            return Tween.fromTo(m_from, m_to, m_duration, m_easing.GetEase(), onUpdate: v => group.alpha = v);
        }
    }
}