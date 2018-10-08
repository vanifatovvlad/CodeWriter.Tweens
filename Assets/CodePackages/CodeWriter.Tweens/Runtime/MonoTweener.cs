using UnityEngine;

namespace CodeWriter.Tweens
{
    public sealed class MonoTweener : MonoBehaviour
    {
        private Tweener m_tweener;

        void Awake()
        {
            m_tweener = new Tweener();
        }

        void OnDestroy()
        {
            m_tweener.Dispose();
        }

        public void StartTween(TweenBase tween, TweenStartMode startMode = TweenStartMode.Immediate)
        {
            if (m_tweener == null)
                m_tweener = new Tweener();

            m_tweener.StartTween(tween, startMode);
        }

        public void CompleteTweens()
        {
            if (m_tweener == null)
                return;

            m_tweener.CompleteTweens();
        }

        public void CancelTweens()
        {
            if (m_tweener == null)
                return;

            m_tweener.CancelTweens();
        }
    }
}