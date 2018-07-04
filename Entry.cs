using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epenthesis_2.Model
{
    public class Entry
    {
        public string Lemma { get; set; }
        public string Form { get; set; }
        public string PartOfSpeech { get; set; }
        public string Case { get; set; }
        public string CatContext { get; set; }
        public string FonContext { get; set; }
        public string Voc { get; set; }
        public long No { get; set; }
        public string Word { get; set; }


        public class Result
        {
            public string Lemma { get; set; }
            public string PartsOfSpeech { get; set; }
            public string Cases { get; set; }

            public string Contexts { get; set; }

            public string Wok_Examples { get; set; }
            public string Nwok_Examples { get; set; }

            public long Nwok_No { get; set; }
            public long Wok_No { get; set; }

            public string Nwok_Form { get; set; }
            public string Wok_Form { get; set; }

            public long Sum { get; set; }

            public decimal Percentage { get; set; }
            
            
        }


        private static string ToString(string[] input, string delimiter = ",")
        {
            if (input == null) return "";

            var res = "";

            foreach (var item in input)
            {
                res += item;
                res += delimiter + " ";
            }

            res = res.Substring(0, res.Length - 2);

            return res;
        }


        public static Result Query(List<Entry> data, 
            string lemma, 
            string partOfSpeech, 
            string[] cat_contexts = null, string[] fon_contexts = null, string[] words = null,
            int maxExamples = 10)
        {
            var res = new Result();

            //Lemma
            var subSet = data.Where(x => x.Lemma.Equals(lemma));

            //Parts of speech
            subSet = subSet.Where(x => x.PartOfSpeech.Equals(partOfSpeech));

            ////Cases
            //if (@case != null)
            //    subSet = subSet.Where(x => x.Case.Equals(@case));

            //Categorial contexts
            if (cat_contexts != null && cat_contexts.Length != 0)
                subSet = subSet.Where(x => cat_contexts.Contains(x.CatContext));

            //Fonetical contexts
            if (fon_contexts != null && fon_contexts.Length != 0)
                subSet = subSet.Where(x => fon_contexts.Contains(x.FonContext));

            //Words
            if (words != null && words.Length != 0)
                subSet = subSet.Where(x => words.Contains(x.Word));




            res.Lemma = lemma;
            //res.Cases = @case;

            res.Nwok_Form = lemma;
            res.Wok_Form = lemma + "e";

            res.PartsOfSpeech = partOfSpeech;

            //res.Contexts = ToString(cat_contexts);
            //if (res.Contexts.Length > 0) res.Contexts += ", " + ToString(fon_contexts);
            //else res.Contexts += ToString(fon_contexts);
                        

            res.Wok_No = subSet.Where(x => x.Voc.Equals("wok")).Sum(x => x.No);
            res.Nwok_No = subSet.Where(x => x.Voc.Equals("nwok")).Sum(x => x.No);

            res.Sum = res.Wok_No + res.Nwok_No;

            res.Percentage = res.Wok_No / decimal.Parse(res.Sum.ToString());

            //res.Nwok_Examples = ToString(subSet.Where(x => x.Voc.Equals("nwok")).Select(x => x.Lemma + "+" + x.Word).Take(maxExamples).ToArray());
            //res.Wok_Examples = ToString(subSet.Where(x => x.Voc.Equals("wok")).Select(x => x.Lemma + "e+" + x.Word).Take(maxExamples).ToArray());

            return res;


        }


        public static string FormatQuery(Result result)
        {
            var res = new StringBuilder();

            res.Append("Lemat: " + result.Lemma + "\n");
            res.Append("Części mowy: " + result.PartsOfSpeech + "\n");
            res.Append("Rekcja: " + result.Cases + "\n");
            res.Append("Konteksty: " + result.Contexts + "\n");
            res.Append("\n");
            res.Append("   " + result.Nwok_Form + "");
            res.Append("Lemat: " + result.Lemma + "\n");

            return res.ToString();
        }


    }
}
