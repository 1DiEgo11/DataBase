using System.Net.Sockets;
using System.Net;
using System.Text;
using Server_Client;

//чтение файла
static List<User> bd()
{
    var bd = new List<User>();
    using (StreamReader fstream = new StreamReader("note2.txt"))
    {
        while (true)
        {
            User user = new User();
            string textFromFile = fstream.ReadLine();
            if (textFromFile == null) break;
            user.id = int.Parse(textFromFile.Substring(0, 1));
            textFromFile = textFromFile.Substring(2);
            foreach (char i in textFromFile)
            {
                if (i != ' ')
                {
                    user.login += i;
                    textFromFile = textFromFile.Substring(1);
                }
                else
                {
                    user.password = textFromFile.Substring(1, textFromFile.Length - 2);
                    user.isAdmin = int.Parse(textFromFile.Substring(textFromFile.Length - 1));

                    break;
                }
            }
            bd.Add(user);
        }
    }
    return bd;
}

////запись в файл
static void Write(List<User> list)
{
    using (StreamWriter fstream = new StreamWriter("note2.txt"))
    {
        foreach (var us in list)
        {

            fstream.WriteLine($"{us.id} {us.login} {us.password} {us.isAdmin}");
        }
    }
}



TcpListener server = new TcpListener(IPAddress.Any, 7000);
server.Start();


string s = "Привет!";
while (true)
{
    TcpClient client = server.AcceptTcpClient();
    NetworkStream stream = client.GetStream();

    ReceivingAndSending.Sending(stream, s);


    string request = ReceivingAndSending.Receiving(stream);
    Console.WriteLine("Got req: " + request);
}

server.Stop();

public class User
{
    public string login = "";
    public string password = "";
    public int isAdmin;
    public int id;
};

