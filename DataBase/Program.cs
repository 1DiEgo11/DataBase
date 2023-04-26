using System.Net.Sockets;
using System.Net;
using System.Text;
using Server_Client;
using Booking;


//чтение файла
static List<User> bd_user(FileStream stream)
{
    var bd = new List<User>();
    using (StreamReader fstream = new StreamReader(stream))
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
static void Write(List<User> list, FileStream stream)
{
    using (StreamWriter fstream = new StreamWriter(stream))
    {
        foreach (var us in list)
        {

            fstream.WriteLine($"{us.id} {us.login} {us.password} {us.isAdmin}");
        }
    }
}


//Ссылка на папку userов!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
string path = @"C:\Users\asus\source\repos\DB\DataBase\users.txt";
FileStream users_txt = new(path, FileMode.OpenOrCreate);



TcpListener server = new(IPAddress.Any, 7000);
server.Start();
List<User> list = bd_user(users_txt);
int c = 0;

string s = "";
while (true)
{
    int k = 0;
    TcpClient client = server.AcceptTcpClient();
    NetworkStream stream = client.GetStream();

    string request = ReceivingAndSending.Receiving(stream);
    c = int.Parse(request.Substring(0, 1));
    

    switch (c)
    {
        case 1:
            request = request.Substring(2);
            foreach(var us in list)
            {
                if (us.login == request)
                {
                    k = 1;
                    s = us.id.ToString() + "/" + us.password + "/" + us.isAdmin;
                }
            }
            if (k == 0) s = "not found";
            break;
        case 2:
            request = request.Substring(2);
            foreach (var us in list)
            {
                if (us.login == request)
                {
                    k = 1;
                    s = "used";
                }
            }
            if (k == 0) s = "not foud"; 
            break;
        case 4:
            request = request[2..];
            s = Bookings.Link_Table(request);
            break;
    }
    ReceivingAndSending.Sending(stream, s);
    Console.WriteLine("Отправил: " + s);
}

server.Stop();

public class User
{
    public string login = "";
    public string password = "";
    public int isAdmin;
    public int id = 0;
};

