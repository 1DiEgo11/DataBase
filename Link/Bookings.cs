using FilesAndConvertation;
using System;
using System.Text;

namespace Booking
{
    public class Bookings
    {
        public static string Get_Link_Table(string id)
        {
            string path = @"C:\Users\asus\source\repos\DB\DataBase\Link.txt";
            FileStream Link_txt = new(path, FileMode.OpenOrCreate);

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
                    if (s[..1] == id)
                    {
                        k = 1;
                        send += s[2..] + ' ';
                    }
                    else continue;
                }
                file.Close();
                Link_txt.Close();
            }
            
            send = send.Replace(' ', '/');
            var swit = new Switches();
            send = swit.Convert_in_com(send);
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
        public static void Del_Booking(string id, string com)
        {
            
            string path = @"C:\Users\asus\source\repos\DB\DataBase\Link.txt";
            //FileStream Link_txt = new(path, FileMode.OpenOrCreate);
            Console.WriteLine(com);
            var swit = new Switches();
            com = swit.Convert_from_com(com);
            Console.WriteLine("a");
            Console.WriteLine(com);

            var re = File.ReadAllLines(path, Encoding.Default).Where(s => s != id + ' ' + com );
            File.WriteAllLines(path, re, Encoding.Default);

            //string s;
            //using (StreamReader file0n = new(Link_txt))
            //{
            //    while (true)
            //    {
            //        s = file0n.ReadLine();
            //        if (s == null) break;
            //        if (s[..1] == id && s[2..] == com)
            //        {
            //            file0n.Close();
            //            Link_txt.Close();
            //            var re = File.ReadAllLines(path, Encoding.Default).Where(s => !s.Contains(com));
            //            File.WriteAllLines(path, re, Encoding.Default);
            //        }
            //        else continue;
            //    }
            //    file0n.Close();
            //    Link_txt.Close();
            //}
        }
    }
}