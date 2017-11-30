namespace Example
{
    public interface IHeater
    {
        bool IsHot { get; }
        void On();
        void Off();
    }
}
