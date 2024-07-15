namespace Server.Service
{
    public interface IPasteService
    {
        string Upload(HttpContext context, string content);
        string Get(string id);
    }
}
