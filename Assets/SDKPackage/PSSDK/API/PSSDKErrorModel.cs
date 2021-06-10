using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSDKErrorModel
{
    private int code;
    private string message;

     public string Message { get => message; set => message = value; }
    public int Code { get => code; set => code = value; }
}
