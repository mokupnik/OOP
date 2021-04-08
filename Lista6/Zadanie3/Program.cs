using System;
using System.IO;
using System.Linq;
using System.Xml;

namespace Zadanie3 {


    public interface DataAccessHandlerStrategy{
         void Connect();
        void Download();
        void Process();
        void Close();

    }

    public  class DataAccessHandler {
        public DataAccessHandlerStrategy Istrategy;

        public DataAccessHandler(DataAccessHandlerStrategy Istrategy)
        {
            this.Istrategy = Istrategy;


        }

        public void Execute()
        {

            this.Istrategy.Connect();
            this.Istrategy.Download();
            this.Istrategy.Process();
            this.Istrategy.Close();

        }

    }

    public class SqlConnect {
        
       
        public SqlConnect (string connection)
        {



        }


          public void Close()
        {

            Console.WriteLine("The SQL connection has been terminated");

        }

    };
    public class SqlTask {
        public SqlConnect connect;
        public SqlTask (string task, SqlConnect connection)

        {   
            this.connect = connection;

        }
        public int Execute()
        {

            return 12;

        }
      
    
    };
// w visual studio code nie moglem znalezc rozszerzenia odpowiedzialnego za sql, wiec roboczo stworzylem dwie puste klasy pomocnicze

    public class DBHandler : DataAccessHandlerStrategy {
        private SqlConnect sqlC;
        public string sqlnameconnection;
        
        private int sum;
    
        public string cname;
        
        public string tname;

        public DBHandler(string cname, string tname, string sqlnameconnection)
        {
            this.sqlnameconnection = sqlnameconnection;
            this.cname = cname;
            this.tname = tname;

        }


        public void Connect() {
            this.sqlC = new SqlConnect(this.sqlnameconnection);

        }


        public void Download(){
            string task = "SELECT SUM(cname) FROM tname";
            SqlTask task_sql = new SqlTask(task, sqlC);
            this.sum = task_sql.Execute();
                

        }

        public  void Close(){

            this.sqlC.Close();


        }


        public  void Process(){
            Console.WriteLine("Suma columny {0} z tabeli {1} wynosi {2}", cname,tname,sum);



        }


      



    }

    public class XmlHandler : DataAccessHandlerStrategy {
        public string fileName;
        private FileStream file;
        private XmlDocument xml_d = null;

        public XmlHandler(string fileName) {
            this.fileName = fileName;
            this.xml_d = new XmlDocument();
        }

        public void Connect() {
            this.file = File.OpenRead(this.fileName);


        }

        public void Download() {
            this.xml_d.Load(this.file);   
        }


        private string Longest(XmlNodeList nodes)
        {
            string _longest = "";
            foreach(XmlNode node in nodes )
            {
                if(_longest.Length < node.Name.Length)
                {


                    _longest = node.Name;

                }

                


            }
        return _longest;



        }

        public  void Process() {
            string longest = Longest(this.xml_d.DocumentElement.ChildNodes);
            Console.WriteLine(longest);
        }   

        public  void Close() {

            this.file.Close();
            Console.WriteLine("The XML connection has been terminated!");        }
    }


   class Program{
        public static void Main() {
           var xml = new DataAccessHandler(new XmlHandler("test.xml"));
           var sql = new DataAccessHandler(new DBHandler("Pole","Kwadraty", "connection_ame"));
           xml.Execute();
           sql.Execute();

    }}
}