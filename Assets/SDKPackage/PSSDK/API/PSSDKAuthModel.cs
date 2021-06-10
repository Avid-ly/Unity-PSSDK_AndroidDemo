using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSSDKAuthModel
{


   

    private AuthCollectionStatus authCollectionStatus;

    private AuthSharingStatus authSharingStatus;

    private string privacyPolicy;


         
     public AuthCollectionStatus AuthCollectionStatus1 { get => authCollectionStatus; set => authCollectionStatus = value; }
    public AuthSharingStatus AuthSharingStatus1 { get => authSharingStatus; set => authSharingStatus = value; }
    public string PrivacyPolicy { get => privacyPolicy; set => privacyPolicy = value; }

    public enum RequestStatus
    {
        REQUESTED, //请求过授权
        NOT_REQUESTED //未请求过授权
    }


    //能否收集
    public enum AuthCollectionStatus
    {
        UNKNOW, //未知
        AGREE,//同意
        DISAGREE //拒绝
    }


    //能否分享
    public enum AuthSharingStatus
    {
        UNKNOW, //未知
        AGREE,//同意
        DISAGREE //拒绝
    }



}
