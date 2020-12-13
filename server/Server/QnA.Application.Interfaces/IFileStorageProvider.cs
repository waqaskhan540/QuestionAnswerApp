namespace QnA.Application.Interfaces
{
    public interface IFileStorageProvider
    {
        string SaveFile(byte[] content, string fileType);
    }
}
