using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraceXXJSON;
using System;

namespace PSSDK {
	public class PSSDKObject : MonoBehaviour
	{
		private static PSSDKObject instance = null;
		public static readonly string Unity_Callback_Class_Name = "PSSDK_Callback_Object";
		public static readonly string Unity_Callback_Function_Name = "onNativeCallback";

		public static readonly string Unity_Callback_Message_Key_Function = "callbackMessageKeyFunctionName";
		public static readonly string Unity_Callback_Message_Key_Parameter = "callbackMessageKeyParameter";

		// android
        private readonly static string Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACY_DATA_Success = "PSSDK_REQUESTPRIVACY_DATA_Success";
        private readonly static string Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACY_DATA_Fail    = "PSSDK_REQUESTPRIVACY_DATA_Fail";
		

    	// loaddialogDialogData回调
   		private readonly static string Unity_Callback_Message_Function_PSSDKLOADDIALOG_DATA_Success = "PSSDK_PSSDKLOADDIALOG_DATA_Success";
   		private readonly static string Unity_Callback_Message_Function_PSSDKLOADDIALOG_DATA_Fail = "PSSDK_PSSDKLOADDIALOG_DATA_Fail";


		// 授权结果
		private readonly static string Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACYSTATUS_CallBack = "PSSDK_REQUESTPRIVACYSTATUS_CallBack";

        // 反馈结果到服务器
        private readonly static string Unity_Callback_Message_Function_PSSDK_UPDATE_Success    = "PSSDK_UPDATE_Success"; 
        private readonly static string Unity_Callback_Message_Function_PSSDK_UPDATE_Fail   = "PSSDK_UPDATE_Fail"; 


        
        // ios
        private readonly static string Unity_Callback_Message_Function_Login_Succeed_Complete   = "Init_Succeed_Complete";
        private readonly static string Unity_Callback_Message_Function_Login_Error_Complete     = "Init_Error_Complete";
        
        private readonly static string Unity_Callback_Message_Parameter_GameGuestId     = "gameGuestId";
        private readonly static string Unity_Callback_Message_Parameter_SignedRequest   = "signedRequest";
        private readonly static string Unity_Callback_Message_Parameter_LoginMode       = "loginMode";

		public static PSSDKObject getInstance()
		{
			if (instance == null) {
				GameObject polyCallback = new GameObject (Unity_Callback_Class_Name);
				polyCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (polyCallback);
				instance = polyCallback.AddComponent<PSSDKObject> ();
			}
			return instance;
		}

		Action<string,bool,string,bool> requestPrivacyDataSucceedCallback;
		Action<string> requestPrivacyDataFailCallback;

		Action<string> loadDialogDataSuccessCallback;
		Action<string> loadDialogDataFailCallback;

		Action<PSSDKConstant.PrivacyStatusEnum,string> privacyInfoStatusCallBack;

		Action<string> updatePrivacyStatusSucceedCallback;
		Action<string> updatePrivacyStatusFailCallback;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{

		}

		public void setRequestPrivacyDataSucceedCallback(Action<string,bool,string,bool> success, Action<string> fail) {
			requestPrivacyDataSucceedCallback = success;
			requestPrivacyDataFailCallback = fail;
		}
		public void setLoadPirvacyDialogDataCallBack(Action<string> success, Action<string> fail) {
			loadDialogDataSuccessCallback = success;
			loadDialogDataFailCallback = fail;
		}
		
		public void setPrivacyInfoStatusCallBack(Action<PSSDKConstant.PrivacyStatusEnum,string> callBack) {
			privacyInfoStatusCallBack = callBack;
		}

		public void setUpdatePrivacyInfoStatusCallBack(Action<string> success,Action<string> fail) {
			updatePrivacyStatusSucceedCallback = success;
			updatePrivacyStatusFailCallback=fail;
		}

        // 将jsonobj转换了map<>
        public string getInnerJsonParamterValue(Hashtable jsonObj,string key) {
            string msg = "";
            if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
            {
                Hashtable innerJsonObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                if (innerJsonObj.ContainsKey(key)) { 
                    msg = (string)innerJsonObj[key];
                }
            }
            return msg;
        }

