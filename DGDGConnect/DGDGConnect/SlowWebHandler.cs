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
     * https://stackoverflow.com/questions/10565090/getting-the-response-of-a-asynchronous-httpwebrequest */

    class SlowWebHandler
    {
        /* Class:       SlowWebHandler
         * Programmer:  Harry Martin
         * Dependencies:WebQueryObj
         * Description: This class holds the public Web Handler methods
         *              designed to complete the external-accessing functions of HTTP requests
         *              It is prefixed 'Slow' as the async requests are made internally and will halt the program
         */
        public String GetResponse(String webLocation, String type, String method = "GET")
        {
            /* Method:      GetResponse
            *  Description: Accepts the URL, data type and HTTP request method
            *               Returns the web response in string format.           
            */

            //Build a web query object, which contains the URI and other HTTP request info
            WebQueryObj queryObj = new WebQueryObj();
            queryObj.url = webLocation;
            queryObj.method = method;

            queryObj.url = queryObj.url.Replace("+", "%2B"); //http accepts '+' as a space, so we replace it with the required operators

            var task = MakeAsyncRequest(queryObj, type);//Make the web request

            String getValue = task.Result; //Get the response value

            return getValue;
        }

        
        public String GetHTMLResponse(WebQueryObj _wr) //Debug method used for testing
        {
            var task = MakeAsyncRequest(_wr, "text/html");
            String getValue = task.Result;
            return getValue;
        }

        //Makes an Asyncronous web request based on passed parameters 
        public static Task<string> MakeAsyncRequest(WebQueryObj _wr, string contentType)
        {
            /* Method:      MakeAsyncRequest
            *  Description: Accepts the fully formed WebQueryObj and the content type
            *               Completes the HTTP request and returns the response
            *  Source:      Adaptation of code provided by James Manning on May 12th 2012 here:
            *  https://stackoverflow.com/questions/10565090/getting-the-response-of-a-asynchronous-httpwebrequest
            */
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
            /* Method:      ReadStreamFromResponse
            *  Description: Reads the response stream and appends to form the return string
            *  Source:      Adaptation of code provided by James Manning on May 12th 2012 here:
            *  https://stackoverflow.com/questions/10565090/getting-the-response-of-a-asynchronous-httpwebrequest
            */
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