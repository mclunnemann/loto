using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LotteryNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            getHtml();
            print();
            Console.Read();
        }        
        static Dictionary<string, int> num1 = new Dictionary<string, int>();
        static Dictionary<string, int> num2 = new Dictionary<string, int>();
        static Dictionary<string, int> num3 = new Dictionary<string, int>();
        static Dictionary<string, int> num4 = new Dictionary<string, int>();
        static Dictionary<string, int> num5 = new Dictionary<string, int>();
        static Dictionary<string, int> num6 = new Dictionary<string, int>();
        static string jackpot = "";

        public static void getHtml()
        {
            string URL2 = "https://www.kylottery.com/apps/draw_games/powerball/powerball_pastwinning.html";
            HtmlAgilityPack.HtmlWeb web = new HtmlAgilityPack.HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(URL2);
            getArray(doc);
            
        }

        public static void getArray(HtmlAgilityPack.HtmlDocument doc)
        {
            HtmlAgilityPack.HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='content']//div[@id='pastWinningNum']//table[@class='greeStyleLeftNoLines']//tbody");

            foreach (HtmlAgilityPack.HtmlNode node2 in node.SelectNodes(".//tr//td[@class='middleEntry']//table"))
            {
                int count = 1;
                foreach (HtmlAgilityPack.HtmlNode node3 in node2.SelectNodes(".//tr//td"))
                {                    
                    Regex regex1 = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
                    Regex regex2 = new Regex(@"[^\d]");
                    string result = node3.OuterHtml;
                    result = regex1.Replace(result, "").Trim();
                    result = regex2.Replace(result, "").Trim();
                    if(count == 1)
                    {
                        if (num1.ContainsKey(result))
                        {
                            num1[result]++;
                        }
                        else
                        {
                            num1.Add(result, 1);
                        }     
                    }
                    else if (count == 2)
                    {
                        if (num2.ContainsKey(result))
                        {
                            num2[result]++;
                        }
                        else
                        {
                            num2.Add(result, 1);
                        }
                    }
                    else if (count == 3)
                    {
                        if (num3.ContainsKey(result))
                        {
                            num3[result]++;
                        }
                        else
                        {
                            num3.Add(result, 1);
                        }
                    }
                    else if (count == 4)
                    {
                        if (num4.ContainsKey(result))
                        {
                            num4[result]++;
                        }
                        else
                        {
                            num4.Add(result, 1);
                        }
                    }
                    else if (count == 5)
                    {
                        if (num5.ContainsKey(result))
                        {
                            num5[result]++;
                        }
                        else
                        {
                            num5.Add(result, 1);
                        }
                    }
                    else if (count == 6)
                    {
                        if (num6.ContainsKey(result))
                        {
                            num6[result]++;
                        }
                        else
                        {
                            num6.Add(result, 1);
                        }
                    }
                    count++;
                }
            }
            int jCount = 1;
            foreach (HtmlAgilityPack.HtmlNode Jnode in doc.DocumentNode.SelectNodes("//div[@id='content']//div[@id='pastWinningNum']//table[@class='greeStyleLeftNoLines']//tbody" +
                "//tr//td[@class='middleEntry']"))
            {
                if(jCount == 3)
                {
                    Regex regexJ1 = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
                    jackpot = Jnode.OuterHtml;
                    jackpot = regexJ1.Replace(jackpot, "").Trim();
                    break;
                }
                jCount++;
            }
        }
        
        public static void print()
        {            
            var mostUsed1 = num1.OrderByDescending(x => x.Value).First();
            var mostUsed2 = num2.OrderByDescending(x => x.Value).First();
            var mostUsed3 = num3.OrderByDescending(x => x.Value).First();
            var mostUsed4 = num4.OrderByDescending(x => x.Value).First();
            var mostUsed5 = num5.OrderByDescending(x => x.Value).First();
            var mostUsed6 = num6.OrderByDescending(x => x.Value).First();

            Console.WriteLine("The most likely to win Lottery number is: "
                + mostUsed1.Key + " - " + mostUsed2.Key + " - " + mostUsed3.Key + " - " + mostUsed4.Key + " - " + mostUsed5.Key + " - " + mostUsed6.Key);

            Console.WriteLine("The last Jackport was: " + jackpot);

            Console.WriteLine("Numbers used from: https://www.kylottery.com/apps/draw_games/powerball/powerball_pastwinning.html \nThe frequency of the numbers:" 
                +  mostUsed1 + " - " + mostUsed2 + " - " + mostUsed3 + " - " + mostUsed4 + " - " + mostUsed5 + " - " + mostUsed6);
        }

    }
}
