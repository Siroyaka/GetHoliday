using System;
using System.Linq;
using System.IO;

namespace GetHoliday
{
    class Program
    {
        static void Main(string[] args)
        {
            string t;
            using(var req = new HttpReq())
            {
                var a = req.GetContentString("https://www8.cao.go.jp/chosei/shukujitsu/syukujitsu.csv");
                a.Wait();
                t = a.Result;
            }

            var rowsData = t.Split('\n');
            var dataOver2020 =
                rowsData
                .Where(x => x.StartsWith("202"))
                .Select(x =>
                {
                    var sp = x.Split(',');
                    var ans = sp.Length > 1 ? new {D = sp[0], P = sp[1].Split('\r')[0]} : new {D = "0001/01/01", P = sp[0]};
                    return ans;
                });
            
            using(var writer = new StreamWriter("output//holyday.csv", false))
            {
                foreach(var item in dataOver2020)
                {
                    writer.WriteLine($"{item.D},{item.P}");
                }
            }
        }
    }
}
