using System;

namespace CodeWriter.Tweens
{
    public class TweenInvoke : TweenBase
    {
        private readonly Action m_action;

        public TweenInvoke(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            m_action = action;
        }

        protected override void DoStart()
        {

        }

        protected override void DoForceComplete()
        {
            m_action();
        }

        protected override void DoCancel()
        {

        }

        protected override bool DoStep(float deltaTime)
        {
            m_action();
            return true;
        }
    }
}