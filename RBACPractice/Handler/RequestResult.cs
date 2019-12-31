using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RequestResult
{
    public string Status { get; set; }

    public string Msg { get; set; }

    public RequestResult(string pStatus, string pMsg)
    {
        Status = pStatus;
        Msg = pMsg;
    }

    public RequestResult()
    {
        Status = "1";
        Msg = "";
    }

}
