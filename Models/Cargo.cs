using System;

namespace CargoMailParser
{
    public class Cargo
    {
        public Cargo(){

        }
        public Cargo(string id, string cargo_type = null, string quantity = null, 
                        string loading_port = null, string discharging_port = null, string laycan_first_day=null,
                        string laycan_last_day = null, string stowage_factor = null, string stowage_factor_unit=null,
                        string loading_rate = null, string loading_rate_type = null, string discharging_rate = null,
                        string discharging_rate_type = null, string commission = null
                        ){
                            this.id = Int32.Parse(id);
                            this.cargo_type = cargo_type;
                            //this.quantity = float.Parse(quantity);
                            this.loading_port = loading_port;
                            this.discharging_port = discharging_port;
                            this.laycan_first_day = laycan_first_day;
                            this.laycan_last_day = laycan_last_day;
                            //this.stowage_factor = float.Parse(stowage_factor);
                            this.stowage_factor_unit = stowage_factor_unit;
                            //this.loading_rate = float.Parse(loading_rate);
                            this.loading_rate_type = loading_rate_type;
                            //this.discharging_rate = float.Parse(discharging_rate);
                            this.discharging_rate_type = discharging_rate_type;
                            //this.commission = float.Parse(commission);
        }

        private int id;
        private string cargo_type;
        private float quantity;
        private string loading_port;
        private string discharging_port;
        private string laycan_first_day;
        private string laycan_last_day;
        private float stowage_factor;
        private string stowage_factor_unit;
        private float loading_rate;
        private string loading_rate_type;
        private float discharging_rate;
        private string discharging_rate_type;
        private float commission;

        public int Id{get; set;}
        public string Cargo_type{get; set;}
        public float Quantity{get; set;}
        public string Loading_port{get; set;}
        public string Discharging_port{get; set;}
        public string Laycan_first_day{get; set;}
        public string Laycan_last_day{get; set;}
        public float Stowage_factor{get; set;}
        public string Stowage_factor_unit{get; set;}
        public float Loading_rate{get; set;}
        public float Discharging_rate{get; set;}
        public string Loading_rate_type{get; set;}
        public string Discharging_rate_type{get; set;}
        public float Commission{get; set;}
    }
}
