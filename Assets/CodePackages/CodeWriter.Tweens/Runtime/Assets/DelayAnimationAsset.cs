using System.Collections.Generic;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Delay")]
    public class DelayAnimationAsset : TweenAsset
    {
        public float m_delay = 0.2f;

        public override string Title
        {
            get { return "Delay"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        { }

        public override TweenBase CreateTween(ITweenAssetInjector injectorcanvasGroup)
        {
            return Tween.delay(m_delay);
        }
    }
}