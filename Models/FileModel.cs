
// FileModel.cs
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Emit;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Xml.Linq;
using System;
/*
public class FileModel
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}
*/
public class FileModel
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public DateTime UploadDate { get; set; }
    public long FileSize { get; set; }
    public string Username { get; set; } // Hangi kullanýcýnýn yüklediði
    public string Owner { get; set; }  // isteðe baðlý

}