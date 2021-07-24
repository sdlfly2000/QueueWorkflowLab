namespace QueueSocket
{
    public interface IQueueManager<T>
    {
        void PushToQueue(T number);

        T PopFromQueue();
    }
}
