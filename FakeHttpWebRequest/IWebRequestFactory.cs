using System.Net;

namespace FakeHttpWebRequest
{
    public interface IWebRequestFactory
    {
        WebRequest Create(string url);
    }
}