using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PSSDKMiniJSON;
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

		//pssdk 1102 请求授权结果
		private readonly static string Unity_Callback_Message_Function_PSSDK_REQUEST_AUTH_Success = "PSSDK_PSSDK_REQUEST_AUTH_SUCCESS";
		private readonly static string Unity_Callback_Message_Function_PSSDK_REQUEST_AUTH_FAIL= "PSSDK_PSSDK_REQUEST_AUTH_FAIL";


		// 授权结果
		private readonly static string Unity_Callback_Message_Function_PSSDK_REQUESTPRIVACYSTATUS_CallBack = "PSSDK_REQUESTPRIVACYSTATUS_CallBack";

        // 反馈结果到服务器
        private readonly static string Unity_Callback_Message_Function_PSSDK_UPDATE_Success    = "PSSDK_UPDATE_Success"; 
        private readonly static string Unity_Callback_Message_Function_PSSDK_UPDATE_Fail   = "PSSDK_UPDATE_Fail"; 


        
        // ios
        private readonly static string Unity_Callback_Message_Function_GetUserRegion_Complete  		= "GetUserRegion_Complete";           	// 获取用户归属
		private readonly static string Unity_Callback_Message_Function_GetAuthorization_Complete  		= "Authorization_Complete";         		// 获取用户权限
		private readonly static string Unity_Callback_Message_Function_UpdateAuthorization_Complete  	= "UpdateAuthorization_Complete";   		// 更新用户权限
		private readonly static string Unity_Callback_Message_Function_GetAlertInfo_Complete  			= "GetAlertInfo_Complete";           	// 获取弹窗信息
		private readonly static string Unity_Callback_Message_Function_ShowAlert_Complete  			= "ShowAlert_Complete";             		// 使用弹窗请求用户权限
        
        private readonly static string Unity_Callback_Message_Parameter_country   = "country";
		private readonly static string Unity_Callback_Message_Parameter_province  = "province";
		private readonly static string Unity_Callback_Message_Parameter_city      = "city";
		
		private readonly static string Unity_Callback_Message_Parameter_privacyPolicy      = "privacyPolicy";
		private readonly static string Unity_Callback_Message_Parameter_authorization      = "authorization";
		private readonly static string Unity_Callback_Message_Parameter_ignore             = "ignore";
		private readonly static string Unity_Callback_Message_Parameter_type               = "type";
		private readonly static string Unity_Callback_Message_Parameter_succeed            = "succeed";


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


		Action<PSSDKAuthModel> requestAuthSuccessCallback;
		Action<string> requestAuthFailCallback;

		Action<PSSDKConstant.PrivacyStatusEnum,string> privacyInfoStatusCallBack;

		Action<string> updatePrivacyStatusSucceedCallback;
		Action<string> updatePrivacyStatusFailCallback;

	 

		public void setRequestAuthCallback(Action< PSSDKAuthModel> success, Action<string> fail)
		{
			requestAuthSuccessCallback = success;
			requestAuthFailCallback = fail;
		}

		public void setRequestPrivacyDataSucceedCallback(Action<string, bool, string, bool> success, Action<string> fail) {
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

        // 将jsonobj转换了map<>
        public bool getInnerJsonParamterBoolValue(Hashtable jsonObj,string key) {
            bool msg = false;
            if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
            {
                Hashtable innerJsonObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                if (innerJsonObj.ContainsKey(key)) { 
                    msg = (bool)innerJsonObj[key];
                }
            }
            return msg;
        }

        // 将jsonobj转换了map<>
        public int getInnerJsonParamterIntValue(Hashtable jsonObj,string key) {
            int msg = 0;
            if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter))
            {
                Hashtable innerJsonObj = (Hashtable)jsonObj[Unity_Callback_Message_Key_Parameter];
                if (innerJsonObj.ContainsKey(key)) { 
                    msg = (int)innerJsonObj[key];
                }
            }
            return msg;
        }

        public void onNativeCallback(string message) {

        	Debug.Log ("===> message : " + message);
			Hashtable jsonObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode (message);

			string privacyName ="";
  			string  productid ="";
  			string  gamerId="";
  			bool    ignore=false;
  			string  type="";
  			bool    status=false;

			if (jsonObj.ContainsKey (Unity_Callback_Message_Key_Function)) {

				string function = (string)jsonObj[Unity_Callback_Message_Key_Function];
				string paras = (string)jsonObj[Unity_Callback_Message_Key_Parameter];


				Hashtable jsonValueObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode((string)jsonObj[Unity_Callback_Message_Key_Parameter]);


				Debug.Log("pssdk func" + function);
				Debug.Log("pssdk paras" + paras);

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

				else if (function.Equals(Unity_Callback_Message_Function_PSSDK_REQUEST_AUTH_Success))
				{ 
					if (requestAuthSuccessCallback != null)
					{
						PSSDKAuthModel model = new PSSDKAuthModel();
						
						string privacyPolicy = (string)jsonValueObj["privacyPolicy"];
						int collectionStatus = (int)jsonValueObj["collectionStatus"];

 						int sharingStatus = (int)jsonValueObj["sharingStatus"]; 



 
						model.PrivacyPolicy = privacyPolicy;

						if (collectionStatus == 0)
						{     
							model.AuthCollectionStatus1 = PSSDKAuthModel.AuthCollectionStatus.UNKNOW;
						}
						else  if(collectionStatus == 1)
						{
							model.AuthCollectionStatus1 = PSSDKAuthModel.AuthCollectionStatus.DISAGREE;
						}
						else  if (collectionStatus == 2)
						{
							model.AuthCollectionStatus1 = PSSDKAuthModel.AuthCollectionStatus.AGREE;
						}


						if (sharingStatus == 0)
						{
							model.AuthSharingStatus1 = PSSDKAuthModel.AuthSharingStatus.UNKNOW;
						}
						else if (sharingStatus == 1)
						{
							model.AuthSharingStatus1 = PSSDKAuthModel.AuthSharingStatus.DISAGREE;
						}
						else if (sharingStatus == 2)
						{
							model.AuthSharingStatus1 = PSSDKAuthModel.AuthSharingStatus.AGREE;
						}




						requestAuthSuccessCallback(model);
						
					}
					else
					{
						Debug.Log("===> can't run requestAuthSuccessCallback(), no delegate object.");
					}
				}
 
				else if (function.Equals(Unity_Callback_Message_Function_PSSDK_REQUEST_AUTH_FAIL))
				{
					if (requestAuthFailCallback != null)
					{
						Debug.Log("===> pssdk Unity_Callback_Message_Function_PSSDK_REQUEST_AUTH_FAIL.");

						requestAuthFailCallback(paras);

					}
					else
					{
						Debug.Log("===> can't run requestAuthFailCallback(), no delegate object.");
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

				// iOS
				// 获取授权状态
				else if (function.Equals (Unity_Callback_Message_Function_GetAuthorization_Complete)) {

					Debug.Log("===> call function " + Unity_Callback_Message_Function_GetAuthorization_Complete);
					
					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter)) {
						string json = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
						Debug.Log("parameter json : " + json);

						Hashtable paraObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode (json);

						privacyName = (string)paraObj[Unity_Callback_Message_Parameter_privacyPolicy];
						Debug.Log ("===> privacyName " + privacyName);
                    	ignore = (bool)paraObj[Unity_Callback_Message_Parameter_ignore];
                    	Debug.Log ("===> ignore " + ignore);
                    	type = Convert.ToString((int)paraObj[Unity_Callback_Message_Parameter_type]);
                    	Debug.Log ("===> type " + type);
                    	status = (bool)paraObj[Unity_Callback_Message_Parameter_authorization];
                    	Debug.Log ("===> status " + status);
                    	if (requestPrivacyDataSucceedCallback != null) {
							requestPrivacyDataSucceedCallback (privacyName, ignore, type, status);
						}
						else {
							Debug.Log ("===> can't run requestPrivacyDataSucceedCallback(), no delegate object.");
						}
            		}
            		else {
            			Debug.Log("===> Does not contain Parameter");
            		}
				}
				// 主动更新授权状态 
				else if (function.Equals (Unity_Callback_Message_Function_UpdateAuthorization_Complete)) {

					Debug.Log("===> call function " + Unity_Callback_Message_Function_UpdateAuthorization_Complete);

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter)) {
						string json = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
						Debug.Log("parameter json : " + json);

						Hashtable paraObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode (json);

                    	bool succeed = (bool)paraObj[Unity_Callback_Message_Parameter_succeed];
                    	Debug.Log ("===> succeed " + succeed);
                    	
                    	if (succeed) {
							if (updatePrivacyStatusSucceedCallback != null) {
								updatePrivacyStatusSucceedCallback(succeed?"success":"fail");
							}
							else {
								Debug.Log ("===> can't run updatePrivacyStatusSucceedCallback(), no delegate object.");
							}
						}
						else {
							if (updatePrivacyStatusFailCallback != null) {
								updatePrivacyStatusFailCallback("fail");
							}
							else {
								Debug.Log ("===> can't run updatePrivacyStatusFailCallback(), no delegate object.");
							}
						}
            		}
            		else {
            			Debug.Log("===> Does not contain Parameter");
            		}
				}
				// 获取弹窗信息
				else if (function.Equals (Unity_Callback_Message_Function_GetAlertInfo_Complete)) {

					Debug.Log("===> call function " + Unity_Callback_Message_Function_GetAlertInfo_Complete);

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter)) {
						string json = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
						Debug.Log("parameter json : " + json);

						Hashtable paraObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode (json);

                    	bool succeed = (bool)paraObj[Unity_Callback_Message_Parameter_succeed];
                    	Debug.Log ("===> succeed " + succeed);
                    	
	                    if (succeed) {
	                    	if (loadDialogDataSuccessCallback != null) {
								loadDialogDataSuccessCallback ("success");
							}
							else {
								Debug.Log ("===> can't run loadDialogDataSuccessCallback(), no delegate object.");
							}
	                    }
	                    else {
	                    	if (loadDialogDataFailCallback != null) {
								loadDialogDataFailCallback ("fail");
							}
							else {
								Debug.Log ("===> can't run loadDialogDataFailCallback(), no delegate object.");
							}
	                    }
            		}
            		else {
            			Debug.Log("===> Does not contain Parameter");
            		}
				}
				// 使用弹窗向用户请求授权
				else if (function.Equals (Unity_Callback_Message_Function_ShowAlert_Complete)) {

					Debug.Log("===> call function " + Unity_Callback_Message_Function_ShowAlert_Complete);

					if (jsonObj.ContainsKey(Unity_Callback_Message_Key_Parameter)) {
						string json = (string)jsonObj[Unity_Callback_Message_Key_Parameter];
						Debug.Log("parameter json : " + json);

						Hashtable paraObj = (Hashtable)PSSDKMiniJSON.MiniJSON.jsonDecode (json);

                    	bool authorization = (bool)paraObj[Unity_Callback_Message_Parameter_authorization];
                    	Debug.Log ("===> authorization " + authorization);
                    	
	                    if (privacyInfoStatusCallBack != null) {
	                    	if(authorization){
	                    		privacyInfoStatusCallBack (PSSDKConstant.PrivacyStatusEnum.PrivacyInfoStatusAccepted,"");
	                    	}
	                    	else {
	                    		privacyInfoStatusCallBack (PSSDKConstant.PrivacyStatusEnum.PrivacyInfoStatusDenied,"");
	                    	}
						}
						else {
							Debug.Log ("===> can't run privacyInfoStatusCallBack(), no delegate object.");
						}
            		}
            		else {
            			Debug.Log("===> Does not contain Parameter");
            		}
				}

				//unkown call
				else {
					Debug.Log ("===> onTargetCallback unkown function:" );
				}
			}
        }
	}
}


