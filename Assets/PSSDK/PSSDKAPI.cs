using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PSSDK
{

	public class PSSDKApi
	{
		private static PSSDKCall sdkCall = null;


		private static void instanceOfCall() {
			if (sdkCall == null) {
				sdkCall = new PSSDKCall (); 
			}
		}
		
		public static void init (string productId,string gamerId) {
			 	Debug.Log("===> call requestPrivacyData in pssdkapi");
				instanceOfCall ();
				if(productId == null || productId == ""){
					Debug.Log("===> no productid");
					return;
				}
				if(gamerId==null){
					gamerId="";
				}
				sdkCall.init (productId, gamerId);
			
		}

		public static void requestPrivacyData (Action<string,bool,string,bool> success, Action<string> fail) {
			 	Debug.Log("===> call requestPrivacyData in pssdkapi");
				instanceOfCall ();
				sdkCall.requestPrivacyData (success, fail);
		}


		public static void loadPrivacyDialog (Action<string> success, Action<string> fail) {
			 	Debug.Log("===> call loadPrivacyDialog in pssdkapi");
				instanceOfCall ();
				sdkCall.loadPrivacyDialog (success, fail);
			
		}

		public static void showPrivacyDialog (Action<PSSDKConstant.PrivacyStatusEnum,string> callBack) {
				instanceOfCall ();
				sdkCall.showPrivacyDialog (callBack);
		}

		public static void updateAccessPrivacyInfoStatus (string privacyName,bool status,Action<string> success, Action<string> fail) {
				if(privacyName==null || privacyName==""){
					Debug.Log("===> no privacyName");
					return;
				}
				if(status==null){
					Debug.Log("===> no status");
					return;
				}
				instanceOfCall ();
				sdkCall.updateAccessPrivacyInfoStatus (privacyName, status,success, fail);
		}

        
    }

}
