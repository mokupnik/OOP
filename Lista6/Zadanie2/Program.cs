using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Zadanie2 {

    public abstract class DataAccessHandler {
        public abstract void Connect();
        public abstract void Download();
        public abstract void Process();
        public abstract void Close();

        public void Execute() {
            this.Connect();
            this.Download();
            this.Process();
            this.Close();
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

    public class DBHandler : DataAccessHandler {
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


        public override void Connect() {
            this.sqlC = new SqlConnect(this.sqlnameconnection);

        }


        public override void Download(){
            string task = "SELECT SUM(cname) FROM tname";
            SqlTask task_sql = new SqlTask(task, sqlC);
            this.sum = task_sql.Execute();
                

        }

        public override void Close(){

            this.sqlC.Close();


        }


        public override void Process(){
            Console.WriteLine("Suma columny {0} z tabeli {1} wynosi {2}", cname,tname,sum);



        }


      



    }

    public class XmlHandler : DataAccessHandler {
        public string fileName;
        private FileStream file;
        private XmlDocument xml_d = null;

        public XmlHandler(string fileName) {
            this.fileName = fileName;
            this.xml_d = new XmlDocument();
        }

        public override void Connect() {
            this.file = File.OpenRead(this.fileName);


        }

        public override void Download() {
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

                var _c_longest = Longest(node.ChildNodes);
            }
        return _longest;



        }

        public override void Process() {
            string longest = Longest(this.xml_d.DocumentElement.ChildNodes);
            Console.WriteLine(longest);
        }   

        public override void Close() {

            this.file.Close();
            Console.WriteLine("The XML connection has been terminated!");        }
    }


   class Program{
        public static void Main() {
            var xml = new XmlHandler("test.xml");
            xml.Execute();
            var sql = new DBHandler("Pole","Kwadraty","polaczenie");
            sql.Execute();
        }
    }
}