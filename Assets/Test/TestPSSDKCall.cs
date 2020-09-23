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

	public void onRequestPrivacyDataClick() {


        string productId = PRODUCTID;

#if UNITY_IOS && !UNITY_EDITOR
            productId = "1000152";

#elif UNITY_ANDROID && !UNITY_EDITOR

#endif
        
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "call onRequestPrivacyDataClick";

		PSSDKApi.requestPrivacyData (productId,GAMERID,
            new System.Action<string,bool,string,bool> (onRequestPrivacyDataSuccess),
            new System.Action<string>(onRequestPrivacyDataFail));
        Debug.Log("===> call onRequestPrivacyDataClick");
    }


    public void onShowPrivacyDialogClick() {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "call onShowPrivacyDialogClick";
        Debug.Log("===> onShowPrivacyDialogClick pressed ");
        PSSDKApi.showPrivacyDialog(PRODUCTID,"gdpr",
            new System.Action<PSSDKConstant.PrivacyStatusEnum,string>(onShowPrivacyDialogCallBack)
        );
        Debug.Log("===> call onShowPrivacyDialogClick");
    }

     public void onUpdatePrivactAccessStatusClick() {

        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "call onUpdatePrivactAccessStatusClick";

        Debug.Log("===> onUpdatePrivactAccessStatusClick pressed ");
        PSSDKApi.updateAccessPrivacyInfoStatus(PRODUCTID,GAMERID,true,"gdpr",
            new System.Action<string>(onUpdatePrivacyDataSuccess),
            new System.Action<string>(onUpdatePrivacyDataFail)
        );
        Debug.Log("===> call onUpdatePrivactAccessStatusClick");
    }


    private void onRequestPrivacyDataSuccess(string privacy, bool ignore, string type, bool accepted)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "privacy: "+privacy+" ignore:"+ignore+" type : "+type+" accepted : "+accepted;
        Debug.Log ("===> onRequestPrivacyDataSuccess Callback at: " + privacy);
    }

     private void onRequestPrivacyDataFail(string reason)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "fail reason: "+reason;
        Debug.Log ("===> onRequestPrivacyDataFail reason : " + reason);
    }

    private void onShowPrivacyDialogCallBack(PSSDKConstant.PrivacyStatusEnum result, string reason)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "result: "+result+" reason:"+reason;
        Debug.Log ("===> onShowPrivacyDialogCallBack Callback at: " + result);
    }

    private void onUpdatePrivacyDataSuccess(string result)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "result: "+result;
        Debug.Log ("===> onRequestPrivacyDataSuccess Callback at: " + result);
    }

     private void onUpdatePrivacyDataFail(string reason)
    {
        Text text = GameObject.Find("CallText").GetComponent<Text>();
        text.text = "fail reason: "+reason;
        Debug.Log ("===> onUpdatePrivacyDataFail reason : " + reason);
    }


}

