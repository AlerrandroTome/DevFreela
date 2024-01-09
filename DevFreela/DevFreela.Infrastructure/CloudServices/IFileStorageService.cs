namespace DevFreela.Infrastructure.CloudServices
{
    public interface IFileStorageService
    {
        void UploadFile(byte[] bytes, string fileName);
    }
}
