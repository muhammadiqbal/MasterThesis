using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Extraction.Text;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Extraction.Text.Constraints;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace CargoMailParser
{
    public static class QuantityExtractor 
    {
        public static List<StringRegion> extract(List <StringRegion> inputlist)
        {
            
            SequenceSession SequenceSession = new SequenceSession();
            Regex regexQuantity = new Regex("^(\\d*\\.)?\\d+\\s?+\\w((?i)tons|mt|ton|kg|kgs|dwt|mts|mtons(?-i))$");
            

            string [] records = {"- QUANTITY : 25,000 MTS (BAGS 25KGS)",
                "   15,000 mts +/- 10 % CAN (Axan)",
                "   16,000 mts +/- 10 % CAN-27",
                "   16,000 mts CAN-27",
                "   5,100 mts min/max Nitrabor",
                "   5,100 mts Nitrabor",
                "   6,600 mts +/- 10 % moloo NPK (19-04-19)",
                "   6,600 mts NPK",
                "   7,000 mts CAN (Axan)",
                "   8,000 mts CAN (Axan)",
                "  5000  TONS OPTION 4000 OPTION  2000 TONS DJIBOUTI",
                "  9000 TONS MOMBASA",
                "(2000mt for kwangyang, 3000mt for ulsan)",
                "- 2500 TONS LOAD",
                "- Qty : 15,000MT (+/-5% MOLCO)",
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
                "6.5 tons",
                "7 tons",
                "7.5 tons",
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
        
                StringRegion inputRegion = SequenceSession.CreateStringRegion(records[0]);
                SequenceSession.Constraints.Add(
                    new SequenceExample(inputRegion, new[] {
                                inputRegion.Slice(13, 18), // input => "25,000"
                                inputRegion.Slice(19, 22), // input => "MTS"
                            })
                );
                SequenceProgram topRankedProg = SequenceSession.Learn();

                List <StringRegion> results = new List<StringRegion>();
                foreach (var result in topRankedProg.Run(inputlist))
                {
                    results.Add(result);
                }
                 return results;

        }
    }
}
