#if UNITY_EDITOR 
using System.IO;
using System.Xml;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using PSSDK;
public class AndroidPostProcessBuild : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{

    private string TAG = "PSSDK_Plugin==>";
    public int callbackOrder => 0;
    string verionFilePath = "";
    private string fileName = "pssdk_version.xml";
    public void OnPreprocessBuild(BuildReport report)
    {
        string verFilePath = CreateVersionXml();
        verionFilePath = verFilePath;
        WriteVersionToXml(verFilePath);
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        //you don't need to handle suitaion while unity exprot AndroidStudio Project,
        //unity will merge res dir automatically into unityLibrary/unity-android-resources module

        //// check if android platform
        //if (report.summary.platform != UnityEditor.BuildTarget.Android)
        //{
        //    return;
        //}
        //Debug.Log("OnPostprocessBuild " + report.summary.outputPath);
        //string outPath = report.summary.outputPath;
        //if (outPath.EndsWith("apk") || outPath.EndsWith("aab"))
        //{
        //    // do not handle if export apk or aab file
        //    //unity will merge version.xml in xml dir automatically
        //    return;
        //}

        ////check xml dir in exported AndroidStudio project
        //string filePath = outPath + Path.DirectorySeparatorChar + "unityLibrary" +
        //    Path.DirectorySeparatorChar + "src" + Path.DirectorySeparatorChar + "main" +
        //    Path.DirectorySeparatorChar + "res" + Path.DirectorySeparatorChar + "xml" + Path.DirectorySeparatorChar;
        //if (!File.Exists(filePath))
        //{
        //    //create xml dir in export AndroidStudio project
        //    File.Create(filePath);
        //}

        //// copy verion.xml into  exported AndroidStudio project
        //string destPath = filePath + fileName;
        //Debug.Log(TAG + "copying " + verionFilePath + " to " + destPath);
        //File.Copy(verionFilePath, destPath);
        //Debug.Log("copy end");
    }

    private string CreateVersionXml()
    {
        string dirPath = Application.dataPath + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar +
            "Android" + Path.DirectorySeparatorChar + "res" + Path.DirectorySeparatorChar + "xml";
        bool hasDir = Directory.Exists(dirPath);
        if (!hasDir)
        {
            Debug.Log(TAG + dirPath + " not exist, create now");
            Directory.CreateDirectory(dirPath);
        }
        string versionFilePath = dirPath + Path.DirectorySeparatorChar + fileName;
        if (!System.IO.File.Exists(versionFilePath))
        {
            System.IO.File.Create(versionFilePath).Dispose();
        }
        else
        {
            Debug.Log(TAG + "delete old mssdk version xml");

            File.Delete(versionFilePath);
            System.IO.File.Create(versionFilePath).Dispose();
        }



        return versionFilePath;
    }


    private void WriteVersionToXml(string xmlFilePath)
    {
        Debug.Log(TAG + "writing ver xml for android to file :" + xmlFilePath);


        // version
        string iOS_SDK_Version = PSSDKApi.iOS_SDK_Version;
        string Android_SDK_Version = PSSDKApi.Android_SDK_Version;
        string Unity_Package_Version = PSSDKApi.Unity_Package_Version;
        Debug.Log(TAG + "iOS_SDK_Version=" + iOS_SDK_Version);
        Debug.Log(TAG + "Android_SDK_Version=" + Android_SDK_Version);
        Debug.Log(TAG + "Unity_Package_Version=" + Unity_Package_Version);


        XmlDocument myXmlDoc = new XmlDocument();
        XmlElement rootElement = myXmlDoc.CreateElement("version");
        myXmlDoc.AppendChild(rootElement);


        //初始化第一层的第一个子节点
        XmlElement firstLevelElement1 = myXmlDoc.CreateElement("pssdk_version");
        //填充第一层的第一个子节点的属性值（SetAttribute）
        firstLevelElement1.SetAttribute("android_ver", Android_SDK_Version);
        firstLevelElement1.SetAttribute("ios_ver", iOS_SDK_Version);

        firstLevelElement1.SetAttribute("unity_ver", Unity_Package_Version);

        rootElement.AppendChild(firstLevelElement1);
        //将xml文件保存到指定的路径下
        myXmlDoc.Save(xmlFilePath);
    }
}
#endif