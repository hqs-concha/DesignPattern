using DesignPattern.Services.Event;

namespace DesignPattern.Model
{
    public class OrderSubmittedEvent : EventData
    {
        public int OrderNo { get; set; }
        public string Remarks { get; set; }
    }
}
