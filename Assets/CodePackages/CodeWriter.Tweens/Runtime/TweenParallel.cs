namespace CodeWriter.Tweens
{
    public class TweenParallel : TweenContainer
    {
        protected override void DoStart()
        {
            for (int i = 0; i < m_tweens.Count; i++)
            {
                m_tweens[i].Start();
            }
        }

        protected override void DoForceComplete()
        {
            for (int i = 0; i < m_tweens.Count; i++)
            {
                m_tweens[i].Complete();
            }
        }

        protected override void DoCancel()
        {
            for (int i = 0; i < m_tweens.Count; i++)
            {
                m_tweens[i].Cancel();
            }
        }

        protected override bool DoStep(float deltaTime)
        {
            bool isAllCompleted = true;

            for (int i = 0; i < m_tweens.Count; i++)
            {
                var tween = m_tweens[i];
                if (tween.isRunning)
                {
                    tween.Step(deltaTime);
                    if (tween.isRunning)
                    {
                        isAllCompleted = false;
                    }
                }
            }

            return isAllCompleted;
        }
    }
}