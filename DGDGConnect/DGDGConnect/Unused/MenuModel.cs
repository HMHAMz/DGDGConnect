using System;

namespace DGDGConnect
{

    public class MenuModel
    {
        public string name { get; set; }
        public string comment { get; set; }
        public bool isABool { get; set; }
        public string image { get; set; }
        public string clickLink { get; set; }

        public MenuModel(string p_name, string p_comment, bool p_isABool, string p_image, string p_link)
        {
            name = p_name;
            comment = p_comment;
            isABool = p_isABool;
            image = p_image;
            clickLink = p_link;
        }
    }

    

}

