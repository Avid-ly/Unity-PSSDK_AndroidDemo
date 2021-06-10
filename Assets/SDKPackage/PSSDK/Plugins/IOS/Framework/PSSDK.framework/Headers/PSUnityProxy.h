//
//  AASUnityProxy.h
//  AAS
//
//  Created by steve on 2017/5/18.
//  Copyright © 2017年 liuguojun. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface PSUnityProxy : NSObject

#pragma mark - callback

extern "C" void setIosPSSDKCallbackWithClassAndFunction(const char* callbackClassName, const char* callbackFunctionName);

#pragma mark - Function

extern "C" void requestPrivacyAuthorization(const char* productId, const char* playerId);

extern "C" void requestPrivacyAuthorizationWithOrientationForIos(const char* productId, const char* playerId, int orientation);

@end
