using FilesAndConvertation;
using System;
using System.Text;
using Users;

namespace Booking
{
    public class Bookings
    {
        public static string Get_Link_Table(string com, List<User> list)
        {
            string[] settings = com.Split(new char[] {'/', ' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            string id = settings[0];
            string IsAdmin = settings[1];
            string all = settings[2];

            string path = @"C:\Users\asus\source\repos\DB\DataBase\Link.txt";
            FileStream Link_txt = new(path, FileMode.OpenOrCreate);
            
            
            var swit = new Switches();
            int k = 0;
            string s;
            string send = "";
            string[] sp;
            using (StreamReader file = new(Link_txt))
            {
                while(true)
                {
                    s = file.ReadLine();
                    if (s == null) break;
                    if (IsAdmin == "0" || all == "0")
                    {
                        if (s[..1] == id)
                        {
                            k = 1;
                            send += s[2..] + "/";
                        }
                        else continue;
                    }
                    else if( IsAdmin == "1" && all == "1") 
                    {
                        foreach (var us in list)
                        {
                            if (us.id == int.Parse(s[..1]))
                            {
                                k = 1;
                                send += us.login + "/";
                                send += swit.Convert_in_com(s[2..]) + "/";
                            }
                        }   
                    }
                }
                file.Close();
                Link_txt.Close();
            }
            
            send = send.Replace(' ', '/');

            if (IsAdmin == "0" || all == "0") { send = swit.Convert_in_com(send); };
            if (k == 0) { send = " "; };
            return send;
        }
        public static void Set_Link_Table(string command) 
        {
            command = command.Replace('/', ' ');
            StreamWriter writer = new StreamWriter("C:\\Users\\asus\\source\\repos\\DB\\DataBase\\Link.txt", true);
            writer.WriteLine(command);
            writer.Close();
        }
        public static void Del_Booking(string id, string com, List<User> users)
        {
            
            string path = @"C:\Users\asus\source\repos\DB\DataBase\Link.txt";

            
            var swit = new Switches();
            com = swit.Convert_from_com(com);
            if(int.TryParse(id, out int n) == true)
            {
                var re = File.ReadAllLines(path, Encoding.Default).Where(s => s != id + ' ' + com);
                File.WriteAllLines(path, re, Encoding.Default);
            }
            else
            {
                foreach ( var user in users)
                {
                    if(user.login == id)
                    {
                        id = user.id.ToString();
                    }
                }
                var re = File.ReadAllLines(path, Encoding.Default).Where(s => s != id + ' ' + com);
                File.WriteAllLines(path, re, Encoding.Default);
            }
        }
    }
}