using System;

namespace TCPServer
{
    public class WorkflowEventArgs : EventArgs
    {
        public byte[] Payload { get; set; }
    }
}