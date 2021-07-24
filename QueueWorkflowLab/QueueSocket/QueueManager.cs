using Common.Core.DependencyInjection;
using System.Collections.Generic;

namespace QueueSocket
{
    [ServiceLocate(typeof(IQueueManager<>))]
    public class QueueManager : IQueueManager<int>
    {
        private static Queue<int> _queue;

        public QueueManager()
        {
            _queue = new Queue<int>();
        }

        public int PopFromQueue()
        {
            if (_queue.TryDequeue(out int ret))
            {
                return ret;
            }

            return -1;
        }

        public void PushToQueue(int number)
        {
            _queue.Enqueue(number);
        }
    }
}
