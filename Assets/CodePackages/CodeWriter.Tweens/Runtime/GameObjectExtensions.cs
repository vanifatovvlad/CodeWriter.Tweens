using UnityEngine;

namespace CodeWriter.Tweens
{
    public static class GameObjectExtensions
    {
        public static void StartTween(this GameObject go, TweenBase tween, TweenStartMode startMode = TweenStartMode.Immediate)
        {
            go.GetTweener().StartTween(tween, startMode);
        }

        public static void StartTween(this GameObject go, ref TweenBase variable, TweenBase tween, TweenStartMode startMode = TweenStartMode.Immediate)
        {
            if (variable != null)
                variable.Cancel();

            variable = tween;

            go.GetTweener().StartTween(tween, startMode);
        }

        public static void CompleteTweens(this GameObject go)
        {
            go.GetTweener().CompleteTweens();
        }

        public static void CancelTweens(this GameObject go)
        {
            go.GetTweener().CancelTweens();
        }

        private static MonoTweener GetTweener(this GameObject go)
        {
            return go.GetComponent<MonoTweener>() ?? go.AddComponent<MonoTweener>();
        }
    }
}