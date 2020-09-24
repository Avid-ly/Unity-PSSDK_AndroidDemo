using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text.RegularExpressions;

using PSSDK;

public class TestPSSDKCall : MonoBehaviour
{
	private const string PRODUCTID = "600167";
    private const string GAMERID = "001";
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

	}

    public void onInitClick() {


        string productId = PRODUCTID;

#if UNITY_IOS && !UNITY_EDITOR
            productId = "1000152";

#elif UNITY_ANDROID && !UNITY_EDITOR

#endif
        
        PSSDKApi.init(productId,GAMERID);
        showLogMsg("onInitClick press  ");

    }

	public void onRequestPrivacyDataClick() {

		PSSDKApi.requestPrivacyData (new System.Action<string,bool,string,bool> (onRequestPrivacyDataSuccess),
                                     new System.Action<string>(onRequestPrivacyDataFail));
        showLogMsg("onRequestPrivacyDataClick press  ");

    }


   public void onLoadPrivacyDialogClick() {
        PSSDKApi.loadPrivacyDialog(new System.Action<string>(onLoadPrivacyDataSuccess),
            new System.Action<string>(onLoadPrivacyDataFail)
        );

        showLogMsg("onLoadPrivacyDialogClick press  ");
    }

    public void onShowPrivacyDialogClick() {
        PSSDKApi.showPrivacyDialog(new System.Action<PSSDKConstant.PrivacyStatusEnum,string>(onShowPrivacyDialogCallBack)
        );
        showLogMsg("onShowPrivacyDialogClick press  ");
    }

     public void onUpdatePrivactAccessStatusClick() {


        PSSDKApi.updateAccessPrivacyInfoStatus("gdpr",true,
            new System.Action<string>(onUpdatePrivacyDataSuccess),
            new System.Action<string>(onUpdatePrivacyDataFail)
        );

        showLogMsg("onUpdatePrivactAccessStatusClick press  ");

        
    }


    private void onRequestPrivacyDataSuccess(string privacy, bool ignore, string type, bool accepted)
    {
    
        showLogMsg("onRequestPrivacyDataSuccess  "+"privacy: "+privacy+" ignore:"+ignore+" type : "+type+" accepted : "+accepted);
    }

     private void onRequestPrivacyDataFail(string reason)
    {
        showLogMsg("onRequestPrivacyDataFail  "+"reason: "+reason);
    }

    private void onLoadPrivacyDataSuccess(string result)
    {
        showLogMsg("onLoadPrivacyDataSuccess  "+"result: "+result);
    }

     private void onLoadPrivacyDataFail(string reason)
    {
        showLogMsg("onLoadPrivacyDataFail  "+"reason: "+reason);
    }

    private void onShowPrivacyDialogCallBack(PSSDKConstant.PrivacyStatusEnum result, string reason)
    {
        showLogMsg("onShowPrivacyDialogCallBack  "+"result: "+result+" reason:"+reason);
    }

    private void onUpdatePrivacyDataSuccess(string result)
    {

        showLogMsg("onRequestPrivacyDataSuccess result "+result);
    }

     private void onUpdatePrivacyDataFail(string reason)
    {
        showLogMsg("onUpdatePrivacyDataFail reason "+reason);
    }


    private void showLogMsg(string msg){
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = msg;
        Debug.Log ("===> msg " + msg);
    }

}

