namespace Booking
{
    public class Bookings
    {
        public static string Link_Table(string id)
        {
            string path = @"C:\Users\asus\source\repos\DB\DataBase\Link.txt";
            FileStream Link_txt = new(path, FileMode.OpenOrCreate);

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
                        send += s[2..] + ' ';
                    }
                    else continue;

                }
            }
            return send.Replace(' ', '/');
        }
    }
}