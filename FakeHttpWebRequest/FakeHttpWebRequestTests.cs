using System;
using System.Diagnostics;
using System.Net;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace FakeHttpWebRequest
{
    [TestClass]
    public class FakeHttpWebRequestTests
    {

        [TestMethod]
        public void CodeWeavingTheCreateMethod()
        {
            const HttpStatusCode EXPECTED_CODE = HttpStatusCode.Created;

            var fakeRequest = MockRepository.GenerateStub<WebRequest>();
            var fakeResponce = MockRepository.GenerateStub<HttpWebResponse>();

            using (ShimsContext.Create())
            {

                System.Net.Fakes.ShimWebRequest.CreateString = (s) => fakeRequest;

                fakeRequest.Stub(x => x.GetResponse()).Return(fakeResponce);
                fakeResponce.Stub(x => x.StatusCode).Return(EXPECTED_CODE);

                var req = (WebRequest) WebRequest.Create("www.google.com");
                var resp = (WebResponse) req.GetResponse();
                var httpResp = (HttpWebResponse) resp;

                if (httpResp.StatusCode == EXPECTED_CODE)
                {
                    Debug.WriteLine("*************");
                    Debug.WriteLine("Works!!!!!!!!");
                    Debug.WriteLine("*************");

                    return;
                }

                Assert.IsTrue(false, "WTF?! it works in my computer...");
            }
        }


        [TestMethod]
        public void WarpTheCreateMethod()
        {
            const HttpStatusCode EXPECTED_CODE = HttpStatusCode.Created;
            const string REQUEST_URI_STRING = "www.google.com";

            var fakeFactory = MockRepository.GenerateStub<IWebRequestFactory>();
            var fakeRequest = MockRepository.GenerateStub<WebRequest>();
            var fakeResponce = MockRepository.GenerateStub<HttpWebResponse>();


            fakeFactory.Stub(x => x.Create(REQUEST_URI_STRING)).Return(fakeRequest);
            fakeRequest.Stub(x => x.GetResponse()).Return(fakeResponce);
            fakeResponce.Stub(x => x.StatusCode).Return(EXPECTED_CODE);

            var req = fakeFactory.Create(REQUEST_URI_STRING);
            var resp = (WebResponse) req.GetResponse();
            var httpResp = (HttpWebResponse) resp;

            if (httpResp.StatusCode == EXPECTED_CODE)
            {
                Debug.WriteLine("*************");
                Debug.WriteLine("Works!!!!!!!!");
                Debug.WriteLine("*************");

                return;
            }

            Assert.IsTrue(false, "WTF?! it works in my computer...");
        }

        [TestMethod]
        public void PartialMock()
        {
            var fake = MockRepository.GeneratePartialMock<Foo>();

            fake.PrintRe();

            fake.Stub(x => x.PrintRe()).Do(new Action(() => Debug.WriteLine("Override")));

            fake.PrintRe();


        }

    }


    public abstract class Foo
    {
        public virtual void PrintRe()
        {
            Debug.WriteLine("Wow it works");
        }

        public abstract void OverrideMe();
    }
}
