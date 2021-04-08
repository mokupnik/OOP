using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;


namespace Lista8
{


    public interface ICommand {


        void Run ();


    }

    public class FTP
    {
        public void Execute(string addres, string fileName)
            {// Get the object used to communicate with the server.;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(addres);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            request.Credentials = new NetworkCredential("Maciej","299760@uwr.edu.pl");

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            
            
            
            using (FileStream f = File.OpenWrite(fileName)) {
                responseStream.CopyTo(f);
            }

            
            }

    }



    public class CFTP : ICommand {

        string addres;
        string fileName;

        FTP ftp;

        public CFTP(string addres, string fileName)
        {
            this.ftp = new FTP();
            this.addres = addres;
            this.fileName = fileName;
            
        }

        public void Run () {

            this.ftp.Execute(addres,fileName);

        }



    }



public class HTTP : ICommand {

// Create a new WebClient instance.
public WebClient myWebClient;
public string fileName;
public string remoteUri;

public HTTP(string fileName, string remoteUri)
{
    this.fileName = fileName;
    this.remoteUri = remoteUri;
    this.myWebClient = new WebClient();
}


public void Run()
{
    
    myWebClient.DownloadFile(remoteUri,fileName);
   



}

    }

public class RandomTextGenerator {

 private int length;

 public string randomText;
   public RandomTextGenerator ()
   {
      
      // creating a StringBuilder object()
      StringBuilder str_build = new StringBuilder();  
      Random random = new Random(); 
    
        char letter;

      this.length = random.Next(100);

      for (int i = 0; i < length; i++)
      {
        double flt = random.NextDouble();
        int shift = Convert.ToInt32(Math.Floor(25 * flt));
        letter = Convert.ToChar(shift + 65);
        str_build.Append(letter);  

    }
    this.randomText = str_build.ToString();


}
}
public class RandomFile : ICommand {

    string fileName;


    public RandomFile(string fileName)
    {

        this.fileName = fileName;

    } 

    public void Run()
    {   RandomTextGenerator randomText = new RandomTextGenerator();
        File.WriteAllText(fileName, randomText.randomText);
    }

}
public class CopyFile : ICommand {

    string from;
    string to;


    public CopyFile(string from, string to)
    {

        this.from = from;
        this.to = to;

    } 

    public void Run()
    {   File.Copy(from, to);

    }

}

    public class Invoker{
    public ConcurrentQueue <ICommand> que = new ConcurrentQueue<ICommand>();
    public List<ICommand> coms;


    public void Add_To_Que()
    {

        foreach(ICommand com in coms)
        {


            que.Enqueue(com);

        }


    }

    public void Take_from_Que(){
       while(true)
       {
        while(que.Count > 0 || que != null) 
        {

            ICommand com = null;
            
            que.TryDequeue(out com);
            if(com != null)
            {
            com.Run();
            }
            Thread.Sleep(5000);

        }
       }
    

     
    } 

    public Invoker(List<ICommand> coms)
    {
        this.coms = coms;
        Thread add_to = new Thread(Add_To_Que);
        Thread Take_from_1 = new Thread(Take_from_Que);
        Thread Take_from_2 = new Thread(Take_from_Que);
        add_to.Start();
        Take_from_1.Start();
        Take_from_2.Start();
        


    }

    
    }



    
        class Program
    {
        static void Main(string[] args)
        {
            RandomFile ran = new RandomFile("test.txt");
            CopyFile cop = new CopyFile("testcopy.txt","copy_test.txt");
            HTTP htp = new HTTP("IIUwr","http://www.ii.uni.wroc.pl/~wzychla/ra2J2K/sem2.html");
            List<ICommand> comms = new List<ICommand>();
            
            comms.Add(ran);
            comms.Add(htp);
            cop.Run();
            Invoker inv = new Invoker(comms);


        }
    }
}
