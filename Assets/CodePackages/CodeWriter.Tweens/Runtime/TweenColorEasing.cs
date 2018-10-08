using UnityEngine;

namespace CodeWriter.Tweens
{
    public delegate void ColorTweenUpdate(Color c);

    public class TweenColorEasing : TweenEasingBase<Color>
    {
        private ColorTweenUpdate m_externUpdate;

        public ColorTweenUpdate externUpdate
        {
            get { return m_externUpdate; }
            set
            {
                CheckModifyRunningTween();
                m_externUpdate = value;
            }
        }

        protected override void DoUpdate(float t)
        {
            var value = Color.LerpUnclamped(from, to, t);

            if (m_externUpdate != null)
                m_externUpdate(value);
        }
    }
}
