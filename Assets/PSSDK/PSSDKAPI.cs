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

		public static void requestPrivacyData (string productId,string gamerId,Action<string,bool,string,bool> success, Action<string> fail) {
			 	Debug.Log("===> call requestPrivacyData in pssdkapi");
				instanceOfCall ();
				sdkCall.requestPrivacyData (productId, gamerId, success, fail);
			
		}

		public static void showPrivacyDialog (string productId,string privacyName,Action<PSSDKConstant.PrivacyStatusEnum,string> callBack) {
				instanceOfCall ();
				sdkCall.showPrivacyDialog (productId, privacyName, callBack);
		}

		public static void updateAccessPrivacyInfoStatus (string productId,string gamerId,bool status,string privacyName,Action<string> success, Action<string> fail) {
				instanceOfCall ();
				sdkCall.updateAccessPrivacyInfoStatus (productId, gamerId, status, privacyName, success, fail);
		}

        
    }

}
