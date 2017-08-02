using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace DGDGConnect
{
    class LoadResourceText
    {
        public LoadResourceText()
        {
            #region How to load a text file embedded resource
            var assembly = typeof(LoadResourceText).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("WorkingWithFiles.PCLTextResource.txt");

            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            #endregion
        }
    }
}
