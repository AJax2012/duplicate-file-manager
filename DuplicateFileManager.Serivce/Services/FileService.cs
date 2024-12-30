using System.Collections.Immutable;
using System.Security.Cryptography;
using System.Text;

namespace DuplicateFileManager.Serivce.Services;

public class FileService : IFileService
{
    public void DeleteFile(string filePath) => File.Delete(filePath);

    public string GetFileHash(string filePath)
    {
        using var md5 = MD5.Create();
        using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return BitConverter.ToString(md5.ComputeHash(stream));
    }

    public IReadOnlyList<string> GetFilesFromDirectory(string directoryPath, List<string> extensions) =>
        Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
            .Where(x => extensions.Contains(Path.GetExtension(x)))
            .ToImmutableList() ?? [];

    public string GetFileName(string filePath) => Path.GetFileName(filePath);

    public string DecodeFilepath(string base64String) => Encoding.UTF8.GetString(Convert.FromBase64String(base64String));

    public string EncodeFilepath(string stringToEncode) => Convert.ToBase64String(Encoding.UTF8.GetBytes(stringToEncode));
}