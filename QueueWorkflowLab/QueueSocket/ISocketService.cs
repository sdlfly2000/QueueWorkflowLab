using System;

namespace QueueSocket
{
    public interface ISocketService : IDisposable
    {
        void Start();

        void Stop();
    }
}
