namespace QueueSocket
{
    public interface IQueueService<T>
    {
        void PushToQueue(T number);

        T PopFromQueue();
    }
}
