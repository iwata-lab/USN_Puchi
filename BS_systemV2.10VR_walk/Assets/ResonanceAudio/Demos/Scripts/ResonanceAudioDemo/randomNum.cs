using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;


public static class Cube1
{
    // ログを保存するフォルダ名。
    const string DirectoryName = "DebugLogs";

    // 最大件数。
    const int LogFileLimit = 100;

    static Cube1 ()
    {
        Application.logMessageReceived += OnReceived;
    }

    static void OnReceived(string condition, string stackTrace, LogType type)
    {
        if (!Directory.Exists(DirectoryName))
        {
            Directory.CreateDirectory(DirectoryName);
        }

        // ファイル名にタイムスタンプを含めてログ内容を書き出す。
        var now = DateTime.Now;
        var fileName = now.ToString("yyyy-MM-dd-HH-mm-ss-fffffff") + "_" + type.ToString() + ".txt";
        File.WriteAllText(DirectoryName + "/" + fileName, condition + "\n\n" + stackTrace);

        // 最大件数以上を超えるようであれば古いログから削除する。
        // ファイルのメタ情報でソートしたい場合はDirectoryInfo、FileInfoが便利。
        var directoryInfo = new DirectoryInfo(DirectoryName);
        var fileInfos = directoryInfo.GetFiles("*.txt");
        while (LogFileLimit < fileInfos.Length)
        {
            var oldestFileInfo = fileInfos.OrderBy(fileInfo => fileInfo.CreationTime).FirstOrDefault();
            File.Delete(oldestFileInfo.FullName);

            fileInfos = directoryInfo.GetFiles("*.txt");
        }
    }
}