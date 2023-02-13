using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class HTTPConnect : MonoBehaviour
{
    //private ArrayList List = new ArrayList(5);
    //private Rect rect = new Rect(10, 50, 150, 150);
    //�����ַ,д�Լ��������ַ����   
    private string url = "";
    string tishi;
    //�½� List �������
    private int hour;
    private int minute;
    private int second;
    private int year;
    private int month;
    private int day;
    private string[] args = System.Environment.GetCommandLineArgs();
    public class ChinarMessage
    {
        [DllImport("User32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern int MessageBox(IntPtr handle, String message, String title, int type);//���巽��
    }

    private void Awake()
    {
        string path = System.Environment.CurrentDirectory;
        if (args.Length <= 1)
        {
            ChinarMessage.MessageBox(IntPtr.Zero, "���ƽ̨����Ϸ", "��ʾ", 0);
            Application.Quit();
            return;
        }
        if (args[1] == "fromxzq")
        {

        }
        else
        {
            ChinarMessage.MessageBox(IntPtr.Zero, "���ƽ̨����Ϸ", "��ʾ", 0);
            Application.Quit();
            return;
        }
        hour = DateTime.Now.Hour;
        minute = DateTime.Now.Minute;
        second = DateTime.Now.Second;
        year = DateTime.Now.Year;
        month = DateTime.Now.Month;
        day = DateTime.Now.Day;
        string _encryptContent = "@&*23_kid$%@" + (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        Debug.Log(_encryptContent);

        string str1 = EncryptMD5_32(_encryptContent);
        Debug.Log(str1);
        url = "http://127.0.0.1:9999/c/c?checkcode=" + str1;
        Debug.Log(url);
        StartCoroutine(Startclint());
    }


    /// <returns></returns>
    public static string EncryptMD5_32(string _encryptContent)
    {
        string content_Normal = _encryptContent;
        string content_Encrypt = "";
        MD5 md5 = MD5.Create();

        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(content_Normal));

        for (int i = 0; i < s.Length; i++)
        {
            content_Encrypt = content_Encrypt + s[i].ToString("X2");
        }
        return content_Encrypt;
    }

    IEnumerator Startclint()
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.error != null)
        {
            ChinarMessage.MessageBox(IntPtr.Zero, "���������", "��ʾ", 0);
            Application.Quit();
        }
        else
        {
            ResultData resultData = JsonUtility.FromJson<ResultData>(unityWebRequest.downloadHandler.text);

            //Debug.Log(itemdata);
            //���� ConstructItemDatabase() ����
            if (resultData.code == 1)
            {
                SceneManager.LoadSceneAsync("startscene");
            }
            else
            {
                ChinarMessage.MessageBox(IntPtr.Zero, Unicode2String(resultData.msg), "��ʾ", 0);
                Application.Quit();
            }
        }
        //�����󵽵�����ת���� JsonData array ���ͣ����洢��itemdata��

        //��������

    }
    public string Unicode2String(string source)
    {
        return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase).Replace(
             source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
    }

}
[Serializable]
public class ResultData {
    public int code;
    public string msg;
}
