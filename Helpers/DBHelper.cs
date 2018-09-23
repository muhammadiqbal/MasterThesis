using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;


namespace CargoMailParser{
    public static class DBHelper
    {
        public static List<Cargo> RetrieveAllCargos()
        {
            List<Cargo> cargos = new List<Cargo>();
            using (var connection = new MySqlConnection
            {
                ConnectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=true;port=3306;database=cargo;SslMode=none"
            }) {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM cargos ", connection);

                MySqlDataReader reader =  command.ExecuteReader();                
                while (reader.Read())
                {
                    
                    Cargo cargo = new Cargo(reader["id"].ToString(),
                            reader["cargo_type_id"].ToString(),
                            reader["quantity"].ToString(),
                            reader["loading_port"].ToString(),
                            reader["discharging_port"].ToString(),
                            reader["laycan_first_day"].ToString(),
                            reader["laycan_last_day"].ToString(),
                            reader["stowage_factor"].ToString(),
                            reader["sf_unit"].ToString(),
                            reader["loading_rate"].ToString(),
                            reader["loading_rate_type"].ToString(),
                            reader["discharging_rate"].ToString(),
                            reader["discharging_rate_type"].ToString(),
                            reader["commission"].ToString());
                    cargos.Add(cargo);                        
                }                
            }       
            return cargos;
        }

        public static List<Email> RetrieveAllEmails(int limit)
        {
            List<Email> emails = new List<Email>();
            using (var connection = new MySqlConnection
            {
                ConnectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=true;port=3306;database=dev_dbpsbulkcargo;SslMode=none"
            }) {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM email WHERE classification_automated = \"Cargo\" LIMIT "+limit+";", connection);

                MySqlDataReader reader =  command.ExecuteReader();
                while (reader.Read())
                {
                    Email email = new Email(
                            reader["emailID"].ToString(),                            
                            reader["subject"].ToString(),                            
                            reader["body"].ToString(),                               
                            reader["sender"].ToString(),                             
                            reader["receiver"].ToString(),                           
                            reader["cc"].ToString(),                                 
                            reader["classification_manual"].ToString(),              
                            reader["date"].ToString(),                               
                            reader["classification_automated"].ToString(),           
                            reader["IMAPUID"].ToString(),                            
                            reader["IMAPFolderID"].ToString(),                       
                            reader["_created_on"].ToString(),                        
                            reader["classification_automated_certainty"].ToString()                 
                    );
                    Console.WriteLine(reader["body"].ToString());
                    emails.Add(email);
                }
            }           
            return emails;
        }


        public static Email retrieveEmail(int emailID)
        {
            Email email = null;
            using (var connection = new MySqlConnection
            {
                ConnectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=true;port=3306;database=dev_dbpsbulkcargo;SslMode=none"
            }) {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM email WHERE  emailID = "+emailID+";", connection);

                MySqlDataReader reader =  command.ExecuteReader();
                reader.Read();
                email = new Email(
                            reader["emailID"].ToString(),                            
                            reader["subject"].ToString(),                            
                            reader["body"].ToString(),                               
                            reader["sender"].ToString(),                             
                            reader["receiver"].ToString(),                           
                            reader["cc"].ToString(),                                 
                            reader["classification_manual"].ToString(),              
                            reader["date"].ToString(),                               
                            reader["classification_automated"].ToString(),           
                            reader["IMAPUID"].ToString(),                            
                            reader["IMAPFolderID"].ToString(),                       
                            reader["_created_on"].ToString(),                        
                            reader["classification_automated_certainty"].ToString()                 
                    );
                    Console.WriteLine(reader["body"].ToString());
                    return email;
            }           
        }

        public static void InsertEmailLines(){
            IEnumerable<Email> emails = RetrieveAllEmails(10000);

            string connectionString = "server=localhost;user id=root;password=root;persistsecurityinfo=true;port=3306;database=email;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            foreach (var email in emails)
            {
                foreach (var line in email.Body.Split(new [] { '\r', '\n' }))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        try
                        {
                            var query = "INSERT INTO emailLines (line) values(@val)";
                            var command = new MySqlCommand(query, connection);
                            command.Parameters.AddWithValue("@val", line);
                            command.Prepare();
                            MySqlDataReader reader =  command.ExecuteReader();
                            reader.Close();
                        }
                        catch (System.Exception)
                        { 
                            throw;
                        }
                    }
                }
               
            }
            //connection.Close();
        }
    }
}
