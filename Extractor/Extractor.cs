
using System.Text.RegularExpressions;

namespace CargoMailParser
{
    public abstract class Extractor
    {
        private string regexPattern;
        public Regex regex;
        
        public Extractor(){
           
        }
        public abstract string extract();
        public abstract Regex buildRegex();

    }
}