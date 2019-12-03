using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RGPTDemo.Handler
{
    public class RequestResponse
    {
        public string Status { get; set; }

        public string Msg { get; set; }

        public List<DataTable> Data { get; set; }

        public RequestResponse()
        {
            Status = "1";
            Msg = "";
            Data = new List<DataTable>();
        }
    }
}