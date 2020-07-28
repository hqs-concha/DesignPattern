
using System;
using System.Collections.Concurrent;
using DesignPattern.Interface;

namespace DesignPattern.Services
{
    public class OrderService : IOrderService
    {
        private int _num = 5;
        private readonly ConcurrentQueue<int> _queue = new ConcurrentQueue<int>();

        public string Create(int id)
        {
            _queue.Enqueue(id);
            return "订单提交中，请骚等。。。";
        }

        public string OrderHandler()
        {
            return $"抢到了";
        }
    }
}
