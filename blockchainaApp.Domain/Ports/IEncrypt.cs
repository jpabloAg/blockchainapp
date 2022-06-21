namespace blockchainaApp.Domain.Ports
{
    public interface IEncrypt
    {
        string GetSHA256(string input);
    }
}