        public void onNativeCallback(string message) {

        	Debug.Log ("===> message : " + message);
			Hashtable jsonObj = (Hashtable)TraceXXJSON.MiniJSON.jsonDecode (message);

  			string  privacyName ="";
  			string  productid ="";
  			string  gamerId="";
  			bool    ignore=false;
  			string  type="";
  			bool    status=false;

			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {

				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				//callback
				if (function.Equals (Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACY_DATA_Success)) {
                    privacyName = getInnerJsonParamterValue(jsonObj,"msg0");
                    ignore = getInnerJsonParamterValue(jsonObj, "msg1")== "true" ? true:false;
                    type=getInnerJsonParamterValue(jsonObj,"msg2");
                    status=getInnerJsonParamterValue(jsonObj,"msg3") == "true" ? true:false;
                    if (requestPrivacyDataSucceedCallback != null) {
						requestPrivacyDataSucceedCallback (privacyName, ignore,type,status);
					}
					else {
						Debug.Log ("===> can't run requestPrivacyDataSucceedCallback(), no delegate object.");
					}
				} 
				else if (function.Equals (Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACY_DATA_Fail)) {
                    string reason = getInnerJsonParamterValue(jsonObj, "msg0");
                    if (requestPrivacyDataFailCallback != null) {
						requestPrivacyDataFailCallback (reason);
					}
					else {
						Debug.Log ("===> can't run requestPrivacyDataFailCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_PSSDKLOADDIALOG_DATA_Success)) {
                    string result = getInnerJsonParamterValue(jsonObj, "msg0");
                    if (loadDialogDataSuccessCallback != null) {
						loadDialogDataSuccessCallback (result);
					}
					else {
						Debug.Log ("===> can't run loadDialogDataSuccessCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_PSSDKLOADDIALOG_DATA_Fail)) {
                    string reason = getInnerJsonParamterValue(jsonObj, "msg0");
                    if (loadDialogDataFailCallback != null) {
						loadDialogDataFailCallback (reason);
					}
					else {
						Debug.Log ("===> can't run loadDialogDataFailCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACYSTATUS_CallBack)) {
                    string  statusOrder = getInnerJsonParamterValue(jsonObj, "msg0");
                    string  unkownReason= getInnerJsonParamterValue(jsonObj, "msg1");
					if (privacyInfoStatusCallBack != null) {
						if(statusOrder=="0"){
                    		privacyInfoStatusCallBack (PSSDKConstant.PrivacyStatusEnum.PrivacyInfoStatusUnkown,unkownReason);
                    	}
                    	if(statusOrder=="1"){
                    		privacyInfoStatusCallBack (PSSDKConstant.PrivacyStatusEnum.PrivacyInfoStatusAccepted,"");
                    	}
                    	if(statusOrder=="2"){
                    		privacyInfoStatusCallBack (PSSDKConstant.PrivacyStatusEnum.PrivacyInfoStatusDenied,"");
                    	}
					}
					else {
						Debug.Log ("===> can't run privacyInfoStatusCallBack(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_PSSDK_UPDATE_Success)) {

					if (updatePrivacyStatusSucceedCallback != null) {
						updatePrivacyStatusSucceedCallback ("success");
					}
					else {
						Debug.Log ("===> can't run updatePrivacyStatusSucceedCallback(), no delegate object.");
					}
				}
				else if (function.Equals (Unity_Callback_Message_Function_PSSDK_UPDATE_Fail)) {
 					string  reason = getInnerJsonParamterValue(jsonObj, "msg0");
					if (updatePrivacyStatusFailCallback != null) {
						updatePrivacyStatusFailCallback (reason);
					}
					else {
						Debug.Log ("===> can't run updatePrivacyStatusFailCallback(), no delegate object.");
					}
				}				
			
				//unkown call
				else {
					Debug.Log ("===> onTargetCallback unkown function:" + function);
				}
			}
        }
	}
}


