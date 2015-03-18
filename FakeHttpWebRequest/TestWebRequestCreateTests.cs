using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeHttpWebRequest
{
    [TestClass]
    public class TestWebRequestCreateTests
    {

        [TestMethod]
        public void BasicExample()
        {

            var response = "my response string here";
            WebRequest.RegisterPrefix("test", new TestWebRequestCreate());
            var request = TestWebRequestCreate.CreateTestRequest(response);

            var url = "test://MyUrl";

            var f = WebRequest.Create(url);

            var responce = f.GetResponse() as HttpWebResponse;

            var stream = responce.GetResponseStream();

            var content = new StreamReader(stream).ReadLine();

            Assert.AreEqual(response, content);

        }

    }
}
