using UnityEngine;
using System.Collections;

using System.IO;
//NowCardSet:是类型间交流卡组数据的公有类。直接采用静态类。

public class NowCardSet {
    static string FileName;
    static public void LoadCardFile(string inputFile)
    {
        FileName = inputFile;
    }
    static public void SaveCardFile()//不需要名字，自动利用即可读取的文件名即可。
    {

    }
    static int[] CardSet;
}

