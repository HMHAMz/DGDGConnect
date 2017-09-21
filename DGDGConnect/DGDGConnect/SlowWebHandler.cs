using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace DGDGConnect
{

    /* The Following HTTP GET/POST methodology is a adaptation of the code provided by James Manning on May 12th 2012 here:
     * https://stackoverflow.com/questions/10565090/getting-the-response-of-a-asynchronous-httpwebrequest
     * */

    class SlowWebHandler
    {
        /* Method: Get Response
         * Accepts the URI, data type and HTTP request method
         * Returns the web response in string format.           */
        public String GetResponse(String webLocation, String type, String method = "GET")
        {
            //Build a web query object, which contains the URI and other HTTP request info
            WebQueryObj queryObj = new WebQueryObj();
            queryObj.url = webLocation;
            queryObj.method = method;

            queryObj.url = queryObj.url.Replace("+", "%2B");

            /*Make the web request - this could be done more effectively by calling outside of Get Response 
             * (to utilize the Async methodology) */
            var task = MakeAsyncRequest(queryObj, type);
            //Get the response value
            String getValue = task.Result;

            return getValue;
        }

        //Debug method used for testing
        public String GetHTMLResponse(WebQueryObj _wr)
        {
            var task = MakeAsyncRequest(_wr, "text/html");

            String getValue = task.Result;

            return getValue;
        }

        //Makes an Asyncronous web request based on passed parameters 
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