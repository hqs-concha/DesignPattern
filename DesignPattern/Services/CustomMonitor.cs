using System;
using System.Threading;
using DesignPattern.Interface;
using Microsoft.Extensions.Primitives;

namespace DesignPattern.Services
{
    public class CustomMonitor : ICustomMonitor
    {
        private CancellationTokenSource _cts;
        public CustomMonitor()
        {
            ChangeToken.OnChange<ICustomMonitor>(GetToken, InvokeChanged, this);
        }

        public bool MonitoringEnabled { get; set; } = false;
        public string CurrentState { get; set; } = "Not monitoring";

        public void OnChange()
        {
            _cts.Cancel();
        }

        private void InvokeChanged(ICustomMonitor monitor)
        {
            if (MonitoringEnabled)
            {
                Console.WriteLine($"{monitor.MonitoringEnabled}:{monitor.CurrentState}");
            }
        }

        private IChangeToken GetToken()
        {
            _cts = new CancellationTokenSource();
            return new CancellationChangeToken(_cts.Token);
        }
    }
}
