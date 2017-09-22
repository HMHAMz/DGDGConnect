using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace DGDGConnect
{
    public static class LoadResourceText
    {
        public static String GetLocal(String location)
        {
            /* Method: GetLocal
            * Programmer: Harry Martin
            * Description: This method will attempt to load the json file from the local resources
            The stream reader will convert that stream into a string variable */
            String value;
            Stream stream;
            var assembly = typeof(JsonParser).GetTypeInfo().Assembly;
            try
            {
                stream = assembly.GetManifestResourceStream(location);
                value = "";
                using (var reader = new System.IO.StreamReader(stream))
                {
                    value = reader.ReadToEnd();
                }

                return value;
            }
            catch (FileNotFoundException ex)
            {
                return "File not found.";
            }
            catch (Exception ex)
            {
                return "Unknown exception.";
            }
        }

        public static String GetNetwork(String url, String method)
        {
            /* Method: GetNetwork
            * Programmer: Harry Martin
            * Description: This method will attempt to load the json file from the network url
            The stream reader will convert that stream into a string variable */
            SlowWebHandler WebHandler = new SlowWebHandler();
            String value = WebHandler.GetResponse(url, "application/json; charset=utf-8", method);

            return value;
        }
    }
}
