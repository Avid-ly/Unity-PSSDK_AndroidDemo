
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace PSSDK
{
    public class PSSDKCall {


#if UNITY_IOS && !UNITY_EDITOR

    		[DllImport("__Internal")]
			private static extern void setIosCallbackWithClassAndFunction(string callbackClassName, string callbackFunctionName);

			[DllImport("__Internal")]
			private static extern void initIosSDK(string productId);

			[DllImport("__Internal")]
			private static extern void setLoginCallback();

			[DllImport("__Internal")]
			private static extern void login();

			[DllImport("__Internal")]
			private static extern void showUserCenter();

			[DllImport("__Internal")]
			private static extern string getIosFacebookLoginedToken();

			[DllImport("__Internal")]
			private static extern string getGGID();

			
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.ps.sdk.unity.PSSDKProxy";
			private readonly static string JavaClassStaticMethod_requestPrivacyData = "requestPrivacyData";
			private readonly static string JavaClassStaticMethod_showPrivacyDialog = "showPrivacyDialog";
			private readonly static string JavaClassStaticMethod_updateAccessPrivacyInfoStatus = "updateAccessPrivacyInfoStatus";

#else
        // "do nothing";
#endif


        public PSSDKCall() {
            PSSDKObject.getInstance();
#if UNITY_IOS && !UNITY_EDITOR
				Debug.Log ("===> PSSDKCall instanced.");
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> PSSDKCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
#endif
        }


        public void requestPrivacyData(string productId,string gamerId, Action<string,bool,string,bool> success, Action<string> fail) {
           
			Debug.Log("===> call requestPrivacyData in pssdkcall");
            // 设置callback回调
            PSSDKObject.getInstance().setRequestPrivacyDataSucceedCallback(success, fail);
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
            setLoginCallback();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_requestPrivacyData,
								PSSDKObject.Unity_Callback_Class_Name,
								PSSDKObject.Unity_Callback_Function_Name,
								productId,gamerId);
			}
#endif
        }


        public void showPrivacyDialog(string productId,string privacyName,Action<PSSDKConstant.PrivacyStatusEnum, string> callback) {
            // 设置callback回调
            PSSDKObject.getInstance().setPrivacyInfoStatusCallBack(callback);
            // 调用原生的方法
#if UNITY_IOS && !UNITY_EDITOR
			setLoginCallback();

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_showPrivacyDialog,
								PSSDKObject.Unity_Callback_Class_Name,
								PSSDKObject.Unity_Callback_Function_Name,
								productId,privacyName);
			}
#endif
        }

        public void updateAccessPrivacyInfoStatus(string productId, string gamerId, bool privacyStatus, string privacyName,Action<string> success,Action<string> fail)
        {
            Debug.Log("===> updateAccessPrivacyInfoStatus in PSSDKCall.");
            PSSDKObject.getInstance().setUpdatePrivacyInfoStatusCallBack(success,fail);
#if UNITY_IOS && !UNITY_EDITOR
			

#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic(JavaClassStaticMethod_updateAccessPrivacyInfoStatus,
								PSSDKObject.Unity_Callback_Class_Name,
								PSSDKObject.Unity_Callback_Function_Name,
								productId,gamerId,privacyStatus? "true":"false",privacyName);
			}
#endif
        }

    }
}
