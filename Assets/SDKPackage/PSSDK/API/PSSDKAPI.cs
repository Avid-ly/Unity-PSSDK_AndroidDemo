using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace PSSDK
{

	public class PSSDKApi
	{
		public readonly static string iOS_SDK_Version = "2.0.0.1";
		public readonly static string Android_SDK_Version = "2.0.0.1";
		public readonly static string Unity_Package_Version = "2.0.0.1";

		private static PSSDKCall sdkCall = null;

		private static void instanceOfCall() {
			if (sdkCall == null) {
				sdkCall = new PSSDKCall (); 
			}
		}
		

		public static void requestAuthStatus(string pid, string playerId,Action<PSSDKAuthModel> success, Action<string> fail)
		{
			Debug.Log("===> call requestAuthStatus in pssdkapi");
			instanceOfCall();
			sdkCall.requestPrivacyAuthorization(pid, playerId, success, fail);
		}
    }

}
