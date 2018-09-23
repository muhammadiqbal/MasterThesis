using System;

namespace CargoMailParser
{
    public class Email
    {
        public Email(
            string emailID,                            
            string subject,                            
            string body,                               
            string sender,                             
            string receiver,                           
            string cc,                                 
            string classification_manual,              
            string date,                               
            string classification_automated,           
            string IMAPUID,                            
            string IMAPFolderID,                       
            string _created_on,                        
            string classification_automated_certainty                  
        ){
            this.emailID = Int32.Parse(emailID);                            
            this.subject = subject;                            
            this.Body = body;                               
            this.sender = sender;                             
            this.receiver = receiver;                           
            this.cc = cc;                                 
            this.classification_manual = classification_manual;              
            this.date = date;                               
            this.classification_automated = classification_automated;           
            this.IMAPUID = IMAPUID;                            
            this.IMAPFolderID = IMAPFolderID;                       
            this._created_on = _created_on;                        
            this.classification_automated_certainty = float.Parse(classification_automated_certainty);

        } 
        public int emailID{get; set;}                             
        public string subject{get; set;}                             
        
        public string Body{get; set;}                                
        public string sender{get; set;}                              
        public string receiver{get; set;}                            
        public string cc{get; set;}                                  
        public string classification_manual{get; set;}               
        public string date{get; set;}                                
        public string classification_automated{get; set;}            
        public string IMAPUID{get; set;}                             
        public string IMAPFolderID{get; set;}                        
        public string _created_on{get; set;}                         
        public float classification_automated_certainty{get; set;} 
    }
}
