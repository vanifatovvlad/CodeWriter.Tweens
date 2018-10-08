namespace CodeWriter.Tweens
{
    public enum EasingType
    {
        Linear,

        ExpoEaseOut,
        ExpoEaseIn,
        ExpoEaseInOut,
        ExpoEaseOutIn,

        CircEaseOut,
        CircEaseIn,
        CircEaseInOut,
        CircEaseOutIn,

        QuadEaseOut,
        QuadEaseIn,
        QuadEaseInOut,
        QuadEaseOutIn,

        SineEaseOut,
        SineEaseIn,
        SineEaseInOut,
        SineEaseOutIn,

        CubicEaseOut,
        CubicEaseIn,
        CubicEaseInOut,
        CubicEaseOutIn,

        QuartEaseOut,
        QuartEaseIn,
        QuartEaseInOut,
        QuartEaseOutIn,

        QuintEaseOut,
        QuintEaseIn,
        QuintEaseInOut,
        QuintEaseOutIn,

        ElasticEaseOut,
        ElasticEaseIn,
        ElasticEaseInOut,
        ElasticEaseOutIn,

        BounceEaseOut,
        BounceEaseIn,
        BounceEaseInOut,
        BounceEaseOutIn,

        BackEaseOut,
        BackEaseIn,
        BackEaseInOut,
        BackEaseOutIn,
    }

    public static class EasingTypeExtension
    {
        //[UnityEditor.MenuItem("Easings/Gen")]
        //static void Gen()
        //{
        //    var names = System.Enum.GetNames(typeof(EasingType));
        //    var sb = new System.Text.StringBuilder();
        //    foreach(var name in names)
        //    {
        //        sb.Append("case EasingType.").Append(name).AppendLine(":");
        //        sb.Append("return Easings.").Append(name).AppendLine(";");
        //        sb.AppendLine();
        //    }
        //    UnityEngine.Debug.Log(sb);
        //}

        public static TweenEase GetEase(this EasingType type)
        {
            switch (type)
            {
                case EasingType.Linear:
                    return Easings.Linear;

                case EasingType.ExpoEaseOut:
                    return Easings.ExpoEaseOut;

                case EasingType.ExpoEaseIn:
                    return Easings.ExpoEaseIn;

                case EasingType.ExpoEaseInOut:
                    return Easings.ExpoEaseInOut;

                case EasingType.ExpoEaseOutIn:
                    return Easings.ExpoEaseOutIn;

                case EasingType.CircEaseOut:
                    return Easings.CircEaseOut;

                case EasingType.CircEaseIn:
                    return Easings.CircEaseIn;

                case EasingType.CircEaseInOut:
                    return Easings.CircEaseInOut;

                case EasingType.CircEaseOutIn:
                    return Easings.CircEaseOutIn;

                case EasingType.QuadEaseOut:
                    return Easings.QuadEaseOut;

                case EasingType.QuadEaseIn:
                    return Easings.QuadEaseIn;

                case EasingType.QuadEaseInOut:
                    return Easings.QuadEaseInOut;

                case EasingType.QuadEaseOutIn:
                    return Easings.QuadEaseOutIn;

                case EasingType.SineEaseOut:
                    return Easings.SineEaseOut;

                case EasingType.SineEaseIn:
                    return Easings.SineEaseIn;

                case EasingType.SineEaseInOut:
                    return Easings.SineEaseInOut;

                case EasingType.SineEaseOutIn:
                    return Easings.SineEaseOutIn;

                case EasingType.CubicEaseOut:
                    return Easings.CubicEaseOut;

                case EasingType.CubicEaseIn:
                    return Easings.CubicEaseIn;

                case EasingType.CubicEaseInOut:
                    return Easings.CubicEaseInOut;

                case EasingType.CubicEaseOutIn:
                    return Easings.CubicEaseOutIn;

                case EasingType.QuartEaseOut:
                    return Easings.QuartEaseOut;

                case EasingType.QuartEaseIn:
                    return Easings.QuartEaseIn;

                case EasingType.QuartEaseInOut:
                    return Easings.QuartEaseInOut;

                case EasingType.QuartEaseOutIn:
                    return Easings.QuartEaseOutIn;

                case EasingType.QuintEaseOut:
                    return Easings.QuintEaseOut;

                case EasingType.QuintEaseIn:
                    return Easings.QuintEaseIn;

                case EasingType.QuintEaseInOut:
                    return Easings.QuintEaseInOut;

                case EasingType.QuintEaseOutIn:
                    return Easings.QuintEaseOutIn;

                case EasingType.ElasticEaseOut:
                    return Easings.ElasticEaseOut;

                case EasingType.ElasticEaseIn:
                    return Easings.ElasticEaseIn;

                case EasingType.ElasticEaseInOut:
                    return Easings.ElasticEaseInOut;

                case EasingType.ElasticEaseOutIn:
                    return Easings.ElasticEaseOutIn;

                case EasingType.BounceEaseOut:
                    return Easings.BounceEaseOut;

                case EasingType.BounceEaseIn:
                    return Easings.BounceEaseIn;

                case EasingType.BounceEaseInOut:
                    return Easings.BounceEaseInOut;

                case EasingType.BounceEaseOutIn:
                    return Easings.BounceEaseOutIn;

                case EasingType.BackEaseOut:
                    return Easings.BackEaseOut;

                case EasingType.BackEaseIn:
                    return Easings.BackEaseIn;

                case EasingType.BackEaseInOut:
                    return Easings.BackEaseInOut;

                case EasingType.BackEaseOutIn:
                    return Easings.BackEaseOutIn;

                default:
                    return Easings.Linear;
            }
        }
    }
}