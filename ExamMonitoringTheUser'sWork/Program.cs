using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ExamMonitoringTheUser_sWork
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var kh = new KeyboardHook(true);       
            kh.KeyDown += Kh_KeyDown;
            Application.Run();
        }
       static Dictionary<Keys, int> Dkey = new Dictionary<Keys, int>();
        private static void Kh_KeyDown(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            if (Dkey.ContainsKey(key))
            {
                Dkey[key]++;
            }
            else
            {
                Dkey[key] = 1;
            }
            var mylist = Dkey.OrderByDescending(d => d.Value);
            string bannedWordsFile = Path.Combine(@"D:\Destination\", "bannedWords.txt");
            using (StreamWriter writer = new StreamWriter(bannedWordsFile))
            {
                foreach (KeyValuePair<Keys, int> KEYS in mylist)
                {
                    writer.WriteLine(KEYS.Key + ": " + KEYS.Value);
                }
            }
        }
    }
}