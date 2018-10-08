namespace CodeWriter.Tweens
{
    public interface ITweenControl
    {
        bool isRunning { get; }

        void Start();
        void Complete();
        void Cancel();
        void Step(float deltaTime);
    }
}