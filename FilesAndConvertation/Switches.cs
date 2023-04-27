namespace FilesAndConvertation
{
    public class Switches
    {
        List<string> date = new List<string>();
        List<string> time = new List<string>();
        List<string> restaurants = new List<string>();
        List<string> tables = new List<string>();

        const string FileName1 = "C:\\Users\\asus\\source\\repos\\DB\\DataBase\\Date.txt";
        const string FileName2 = "C:\\Users\\asus\\source\\repos\\DB\\DataBase\\Restaurants.txt";
        const string FileName3 = "C:\\Users\\asus\\source\\repos\\DB\\DataBase\\Table.txt";
        const string FileName4 = "C:\\Users\\asus\\source\\repos\\DB\\DataBase\\Time.txt";
        public static string[] Read(string file)
        {
            string path;
            path = Path.Combine(Environment.CurrentDirectory, file);
            string[] lines = File.ReadAllLines(path);
            return lines;
        }
        public List<string> Switch1()
        {
            string[] fileDate = Read(FileName1);
            string[] str0 = fileDate[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str1 = fileDate[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = fileDate[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            date.Add(str0[1]);

            date.Add(str1[1]);

            date.Add(str2[1]);
            return date;
        }
        public List<string> Switch2()
        {
            string[] fileDate = Read(FileName2);
            string[] str0 = fileDate[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str1 = fileDate[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = fileDate[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            restaurants.Add(str0[1]);

            restaurants.Add(str1[1]);

            restaurants.Add(str2[1]);
            return restaurants;
        }

        public List<string> Switch3()
        {
            string[] fileDate = Read(FileName3);
            string[] str0 = fileDate[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str1 = fileDate[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = fileDate[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            tables.Add(str0[1]);

            tables.Add(str1[1]);

            tables.Add(str2[1]);
            return tables;
        }
        public List<string> Switch4()
        {
            string[] fileDate = Read(FileName4);
            string[] str0 = fileDate[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str1 = fileDate[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str2 = fileDate[2].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str3 = fileDate[3].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str4 = fileDate[4].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] str5 = fileDate[5].Split(' ', StringSplitOptions.RemoveEmptyEntries);

            time.Add(str0[1]);
            time.Add(str1[1]);
            time.Add(str2[1]);
            time.Add(str3[1]);
            time.Add(str4[1]);
            time.Add(str5[1]);
            return time;
        }

        public string Convert_in_com(string str)
        {
            var restourants1 = Switch2();
            var date1 = Switch1();
            var time1 = Switch4();
            var table1 = Switch3();

            string otpravka = "";
            string[] zapros = str.Split(new char[] { '/', ' ', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < zapros.Length + 1; i += 4)
            { 
                otpravka += restourants1[int.Parse(zapros[i - 1]) - 1] + '/' + date1[int.Parse(zapros[i]) - 1] + '/' + time1[int.Parse(zapros[i + 1]) - 1] + '/' + table1[int.Parse(zapros[i + 2]) - 1] + '/';
            }
            return otpravka;
        }
        public string Convert_from_com(string str)
        {
            string[] In = str.Split(new char[] { '/', ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine(In[0] + ' ' + In[1] + ' ' + In[2] + ' ' + In[3]);
            var restourants1 = Switch2();
            var date1 = Switch1();
            var time1 = Switch4();
            var table1 = Switch3();
            string s = "";

            for (int i = 1; i < restourants1.Count + 1; i++)
            {
                if (restourants1[i - 1] == In[0])
                {
                    s += i + " ";
                }
            }
            for (int i = 1; i < date1.Count + 1; i++)
            {
                if (date1[i - 1] == In[1])
                {
                    s += i + " ";
                }
            }
            for (int i = 1; i < time1.Count + 1; i++)
            {
                if (time1[i - 1] == In[2])
                {
                    s += i + " ";
                }  
            }
            for (int i = 1; i < table1.Count + 1; i++)
            {
                if (table1[i - 1] == In[3])
                {
                    s += i;
                }
            }

            return s;
        }
    }
}