using System;
using Microsoft.ProgramSynthesis;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Specifications;
using System.Collections.Generic;

namespace CargoMailParser
{
    public class WitnessFunctions : DomainLearningLogic
    {
        public WitnessFunctions(Grammar grammar) : base(grammar) { }
          //how to ombine edit distance and regex?
            // Regex [] regexes = {
            //     //regex example (L\/D rate [-+]?[0-9]*\.?[0-9])
            //     //regex for matching quantity and cargo type
            //    new Regex(@"([-+]?[0-9]*\.?[0-9] (MT|mt|MTS|MTs|TONS) ([a-z]*|[A-Z]*))"),

            //     // look for keyword in the sentence and do the matching
            //     // keyword for loading discharing rate (load, loading, L/D, Discharging, Disch)
            //     //regex for matching loading/discharging rate
            //     new Regex(@"([-+]?[0-9]*\.?[0-9] ((FHEX EIU)|(FHEX UU)|(FSHEX EIU)|(FSHEX UU)|(SHEX EIU)|(SHEX UU)|(SHINC)|(SSHEX EIU)|(SSHEX UU)|(SSHINC)|(TFHEX EIU)|(TFHEX UU)))"),
                
            //     // look for keyword in the sentence and do the matching
            //     // keyword for stowage factor (sf, sf_units)
            //     //regex for matching stowage factor
            //      new Regex(@" ([-+]?[0-9]*\.?[0-9] (DWCC)|(FT/MT)|(M2/MT))"),

            //     //regex for matching laycan
            //      new Regex(@"(laycan)? \w?\s?([-+]?[0-9]*\.?[0-9] )"),
                
            //     //regex for matching commision
            //      new Regex(@"(Comm)? \w?\s?([-+]?[0-9]*\.?[0-9] %|PCT )"),
            // };
       // public cargoMailLearners(Grammar grammar) : base(grammar) {}

       // [WitnessFunction("RegexMatch", parameterIndex: 1)]
        // Your custom learning logic here (for example, witness functions)


    }
}

