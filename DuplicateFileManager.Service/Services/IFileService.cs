namespace DuplicateFileManager.Service.Services;

public interface IFileService
{
    void DeleteFile(string filePath);
    string GetFileHash(string filePath);
    IReadOnlyList<string> GetFilesFromDirectory(string directoryPath, List<string> extensions);
    string GetFileName(string filePath);
    string DecodeFilepath(string base64String);
    string EncodeFilepath(string stringToEncode);
}