using UnityEngine;
using System.Collections;

using System.IO;

//NowCardSet:是类型间交流卡组数据的公有类。直接采用静态类。

public class Common_NowCardSet {
    static string FileName;
    static public void LoadCardFile(string inputFile)
    {
        FileName = inputFile;
        StreamReader sr = new StreamReader(FileName, System.Text.Encoding.Default);
        string line;
        Length = 0;
        while ((line = sr.ReadLine()) != null)
        {
            CardSet[Length] = int.Parse(line);
            Length++;
        }

    }
    static public void SaveCardFile()//不需要名字，自动利用即可读取的文件名即可。
    {
        FileStream fs = new FileStream(FileName, FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);
        //开始写入
        for (int i = 0; i < Length; i++)
        {
            sw.Write(CardSet[i]+'\n');
        }
        //清空缓冲区
        sw.Flush();
        //关闭流
        sw.Close();
        fs.Close();
    }
    public static int[] CardSet;//卡组
    public static int Length;//卡组长度
}

