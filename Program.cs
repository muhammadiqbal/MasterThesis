using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Matching.Text;

namespace CargoMailParser
{
    class Program
    {
        static void Main(string[] args)
        {
            // List <Email> emails = DBHelper.RetrieveAllEmails(50);
            // //Console.WriteLine(email.Body);
            // Regex regex = new Regex("sample pattern");
            // foreach (var email in emails)
            // {
            //     string[] lines = email.Body.Split("\n");
                
            //     foreach (var line in lines)
            //     {
            //         Console.WriteLine(line);
            //         Console.WriteLine("=================================================");
            //         regex.Match(line);
            //     }

            //     Console.WriteLine("######################################\n");   
            // }
            List<string> entities = null; 
            Email email = DBHelper.retrieveEmail(50000);
            var emailLines = email.Body.Split(new [] { '\r', '\n' });
            // foreach (var emailLine in emailLines)
            // {
            //     string classification = CmdExecutor.run_cmd("python3 classifier.py "+emailLine);
            //     // switch (classification)
            //     // {
            //     //     case "Quantity": PatternQuantiy.Match();
            //     //     case "Description": PatternCargodescription.Match();
            //     //     case "Ld rate": PatternLdRate.Match();
            //     //     case "Laycan date": PatternLaycanDate.Match();
            //     //     case "Commision":PatternCommision.Match();
            //     //     case "Ports": PatternPort.Match();
            //     //     case "Ports Quantity": PatternPortQuantity.Match();
            //     //     default:
            //     // }
            //     entities.Add();
            // }
            //DBHelper.InsertEmailLines();
            MatchCollection result = null;
            IEnumerable <Regex> regexdescription =PatternCargodescription();
            foreach (var line in emailLines)
            {
                foreach (var regex in regexdescription)
                {
                    result = regex.Matches(line);
                    foreach (var item in result)
                    {
                        Console.WriteLine("match :"+item);
                    }
                }
            }           
            

            

        }

        
    static IEnumerable<Regex> PatternCargodescription(){
        IEnumerable<string> inputs = new []{

            "8/15,000 MT BULK MINERALS",
            "10,000 MT BULK MINERALS",
            "1 X  ABT  9,500 MT BULK MINERALS",
            "1 X  ABT 10,000 MT BULK MINERALS",
            "1 OR 2 LOTS OF 10,000 MT BULK MINERALS",
            "50000 mts bagged UREA 50kg bags",
            "1,700.mt 2% mol chopt harmless sodium sulph in bbgs stw abt 48\'",
            "1450/3000 mts barley, sf 52 wog",
            "Izm3000/10 wbp 54 wog",
            "5000 mt +/-5% of Anthracite coal SF 44",
            "5000 mt of Anthracite coal SF 44",
            "ABT 867T/1703CBM/282PCS, LOOSE PIPES",
            "abt 1800m3 / 208mts Project cargo",
            "2000 MTS BULK PERLITE / 2 GRADES",
            "GRADE A 1500 MTS EX ADAMAS",
            "GRADE B 500 MTS EX VOUDIA",
            "about  2,650.mt equal abt 3400cbm Chipboards in pallets",
            "3,765 tons STEELS",
            "44/4570 mts profiles",
            "6500 10% MOLOO TONS MARBLE CHIPS IN BULK",
            "5200 CBM MEDIUM-DENSITY FIBREBOARD (MDF) IN CRATES S.F. 3 CBM WOG STRICTLY UNDERDECK AND AND BALANCE 2000-3000 BUNDLES OF SAWN TIMBER PACKED IN BUNDLES SF 3 CBM WOG, NEED DECK OPTION - NEED ABOUT 6K-8K DWT TNG",
            "7,682/7,752 mts in chopt of blk mop stw abt 1m3/mt wog",
            "Cable Reels, 2 units.  LxWxH 9.2x5.9x10.2 mm / Weight: 40200 kg / per unit, plus EU pallet with accessories - 1 mts",
            "7/15000 mts of scrap sf abt 70 wog",
            "M/M 54/56000 slabs - qty in chopt",
            "01 Unit  155 MM T5 HOWITZER GUN mounted on a Vehicle",
            "(45,000-60,000) MT MOLOO",
            "M/M  5.450 / 5.750 mts in chopt coils",
            "3000 MTS CALCIT IN SB/ BB"
        };
        IEnumerable<Regex> regexDescription = PatternBuilder.GetPatternRegex(inputs);
        return regexDescription;
    }


