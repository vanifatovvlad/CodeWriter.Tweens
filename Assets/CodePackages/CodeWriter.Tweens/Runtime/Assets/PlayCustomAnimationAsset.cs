using System.Collections.Generic;

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Play Custom")]
    public class PlayCustomAnimationAsset : TweenAsset
    {
        public TweenAsset m_asset;

        public override string Title
        {
            get { return "Play"; }
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            return m_asset.CreateTween(injector);
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            m_asset.RegisterInjections(list);
        }
    }
}