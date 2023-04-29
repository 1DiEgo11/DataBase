using System.Net.Sockets;
using System.Net;
using System.Text;
using Server_Client;
using Booking;
using Users;

//чтение файла
static List<User> bd_user(FileStream stream)
{
    var bd = new List<User>();
    using (StreamReader fstream = new(stream))
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
static void Write(List<User> list, string path)
{
    using (StreamWriter fstream = new StreamWriter(path))
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
    Console.WriteLine("Принял: " + request);
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
            int id = 1;
            request = request.Substring(2);
            foreach (var us in list)
            {
                id = us.id + 1;
                if (us.login == request)
                {
                    k = 1;
                    s = "used";
                }
            }
            if (k == 0) s = "not found/" + id.ToString();
            break;
        case 3:
            request = request.Substring(2);
            User user = new User();
            foreach (char i in request)
            {
                if (i != '/')
                {
                    user.id = user.id * 10 + int.Parse(request.Substring(0, 1));
                    request = request.Substring(1);
                }
                else
                {
                    request = request.Substring(1);
                    break;
                }
            }
            foreach (char i in request)
            {
                if (i != '/')
                {
                    user.login += i;
                    request = request.Substring(1);
                }
                else
                {
                    user.password = request.Substring(1);
                    user.isAdmin = 0;
                    break;
                }
            }
            list.Add(user);
            Write(list, path);
            break;
        case 4:
            request = request[2..];
            s = Bookings.Get_Link_Table(request, list);
            break;
        case 5:
            string str1 = request[2..];
            Console.WriteLine(str1);
            Bookings.Set_Link_Table(str1);
            break;
        case 6:
            s = "";
            string s1 = request[2..];
            string[] s2 = s1.Split(new char[] {' ', '/', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            Bookings.Del_Booking(s2[0], s2[1] + " " + s2[2] + " " + s2[3] + " " + s2[4], list);
            break;
    }
    ReceivingAndSending.Sending(stream, s);
    Console.ForegroundColor = ConsoleColor.Black;
    Console.BackgroundColor = ConsoleColor.Yellow;
    Console.WriteLine("Отправил: " + s);
}

server.Stop();