    static IEnumerable<Regex> PatternLaycanDate(){
        IEnumerable<string> inputs = new []{
            "20-30 April",
            "29 March / 3 April",
            "l/c 25-26.03.17",
            "29.03 onw",
            "29-31.03.2017",
            "11-14.04.2017",
            "31 MARCH/06 APRIL 2017",
            "5/15 April",
            "5 / 9 APRIL",
            "4/6 Apri",
            "29/30  March",
            "30/31 March",
            "10/13th APRIL",
            "04 - 06/apr 2017",  
            "l/c 16-19 APR 2017",
            "1 april",
            "01/10 April 2017",
            "20’April’17 to 26’April’17",
            "02 – 03 April 2017",
            "01-05.04.17"
        };
        IEnumerable<Regex> regexLaycanDate = PatternBuilder.GetPatternRegex(inputs);
        return regexLaycanDate;
    }

    static IEnumerable<Regex> PatternCommision(){
        IEnumerable<string> inputs = new []{
            "3.75 PCT + 1,25 PCT WONSILD",
            "3.75 + 1,25 WONSILD",
            "5.0% iac",
            "Gnc 3.75% iac",
            "2.5% ttl comm here",
            "3.75% iac",
            "2.5% ttl comm here",
            "2.5% ttl comm here",
            "TTL COMM 3.75% IAC",
            "3.75% iac",
            "2.5% ttl comm here",
            "2.5% ttl comm here",
            "Gnc 3.75% IAC ",
            "2.5% TTL COMM HERE",
            "2.5% TTL COMM HERE",
            "5.0% iac",
            "gnc 2.5 % ttl comm here",
            "gcn 3.75% IAC",
            "2.5% TTL Comm HERE",
            "2.5% ttl comm",
            "3.75% IAC"
        };
        IEnumerable<Regex> regexCommision = PatternBuilder.GetPatternRegex(inputs);
        return regexCommision;
    }

    
    static IEnumerable<Regex> PatternLdRate(){
        IEnumerable<string> inputs = new []{
            "5,000 MT SHEXEIU / 6,000 MT SHINC",
            "5,000 MT SHEXEIU / 6,000 MT SHINC",
            "5,000 MT SHEXEIU / 6,000 MT SHINC",
            "4000 mts pwwd shexc bends - 12 hrs TT bends",
            "ttl 2 days SHINC",
            "1000/1000",
            "2000x/2000x",
            "2000x/2000x",
            "L 1350 MTS/WWD SSHEX",
            "D 1350 MTS/WWD SSHEX",
            "5 wwd l/d",
            "2000x/1500x",
            "3000 sshex /800 fshex",
            "2000 sshex /1000 sshex",
            "6 TTL WWDAYS",
            "Load rate: 12 days SSHEX EIU",
            "Dis rate: 12 days FHEX (incl Sat) EIU",
            "load rate: 3000 mt pwwd sshinc  ",
            "disch rate: 2500 mt sshex eiu",
            "l/d 12 hrs sshinc / 12 hrs sshinc  ",
            "4000fhex uu / 10000shinc",
            "7,000 MT/day",
            "VPT- 8000 MT/day",
            "GPL-12000 MT/day",
            "4.500 Sshinc / 2.750 Sshex-eiu ",
            "6 TTL WWDAYS SSHEX EIU"
        };
        IEnumerable<Regex> regexDescription = PatternBuilder.GetPatternRegex(inputs);
        return regexDescription;
    }

        static IEnumerable<Regex> PatternPort(){
        IEnumerable<string> inputs = new []{
            "1 SB MERSIN / 1 SP CHINA (INT. XINGANG)",
            "DURRES / 1SP CHINA (INT. XINGANG)",
            "1 SB MERSIN / 1-2 SP(S) CHINA",
            "Alexandria, Egypt -> Montevideo, Uruguay",
            "Zarzis -> Lattakia or Tartous",
            "Chornomorsk -> Odessa road OPL",
            "Kherson -> Tbs Mar",
            "Rostov AB -> Iskenderun",
            "Rostov AB -> Hereke",
            "MASAN, KOREA -> 1 PORT EGYPT, PREF. PORT SAID/ALEXANDRIA/DAMIETTA ",
            "Marmara Sea -> Houston",
            "2 MILOS -> 1 LE TREPORT FRANCE",
            "Aveiro, North Terminal ->  Sousse",
            "Nikolaev 10.3 -> Beirut, Lebanon ",
            "Barcelona /  Port Said",
            "Vilanova (close to Barcelona) -> Sousse",
            "2975/3110 mts profiles",
            "MARINA DI CARRARA -> GABES",
            "Penang and Bintulu, Malaysia -> Aden and Hodeidah, Yemen",
            "1 sb nikolayev -> 1 sb sibenik",
            "Klaipeda -> New Castle, UK ",
            "Misurata -> Iskenderun",
            "B.Abbas -> Prachuap",
            "Karachi -> Durban",
            "Isdemir -> Seville, Spain",
            "ISKENDERUN -> AZOV (EXPECTED OBUKHOVKA)"
        };
        IEnumerable<Regex> regexPort = PatternBuilder.GetPatternRegex(inputs);
        return regexPort; 
    }

    }
}
