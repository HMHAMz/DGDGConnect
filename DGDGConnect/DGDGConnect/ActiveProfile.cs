using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DGDGConnect
{
    public sealed class ActiveProfile
    {
        private static ActiveProfile instance = null;
        private static readonly object padlock = new object();

        String username;
        String password;

        ActiveProfile()
        {
            username = "empty";
            password = "empty";
        }

        public String GetName()
        {
            return username;
        }

        public void SetName(String _n)
        {
            username = _n;
        }


        public static ActiveProfile Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ActiveProfile();
                    }
                    return instance;
                }
            }
        }
    }
}
