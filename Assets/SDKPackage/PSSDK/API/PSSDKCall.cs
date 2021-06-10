
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
			private static extern void setIosPSSDKCallbackWithClassAndFunction(string callbackClassName, string callbackFunctionName);

			 

			[DllImport("__Internal")]
			private static extern void requestPrivacyAuthorization(string pid,string playerId);

			
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.ps.sdk.unity.PSSDKProxy";
			private readonly static string JavaClassStaticMethod_init = "init";
			private readonly static string JavaClassStaticMethod_requestPrivacyData = "requestPrivacyData";
			private readonly static string JavaClassStaticMethod_loadPrivacyDialog = "loadPrivacyDialog";
			private readonly static string JavaClassStaticMethod_showPrivacyDialog = "showPrivacyDialog";
			 private readonly static string JavaClassStaticMethod_requestAuth = "requestAuth";

			private readonly static string JavaClassStaticMethod_updateAccessPrivacyInfoStatus = "updateAccessPrivacyInfoStatus";

 
#else
		// "do nothing";
#endif


		public PSSDKCall() {
            PSSDKObject.getInstance();
#if UNITY_IOS && !UNITY_EDITOR
            setIosPSSDKCallbackWithClassAndFunction(PSSDKObject.Unity_Callback_Class_Name, PSSDKObject.Unity_Callback_Function_Name);
				Debug.Log ("===> PSSDKCall instanced.");
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> PSSDKCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
#endif
        }
 

		public void requestPrivacyAuthorization(string pid, string playerId, Action<PSSDKAuthModel> success, Action<string> fail) {
			Debug.Log("===> call requestPrivacyAuthorization in pssdkcall");
			PSSDKObject.getInstance().setRequestAuthCallback(success, fail);



#if UNITY_IOS && !UNITY_EDITOR
            requestPrivacyAuthorization(pid,playerId);

#elif UNITY_ANDROID && !UNITY_EDITOR

			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_requestAuth,pid,playerId,
								PSSDKObject.Unity_Callback_Class_Name,
								PSSDKObject.Unity_Callback_Function_Name);
			}
#endif

		}




	}
}
