namespace InputReader
{
    public interface IEntityInputSource
    {
        float HorizontalDirection { get; }
        float VerticalDirection { get; }
        bool Jump { get; }
        bool Pause { get; }

        void ResetOneTimeActions();
    }
}