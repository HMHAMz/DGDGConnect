using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGDGConnect
{
    class WebMethod
    {
       /* Class:        WebMethod
       *  Programmer:   Harry Martin
       *  Dependencies: LoadResourceText
       *  Description:  This class holds the public web-query methods
       *                designed to simplify the HTTP GET/POST requests 
       */
        //global vars
        static String URL = "http://introtoapps.com/datastore.php?";
        static String APPID = "appid=216022543";

        //Readability helpers
        static String AND = "&";
        static String OBJID = "objectid=";

        //Action types
        static String save_action = "action=save";
        static String load_action = "action=load";
        static String list_action = "action=list";

        public static String Serialize(object s_object)
        {
            //Serialize the passed object into Json
            String enc_json = JsonConvert.SerializeObject(s_object);

            return enc_json;
        }

        public static String GetResult(String p_action, String p_objectid, String p_serialized_json,  String p_http_method = "GET")
        {
            String postResult;
            String URI;

            // The following switch allows the programmer to define an action in lamence terms and still function
            switch (p_action)
            {
                case ("save"):
                    p_action = save_action;
                    break;
                case ("load"):
                    p_action = load_action;
                    break;
                case ("list"):
                    p_action = list_action;
                    break;
                default:
                    break;
            }

            //Define the full URI 
            if (p_serialized_json == "")
            {
                URI = URL + p_action + AND + APPID + AND + OBJID + p_objectid;
            } else
            {
                URI = URL + p_action + AND + APPID + AND + OBJID + p_objectid + AND + "data=" + p_serialized_json;
            }

            //Do the HTTP POST request and get the result
            try
            {
                //Attempt to do the HTTP request
                postResult = LoadResourceText.GetNetwork(URI, p_http_method);
            }
            catch (Exception ex)
            {
                //Request failed (likely bad parameters)
                postResult = "Web request failed.";
            }
            return postResult;
        }

    }
}
