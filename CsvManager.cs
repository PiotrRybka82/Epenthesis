using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Epenthesis_2.Model
{
    public static class CsvManager
    {
        public static List<Entry> ReadFile(string[] paths, Encoding encoding = null)
        {
            encoding = (encoding == null ? Encoding.UTF8 : encoding);

            var res = new List<Entry>();

            foreach (var path in paths)
            {
                try
                {
                    var uri = new Uri("pack://application:,,,/" + path);
                    var stream = Application.GetResourceStream(uri).Stream;
                                        

                    using (var reader = new StreamReader(stream, encoding))
                    {
                        var line = "";

                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Contains("subst")) continue;

                            var newEntry = AnalyzeEntryLine(line);

                            res.Add(newEntry);
                        }
                    }
                }
                catch (Exception)
                {
                                        
                }

                
            }

            return res;
        }

        public static Entry AnalyzeEntryLine(this string line, string[] delimiter = null)
        {
            if (delimiter == null) delimiter = new string[] { "|" };

            var res = new Entry();

            var temp = line.Split(delimiter, StringSplitOptions.None);

            res.Lemma = temp[0];
            res.Form = temp[1];
            res.PartOfSpeech = temp[2];
            res.Case = temp[3];
            res.CatContext = temp[4];
            res.FonContext = temp[5];
            res.Voc = temp[6];
            long t;
            if (long.TryParse(temp[7], out t)) res.No = t;
            else res.No = 0;
            res.Word = temp[8];                      

            return res;
        }
    }
}
