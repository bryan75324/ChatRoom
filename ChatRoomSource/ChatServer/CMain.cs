﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatRoom
{
    internal class CMain
    {
        private static void Main(string[] args)
        {
            CChatServer server = new CChatServer();
            server.Bind(4099);
            server.RunAccept();
        }
    }
}