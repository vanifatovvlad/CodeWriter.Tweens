namespace CodeWriter.Tweens
{
    public class TweenEasing : TweenEasingBase<float>
    {
        private TweenUpdate m_externUpdate;

        public TweenUpdate externUpdate
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
            var value = from + (to - from) * t;

            if (m_externUpdate != null)
                m_externUpdate(value);
        }
    }
}