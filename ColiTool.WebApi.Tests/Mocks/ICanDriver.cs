﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColiTool.WebApi.Tests.Mocks
{
    public interface ICanDriver
    {
        void SendMessage(int id, byte[] data);
        byte[] ReceiveMessage();
    }
}