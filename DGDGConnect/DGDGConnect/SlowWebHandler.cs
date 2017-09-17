using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DGDGConnect
{

    /* The Following HTTP GET/POST methodology is a adaptation of the code provided by James Manning on May 12th 2012 here:
     * https://stackoverflow.com/questions/10565090/getting-the-response-of-a-asynchronous-httpwebrequest
     * */

    class SlowWebHandler
    {
        public String GetResponse(WebQueryObj _wr)
        {
            var task = MakeAsyncRequest(_wr, "text/html");

            String getValue = task.Result;

            return getValue;
            // Console.WriteLine("Got response of {0}", task.Result);
        }

        // Define other methods and classes here
        public static Task<string> MakeAsyncRequest(WebQueryObj _wr, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_wr.url);
            request.ContentType = contentType;
            request.Method = _wr.method;

            Task<WebResponse> task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                (object)null);

            return task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader sr = new StreamReader(responseStream))
            {
                //Need to return this response 
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }
    }
}