namespace DesignPattern.Interface
{
    public interface ICustomMonitor
    {
        bool MonitoringEnabled { get; set; }
        string CurrentState { get; set; }
        void OnChange();
    }
}
