namespace CodeWriter.Tweens
{
    public static class EasingsUtils
    {
        public static float Lerp(float from, float to, float t, TweenEase easing)
        {
            return easing(t, from, to - from, 1f);
        }
        
        public static float Lerp(float from, float to, float t, float duration, TweenEase easing)
        {
            return easing(t, from, to - from, duration);
        }

        public static float Lerp(float from, float to, float t, float duration, EasingType easing)
        {
            return easing.GetEase().Invoke(t, from, to - from, duration);
        }
    }
}