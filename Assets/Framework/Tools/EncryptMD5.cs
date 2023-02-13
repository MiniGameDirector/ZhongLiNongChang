using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EncryptMD5
{
    public static string EncryptString(string plainText)
    {
        using (var md = MD5.Create())
        {
            byte[] buffer = Encoding.Default.GetBytes(plainText);
            StringBuilder sb = new StringBuilder();//��Ҫ���ַ����������Ӳ����������
            foreach (var item in md.ComputeHash(buffer))
            {
                sb.Append(item.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
