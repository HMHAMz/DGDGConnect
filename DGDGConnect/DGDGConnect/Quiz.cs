using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace DGDGConnect
{
    public class Quiz
    {
        /* Class:       Quiz
        *  Programmer:  Harry Martin
        *  Description: This class holds the Quiz data, including the array of Question objects
        */
        public string id { get; set; }
        public string title { get; set; }
        public List<QuizQuestion> questions { get; set; }
        public List<int> questionsPerPage { get; set; }
        public int score { get; set; }
    }

}