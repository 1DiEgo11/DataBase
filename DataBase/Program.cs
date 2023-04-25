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
            foreach (char i in textFromFile)
            {
                if (i != ' ')
                {
                    user.id = user.id * 10 + int.Parse(textFromFile.Substring(0, 1));
                    textFromFile = textFromFile.Substring(1);
                }
                else
                {
                    textFromFile = textFromFile.Substring(1);
                    break;
                }
            }

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


static bool Reg(string log)
{
    List<User> list = bd();
    foreach (var us in list)
    {
        if (us.login == log) return false;
    }
    return true;
}

static string sing(string log)
{
    List<User> list = bd();
    foreach (var us in list)
    {
        if (us.login == log) return us.password;
    }
    return "неверный логин";
}

TcpListener server = new TcpListener(IPAddress.Any, 8080);
server.Start();
List<User> list = bd();
int c = 0;

string s = "Привет!";
while (true)
{
    int k = 0;
    TcpClient client = server.AcceptTcpClient();
    NetworkStream stream = client.GetStream();
    ReceivingAndSending.Sending(stream, s);
    string request = ReceivingAndSending.Receiving(stream);
    if (request == "Привет") continue;
    c = int.Parse(request.Substring(0, 1));
    request = request.Substring(2);
    foreach (var us in list)
    {
        if (us.login == request)
        {
            k = 1;
            switch (c)
            {
                case 1:
                    s = us.id.ToString() + "/" + us.password;
                    break;
                case 2:
                    s = "used";
                    break;
            }
        }
    }
    if (k == 0) s = "not found";
    Console.WriteLine("Got req: " + request);


}

server.Stop();

public class User
{
    public string login = "";
    public string password = "";
    public int isAdmin;
    public int id = 0;
};

