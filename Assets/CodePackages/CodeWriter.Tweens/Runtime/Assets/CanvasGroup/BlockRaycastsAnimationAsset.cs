using System.Collections.Generic;
using UnityEngine;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("CanvasGroup/Set BlockRaycasts")]
    public class BlockRaycastsAnimationAsset : TweenAsset
    {
        public string m_CanvasGroup = DEFAULT_INJECT;

        public bool m_blockRaycasts = false;

        public override string Title
        {
            get { return "BlockRaycasts"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            list.Add(new TweenAssetInjection(m_CanvasGroup, typeof(CanvasGroup)));
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var group = (CanvasGroup)injector.GetInjection<CanvasGroup>(m_CanvasGroup);
            return Tween.invoke(() => group.blocksRaycasts = m_blockRaycasts);
        }
    }
}