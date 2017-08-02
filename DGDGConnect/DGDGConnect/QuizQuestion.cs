using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DGDGConnect
{
    public class QuizQuestion
    {
        public int id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public string help { get; set; }
        public List<string> options { get; set; }
        public string validate { get; set; }
        public object answer { get; set; }
        public int weighting { get; set; }

        public QuizQuestion() {}
    }
}