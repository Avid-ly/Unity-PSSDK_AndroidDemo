using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSSDK;
using System;
public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onRequestAuthClick()
    {
        PSSDKApi.requestAuthStatus("your pid", "your gameid",
            new System.Action<PSSDKAuthModel>(onRequsetAuthSuccess),
            new System.Action<string>(onRequsetAuthFail)
            );
    }


    public void onRequsetAuthSuccess(PSSDKAuthModel authModel)
    {
        Debug.Log("pssdk  onRequsetAuthSuccess PrivacyPolicy=" + authModel.PrivacyPolicy + " AuthCollectionStatus1=" + authModel.AuthCollectionStatus1
            + " AuthSharingStatus1=" + authModel.AuthSharingStatus1);
    }

    public void onRequsetAuthFail(string message)
    {
        Debug.Log("pssdk onRequsetAuthFail :" + message);
    }


}
