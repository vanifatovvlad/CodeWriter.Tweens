using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Transform/Scale FromTo")]
    public class ScaleAnimationAsset : TweenAsset
    {
        public string m_RectTransform = DEFAULT_INJECT;

        public float m_duration = 0.2f;
        public EasingType m_easing = EasingType.Linear;
        public Vector3 m_from = new Vector3(0.5f, 0.5f, 1f);
        public Vector3 m_to = new Vector3(1f, 1f, 1f);

        public override string Title
        {
            get { return "Scale FromTo"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            list.Add(new TweenAssetInjection(m_RectTransform, typeof(RectTransform)));
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var rect = (RectTransform)injector.GetInjection<RectTransform>(m_RectTransform);
            return Tween.scaleFromTo(rect, m_from, m_to, m_duration, m_easing.GetEase());
        }
    }
}