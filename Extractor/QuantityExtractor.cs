using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text;
using Microsoft.ProgramSynthesis.Extraction.Text.Constraints;
using Microsoft.ProgramSynthesis.Learning;


namespace CargoMailParser.Extractor
{
    public static class QuantityExtractor 
    {
        public static void extract(List <string> inputlistString)
        {
            
            RegionSession RegionSession = new RegionSession();
            string regexQuantity = @"[\d*(\.|\,)?\d*]+\s?(tons|mts|mt|ton|kg|kgs|dwt|mtons)";

            string [] records = {"- QUANTITY : 25,000 MTS (BAGS 25KGS)",//0
                "15,000 mts +/- 10 % CAN (Axan)",//1
                "16,000 mts +/- 10 % CAN-27",//2
                "16,000 mts CAN-27",//3
                "5,100 mts min/max Nitrabor",//4
                "5,100 mts Nitrabor",//5
                "6.5 tons",//6
                "7 tons",//7
                "- Qty : 15,000MT (+/-5% MOLCO)",//8
                "7.5 tons",
                "   6,600 mts +/- 10 % moloo NPK (19-04-19)",
                "   6,600 mts NPK",
                "   7,000 mts CAN (Axan)",
                "   8,000 mts CAN (Axan)",
                "  5000  TONS OPTION 4000 OPTION  2000 TONS DJIBOUTI",
                "  9000 TONS MOMBASA",
                "(2000mt for kwangyang, 3000mt for ulsan)",
                "- 2500 TONS LOAD",
                
                "- Quantity: 20,00mt - 2 pct moloo calcined petcoke",
                "- Q’ty: 2 lots x 41,000MT +/- 10% MOLOO",
                "- Unit Weight 30.5 Tons.",
                "-Q’TTY : ABT. 4,000 – 5,000MT",
                "10 m - UW 22 tons",
                "150 x 100 x   20 cm  = uw  150 kg  (x5)",
                "18.5 tons",
                "19 tons",
                "20,000 - 35,000 DWT .",
                "25.000 tons metcoke",
                "3.500 TONS WOODPULP",
                "30.000tons",
                "33,500 MT ON 10.65M SSW",
                "4.4 tons",
                "40-50,000 dwt",
                "40.000tons",
                "41,000 MTONS",
                "44/4570 mts profiles",
                "4500/5K TONS CORN",
                "5 HO / HA / TPC 44.5 MT",
                "5 tons",
                "50.000tons",
                "5000mtons wric",
                "570 x 130 x 130 cm =  3 mt",
                "5K TONS CORN",
                "6 tons",
                "7000MTS, (basis 4cr x 30mts  min) / 12000MTS.",
                "as p/c fr grd 25ts tng",
                "Cargo quantity : 10.000 tons.",
                "CARGO&amp;QTTY: ABT50,000MT COAL IN BULK 10PCT MOLCO",
                "DWCC: 2000-7000 ts",
                "grd sid min 4 x 25 ts cranes",
                "Max 20 years / min 25mt cranes",
                "Max. 28 M.Tons.",
                "No. 1     5,00 m    x    4,05 m    x    3,95 m  =     8.645 kg",
                "No. 2     6,30 m    x    4,70 m    x    3,16 m   =   11.995 kg",
                "one monthly shipment of 40.000 mt 5%moloo",
                "penang(4k mt) +port kelang (24k mt) to luoyuan",
                "QTY",
                "Qty :20000 MT",
                "QTY        : 42000 MTS +- 10% MOL CHARTER OPTION",
                "Qty (mt)",
                "qty - 30,000 mt +/- 2% moloo metalurgical calcined alumina powder in blk",
                "Qty 20 – max 40k Rockphosphate (as sole or part cargo)",
                "Qty 30K min max - BF Coke , (SF is 65 wog)",
                "QTY 5,500 CBM 10% MOLCO (NEED HOLD CAPA MIN 14,000 CBM)",
                "QTY :  20,000 MT +/- 10%",
                "QTY :  60,000 MT +/- 5%",
                "QTY :  MIN 27,000 MT - MAX 30,000 MT",
                "qty : 30000 +- 10% molco",
                "Qty : 50,000 MT +/- 10%",
                "Qty : 55000 mt +\\-10",
                "Qty above 25k - 40k for 2 port discharge Hazira + Kandla",
                "Qty break up - 25,000 mt at Pipavav and 30,000 mt at Porbandar",
                "Qty is 6,000GMT & needs 23,000CBM & above!",
                "Qty is 7,000GMT (est. 5,200 to 5,300DBMT) & needs 25,000CBM & above!",
                "Qty upto 25k for 1 port discharge Hazira",
                "Qty: 10,000BDMT +/-10% MOLOO (S/f: 5.4)",
                "Qty: 20000MT",
                "Qty: 26000  TO 33000  TONS at owners option",
                "Qty: 30 kt and 15kt ±10% Moloo",
                "Qty: 35,000mt",
                "Qty: 35,000mt=0A=",
                "QTY: 40,000-45000 MT LIMESTONE",
                "qty: 9,000 mt",
                "Qty: Firm Quantity-1X (45,000-60,000) MT MOLOO",
                "Qty:35,000mt",
                "Qty:35,000mt=0A=",
                "QTYS : 25,000 ~ 30,000 MT / SHIPMENT (IN BULK OR 1,000KG BIG BAG)",
                "Quantity",
                "Quantity : 7,000mt â€“ 8,000mt (inchopt)",
                "Quantity :50-55,000MT in Bulk ( need geared vsl )",
                "quantity : 25000 mt (+/- 5% charter option ) , other details ",
                "QUANTITY : 30 – 40,000 M/TONS (ABOUT) TO BE FINALIZED",
                "quantity       : 25,000 mt +/-10% moloo sand",
                "Quantity   : 10,000 OR 15,000 Mts",
                "QUANTITY   : 25000 MT (+/- 5% Charter Option ) , OTHER DETAILS INCLUDING",
                "Quantity  30,000 mt +/-5%",
                "Quantity (MT)",
                "Quantity - 10,000mt +/-10% MOLOO",
                "Quantity - 100,000mt +/-10% MOLOO",
                "Quantity - 17,000 BDMT+/-10% MOLOO",
                "Quantity - 45,000mt +/-10% MOLOO",
                "Quantity - 5,000 MT +/-10% MOLOO",
                "Quantity - 55,000mt +/-10% MOLOO",
                "Quantity 12000 mt +/- 10 Pct choption",
                "quantity : (250.000 )mt .",
                "Quantity : 10,000 mt, Or 11,000 mt, Or 15,000 Mts, All in Choptn",
                "Quantity : 15000mt+/-5% molcopt,  Steel scrap for melting in bulk, SF 2.0 abt wog.",
                "Quantity : 200,000 Tons",
                "Quantity : 200,000 Tons of Aggregate",
                "quantity : 40000 +/-10% rock phosphate",
                "Quantity : 7000-8,000mt 2% MOLCO",
                "Quantity : About 10,000 mt, Or 11,000 mt, Or 15,000 Mts, All in Choptn ",
                "Quantity : About 11,000 mt, Or 15,000 Mts, in Choptn",
                "QUANTITY : LOADABLE QTY 50,000 MT + 10%  (supramax or ultramax only)",
                "QUANTITY AS DETERMINED BY THE DRAFT SURVEY WITH THE FLWG REMARKS ONLY:",
                "QUANTITY INCREASED",
                "Quantity is 42\'500 - 52\'000 MT in Charterer\'s option.",
                "quantity requested, if greater.",
                "QUANTITY TO BE LOADED AT STOCKTON AND LB IS CHOPT.",
                "Quantity:",
                "Quantity:                     40,000 MT +/- 10%  Moloo",
                "Quantity:                     50,000 MT +/- 10%  Moloo",
                "Quantity:     abt 27,000mt Bulk Harmless Fertilizers",
                "Quantity:     abt 28,000mt Bulk Harmless Fertilizers.",
                "QUANTITY: 1,000 MTS , 5 MTS / COIL",
                "Quantity: 1,641 pipes (97 bends) / 5,050 mt / 5,522 cbm 5% molchopt",
                "Quantity: 1,656 pipes (114 bends) / 5,069 mt / 5,654 cbm 5% molchopt",
                "Quantity: 20,000-40,000mt BHF",
                "Quantity: 20,000mts (+/-10%)",
                "Quantity: 21.000mt 10% more or less",
                "Quantity: 25,000 mts 10pct moloo",
                "QUANTITY: 25,000 MTS MIN/MAX",
                "Quantity: 25,000 MTS Min/Max (IDEALLY)",
                "Quantity: 25.000 MT",
                "Quantity: 25000 MT in Bulk",
                "Quantity: 25000-27500 MTs in Bulk",
                "quantity: 30,000 mt +/- 10%  chopt",
                "Quantity: 30000mt (ready in the port)",
                "Quantity: 30000mt (ready in the port)=0A=",
                "quantity: 35,000mt 10% moloo",
                "Quantity: 40,000 – 45,000 GMT",
                "Quantity: 50,000MT/10% owners option Thermal Coal",
                "Quantity: 55,000MT/10% owners option",
                "Quantity: 58,000MT/10% owners option",
                "Quantity: 8000 Mton  (+/ - 5 % molco)",
                "QUANTITY: 9,300MT, TRY LESS IF NEEDED",
                "QUANTITY: MIN 11,000MT +5% CHOPT",
                "Quantity: Min 25.000 - Max 30.000 MT in Charterers option",
                "quantity: min 30000 mt + 10pct  oo",
                "QUANTITY: MIN 41000/ MAX 44000 MT IN OO",
                "Quantity: Total 24000mt 10% molchopt Steel scrap",
                "TOTAL QUANTITY: ABOUT MIN 150,000 MTS (6 SHIPMENTS) - 350,000MTS +/- 10% PER YEAR IN CHOPT.",
                "TTL QUANTITY 250.000 MTS",
                "TTL W  900 MTONS",
                "TTL W  900MTONS",
                "VPT- 8000 MT/day",
                "Weight : 1.578 tons/bag",
                "Weight_1 : 41000",
                "Weight_2 : 41000"};
        
                StringRegion inputRegion = RegionSession.CreateStringRegion(records[0]);
                StringRegion inputRegion2 = RegionSession.CreateStringRegion(records[1]);
                StringRegion inputRegion3 = RegionSession.CreateStringRegion(records[2]);
                StringRegion inputRegion4 = RegionSession.CreateStringRegion(records[3]);
                StringRegion inputRegion5 = RegionSession.CreateStringRegion(records[4]);
                StringRegion inputRegion6 = RegionSession.CreateStringRegion(records[5]);
                StringRegion inputRegion7 = RegionSession.CreateStringRegion(records[6]);
                StringRegion inputRegion8 = RegionSession.CreateStringRegion(records[7]);
                StringRegion inputRegion9 = RegionSession.CreateStringRegion(records[8]);
                StringRegion inputRegion10 = RegionSession.CreateStringRegion(records[9]);
                Console.WriteLine(inputRegion.Slice(13, 23));
                Console.WriteLine(inputRegion2.Slice(0, 11));
                Console.WriteLine(inputRegion3.Slice(0, 11));
                IEnumerable<Match> matches = Regex.Matches(@"- QUANTITY : 25,000 MTS (BAGS 25KGS)",regexQuantity,RegexOptions.IgnoreCase);
                foreach (var match in matches)
                {
                    Console.WriteLine(match.Value);
                }
                var examples =new[]{ 
                   new RegionExample(inputRegion,inputRegion.Slice(14, 23)),
                   new RegionExample(inputRegion2,inputRegion2.Slice(1, 11)),
                   new RegionExample(inputRegion3,inputRegion3.Slice(1, 11)),
                };
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion2, new[] {
                //                 inputRegion2.Slice(1, 6), // input => "25,000"
                //                 inputRegion2.Slice(8, 11), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion3, new[] {
                //                 inputRegion3.Slice(1, 6), // input => "25,000"
                //                 inputRegion3.Slice(8, 11), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion4, new[] {
                //                 inputRegion4.Slice(1, 6), // input => "25,000"
                //                 inputRegion4.Slice(8, 11), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion5, new[] {
                //                 inputRegion5.Slice(1, 5), // input => "25,000"
                //                 inputRegion5.Slice(7, 10), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion6, new[] {
                //                 inputRegion6.Slice(1, 5), // input => "25,000"
                //                 inputRegion6.Slice(7, 10), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion7, new[] {
                //                 inputRegion7.Slice(1, 3), // input => "25,000"
                //                 inputRegion7.Slice(5, 8), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion8, new[] {
                //                 inputRegion8.Slice(1, 1), // input => "25,000"
                //                 inputRegion8.Slice(3, 6), // input => "MTS"
                //             })
                // );
                // RegionSession.Constraints.Add(
                //     new RegionExample(inputRegion9, new[] {
                 
                //                 inputRegion9.Slice(9, 14), // input => "25,000"
                //                 inputRegion9.Slice(15, 16), // input => "MTS"
                //             })
                // );
                //IEnumerable<RegionProgram> topRankedPrograms = RegionLearner.Instance.LearnTopK(RegionExample, null, 1, regexQuantity);;

                // if ( RegionSession.Learn()!= null)
                // {
                //     Console.WriteLine(RegionSession.Learn());
                // }
               
                // List <StringRegion> inputlist =new List<StringRegion> ();
               
                // foreach (var input in inputlistString)
                // {
                //     inputlist.Add(RegionSession.CreateStringRegion(input));
                // }
                
                // foreach (var result in topRankedProg.Run(inputlist))
                // {
                //     foreach (var item in result)
                //     {
                //         Console.WriteLine(item);
                //     }
                // }
                IEnumerable<RegionProgram> topRankedPrograms = RegionLearner.Instance.LearnTopK(examples, 
                                                                                            RegionLearner.Instance.ScoreFeature,
                                                                                            1,
                                                                                            null,
                                                                                            default(ProgramSamplingStrategy),
                                                                                            null,
                                                                                            new Regex(regexQuantity)).TopPrograms;
                RegionProgram topRankedProg = topRankedPrograms.FirstOrDefault();
                if (topRankedProg == null)
                {
                    Console.Error.WriteLine("Error: Learning fails!");
                    return;
                }

                foreach (string input in inputlistString)
                {
                    StringRegion inputStringRegion = RegionSession.CreateStringRegion(input); 
                    string output = topRankedProg.Run(inputStringRegion)?.Value ?? "null";
                    Console.WriteLine("\"{0}\" => \"{1}\"", inputStringRegion, output);
                }
                return;

        }
    }
}
