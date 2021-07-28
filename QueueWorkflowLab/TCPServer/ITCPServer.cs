﻿using System;

namespace TCPServer
{
    public interface ITCPServer : IDisposable
    {
        void Start(int listenPort);
        void Stop();
    }
}
