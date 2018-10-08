using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Animator/Set Trigger")]
    public class SetTriggerAnimationAsset : TweenAsset
    {
        public string m_Animator = DEFAULT_INJECT;

        public string m_triggerName;

        public override string Title
        {
            get { return "SetTrigger"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            list.Add(new TweenAssetInjection(m_Animator, typeof(Animator)));
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var animator = (Animator)injector.GetInjection<Animator>(m_Animator);
            return Tween.invoke(() => animator.SetTrigger(m_triggerName));
        }
    }
}