using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LitJson;

namespace Thrift_Opapi
{
    class Config
    {
        public string host;
        public int port;
        public int timeout;
        public override string ToString()
        {
            return string.Format("Name:{0},Level:{1},Age:{2}", host, port, timeout);
        }

    }
}
