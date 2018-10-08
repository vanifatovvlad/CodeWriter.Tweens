using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CodeWriter.Tweens.Assets
{
    [TweenMenuItem("Parallel")]
    public class ParallelAnimationAsset : TweenAsset
    {
        public TweenAsset[] m_parallel = new TweenAsset[0];

        public TweenAsset[] parallel
        {
            get { return m_parallel; }
        }

        public override string Title
        {
            get { return "Parallel"; }
        }

        public override void RegisterInjections(List<TweenAssetInjection> list)
        {
            foreach (var asset in m_parallel)
                asset.RegisterInjections(list);
        }

        public override TweenBase CreateTween(ITweenAssetInjector injector)
        {
            var tween = new TweenParallel();
            foreach (var item in m_parallel)
            {
                tween.Add(item.CreateTween(injector));
            }
            return tween;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ParallelAnimationAsset))]
    public class ParallelAnimationAssetEditor : SequenceAnimationAssetEditorBase
    {
        protected override SerializedProperty Sequence
        {
            get { return serializedObject.FindProperty("m_parallel"); }
        }
    }
#endif
}