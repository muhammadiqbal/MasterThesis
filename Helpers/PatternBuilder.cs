using Microsoft.ProgramSynthesis.Matching.Text;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CargoMailParser
{
    public static class PatternBuilder
    {
        public static IEnumerable<Regex> GetPatternRegex(IEnumerable<string> inputs){
            Session session = new Session();

            session.Inputs.Add(inputs);
            IReadOnlyList <PatternInfo> patterns = session.LearnPatterns();
            List <Regex> Regex = new List<Regex>();
            foreach (var pattern in patterns)
            {
                Regex.Add(new Regex(pattern.Regex));
            }

            return Regex;
        }
    }
}