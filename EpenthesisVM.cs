using Epenthesis_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Epenthesis_2.ViewModel
{
    class EpenthesisVM : BaseVM
    {
        List<Entry> _data;
        string[] _paths = new[] 
        {
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\bez_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\nad_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\od_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\pod_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\przed_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\przez_processed_transcribed.txt",
            //@"D:\Projekty\Epentesis\Epenthesis_2\Data\spod_processed_transcribed.txt"

            @"Data\bez_processed_transcribed.txt",
            @"Data\nad_processed_transcribed.txt",
            @"Data\od_processed_transcribed.txt",
            @"Data\pod_processed_transcribed.txt",
            @"Data\przed_processed_transcribed.txt",
            @"Data\przez_processed_transcribed.txt",
            @"Data\spod_processed_transcribed.txt",
            @"Data\w_processed_transcribed.txt",
            @"Data\z_processed_transcribed.txt"
        };




        public List<string> Lemmas 
        { 
            get
            {
                var temp = _data.Select(x => x.Lemma + " (" + x.PartOfSpeech + ")").Distinct().ToList();
                temp.Sort();
                return temp;
            }
        }



        private string[] _fon_contexts = null;
        private string[] _cat_contexts = null;

        public string Contexts
        {            
            set 
            {

                if (value.Length == 0)
                {
                    _fon_contexts = null;
                    _cat_contexts = null;
                }
                else
                {
                    if (value.Contains(','))
                    {
                        var temp = value.Split(',');

                        var temp_cat = new List<string>();
                        var temp_fon = new List<string>();

                        foreach (var item in temp)
                        {
                            if (item.Contains('*'))
                            {
                                temp_cat.Add(item.Trim().Replace("*", ""));
                            }
                            else
                            {
                                temp_fon.Add(item.Trim());
                            }
                        }

                        _fon_contexts = temp_fon.ToArray();
                        _cat_contexts = temp_cat.ToArray();

                    }
                    else
                    {
                        if (value.Contains('*'))
                        {
                            _cat_contexts = new string[] { value.Trim().Replace("*", "") };                            
                        }
                        else
                        {
                            _fon_contexts = new string[] { value.Trim() };
                        }
                    }
                }                               

                OnPropertyChanged("Result");

                //if (value.Length == 0)
                //{
                //    _fon_contexts = null;
                //    _cat_contexts = null;                    
                //}
                //else
                //{
                //    var temp = value.Replace(" ", "").Split(',').ToList();

                //    if (temp.Count == 0)
                //    {
                //        if (value.Contains('*'))
                //        {
                //            _cat_contexts = new[] { value.Replace("*", "") };                            
                //        }
                //        else
                //        {
                //            _fon_contexts = new[] { value };                            
                //        }
                //    }
                //    else
                //    {
                //        var temp_fon_contexts = new List<string>();
                //        var temp_cat_contexts = new List<string>();

                //        foreach (var item in temp)
                //        {
                //            if (item.Contains('*'))
                //            {
                //                temp_cat_contexts.Add(item.Replace("*", ""));
                //            }
                //            else
                //            {
                //                temp_fon_contexts.Add(item);
                //            }
                //        }

                //        _fon_contexts = temp_fon_contexts.ToArray<string>();
                //        _cat_contexts = temp_cat_contexts.ToArray<string>();
                //    }

                    
                //}
                

                
            }
        }

        private string[] _words;

        public string Words
        {
            set
            {

                if (value.Length == 0)
                {
                    _words = null;
                }
                else
                {
                    if (value.Contains(','))
                    {
                        var temp = value.Split(',');

                        var temp_words = new List<string>();

                        foreach (var item in temp)
                        {
                            temp_words.Add(item.Trim());
                        }

                        _words = temp_words.ToArray();

                    }
                    else
                    {
                        _words = new string[] { value.Trim() };
                    }
                }

                OnPropertyChanged("Result");
            }
        }






        private string _selectedLemma;
        public string SelectedLemma
        {
            set 
            { 
                _selectedLemma = value.Split(' ')[0];
                _selectedPartOfSpeech = value.Split(' ')[1].Replace(")", "").Replace("(", "");

                OnPropertyChanged("Result"); 
            }
        }

        private string _selectedPartOfSpeech;



        
        public Epenthesis_2.Model.Entry.Result Result
        {
            get
            {
                return Entry.Query(_data, _selectedLemma, _selectedPartOfSpeech, _cat_contexts, _fon_contexts, _words);
            }
            
            
        }






        //public Epenthesis_2.Model.Entry.Result Result
        //{
        //    get
        //    {
        //        return Entry.Query(
        //            _data,
        //            _selectedLemma, _selectedPartOfSpeech, _cat_contexts, _fon_contexts, _words
        //            );
        //    }
        //}





        public EpenthesisVM()
        {
            _data = CsvManager.ReadFile(_paths);

            


        }



    }
}
