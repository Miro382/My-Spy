using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

//Created by Miroslav Murin

namespace Saving
{

    class OneLineSave
    {
        public bool SaveToFile(string Text, string PathToFile)
        {
            try
            {
                StreamWriter wr = new StreamWriter(PathToFile);
                wr.WriteLine(Text);
                wr.Flush();
                wr.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }

        public string GetFromFile(string PathToFile)
        {
            try
            {
                StreamReader rr = new StreamReader(PathToFile);
                return rr.ReadToEnd();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return "";
            }
        }
    }

    class SaveWriter
    {
        private List<string> Tag = new List<string>();
        private List<string> Value = new List<string>();
        private List<string> Tagl = new List<string>();
        private List<string> Valuel = new List<string>();
        public string PathToFile = "";

        public SaveWriter(string Pathtofile)
        {
            PathToFile = Pathtofile;
        }

        public SaveWriter()
        {
        }


        public void AddItem(string tag, string value)
        {
            value = ToSaveSafeString(value);
            Tag.Add(tag);
            if (string.IsNullOrEmpty(value))
                value = "$NOT$";
            Value.Add(value);
        }


        public void AddItem(string tag, int value)
        {
            Tag.Add(tag);
            Value.Add("" + value);
        }


        public void AddItem(string tag, bool value)
        {
            Tag.Add(tag);
            Value.Add("" + value);
        }


        public void AddItem(string tag, float value)
        {
            Tag.Add(tag);
            Value.Add("" + value);
        }

        public void AddItem(string tag, short value)
        {
            Tag.Add(tag);
            Value.Add("" + value);
        }

        public void AddItem(string tag, double value)
        {
            Tag.Add(tag);
            Value.Add("" + value);
        }


        public void RemoveItem(string tag)
        {
            for (int i = 0; i < Tag.Count; i++)
            {
                if (Tag[i].Equals(tag))
                {
                    Tag.RemoveAt(i);
                    Value.RemoveAt(i);
                }
            }
        }

        public void RemoveAllItemsWithValue(string value)
        {
            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i].Equals(value))
                {
                    Tag.RemoveAt(i);
                    Value.RemoveAt(i);
                }
            }
        }

        public void Clear()
        {
            Tag.Clear();
            Value.Clear();
        }

        public void Destroy()
        {
            Tag.Clear();
            Value.Clear();
            Tagl.Clear();
            Valuel.Clear();
        }



        public bool Save()
        {
            StreamWriter wr = new StreamWriter(PathToFile);
            try
            {
                for (int i = 0; i < Tag.Count; i++)
                {
                    wr.WriteLine("[" + Tag[i] + "]=" + Value[i]);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            wr.Flush();
            wr.Close();
            return true;
        }



        public bool Load()
        {

            try
            {
                if (File.Exists(PathToFile))
                {
                    using (StreamReader sr = File.OpenText(PathToFile))
                    {
                        string ts = sr.ReadToEnd();


                        char[] charsdel = new char[] { '=', '[', ']', '\n', '\r' };
                        string[] words = ts.Split(charsdel, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < words.Length / 2; i++)
                        {
                            Tagl.Add(words[0 + (i * 2)]);
                            Valuel.Add(words[1 + (i * 2)]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }


        public string GetItem(string tag)
        {
            string Itm = Valuel[Tagl.IndexOf(tag)];
            Itm = FromSaveSafeString(Itm);
            if (Itm.Equals("$NOT$"))
                Itm = "";
            return Itm;
        }

        public int GetItemInt(string tag)
        {
            return int.Parse(Valuel[Tagl.IndexOf(tag)]);
        }

        public bool GetItemBool(string tag)
        {
            return bool.Parse(Valuel[Tagl.IndexOf(tag)]);
        }

        public short GetItemShort(string tag)
        {
            return short.Parse(Valuel[Tagl.IndexOf(tag)]);
        }

        public float GetItemFloat(string tag)
        {
            return float.Parse(Valuel[Tagl.IndexOf(tag)]);
        }

        public double GetItemDouble(string tag)
        {
            return double.Parse(Valuel[Tagl.IndexOf(tag)]);
        }

        public void LoadedValuesToSaveValues()
        {
            Tag.Clear();
            Tag = Tagl;
            Value.Clear();
            Value = Valuel;
        }

        public bool TagExist(string tag)
        {
            try
            {
                if (Tagl.IndexOf(tag) >= 0)
                    return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            return false;
        }


        private static string ToSaveSafeString(string text)
        {
            StringBuilder bs = new StringBuilder(text);
            bs.Replace("=", "$EQC$");
            bs.Replace("[", "$LSC$");
            bs.Replace("]", "$RSC$");
            return bs.ToString();
        }

        private static string FromSaveSafeString(string text)
        {
            StringBuilder bs = new StringBuilder(text);
            bs.Replace("$EQC$", "=");
            bs.Replace("$LSC$", "[");
            bs.Replace("$RSC$", "]");
            return bs.ToString();
        }

    }
}
