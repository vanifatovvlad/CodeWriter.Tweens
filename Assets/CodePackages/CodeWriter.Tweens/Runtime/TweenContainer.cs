using System;
using System.Collections.Generic;

namespace CodeWriter.Tweens
{
    public abstract class TweenContainer : TweenBase
    {
        protected List<ITweenControl> m_tweens = new List<ITweenControl>();

        public void Add(TweenBase tween)
        {
            CheckModifyRunningTween();

            if (tween == null)
                throw new ArgumentNullException("tween");

            m_tweens.Add(tween);
        }
    }
}