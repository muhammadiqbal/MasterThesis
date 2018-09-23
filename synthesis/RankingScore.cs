using System;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.AST;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Features;

namespace CargoMailParser
{
    public class cargoMailRankingscore : Feature<double>
    {
        public cargoMailRankingscore(Grammar grammar) : base(grammar, "cargoMailScore") {}

        protected override double GetFeatureValueForVariable(VariableNode variable) => 0;

        // Your ranking functions here
    }
}

