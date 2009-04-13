using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indihiang.Cores.Features
{
    public class GeneralFeature : BaseLogAnalyzeFeature
    {
        public GeneralFeature(EnumLogFile logFile)
            : base(logFile)
        {
            _featureName = LogFeature.GENERAL;

            LogCollection log = new LogCollection();
            _logs.Add("General", log);
        }
        protected override bool RunFeature(List<string> header, string[] item)
        {
            switch (_logFile)
            {
                case EnumLogFile.NCSA:
                    break;
                case EnumLogFile.MSIISLOG:
                    break;
                case EnumLogFile.W3CEXT:
                    RunW3cext(header, item);
                    break;
            }

            return true;
        }
        private void RunW3cext(List<string> header, string[] item)
        {
            if (header.Exists(FindPage))
            {
                int val = 0;
                int index = header.FindIndex(FindDate);
                int index2 = header.FindIndex(FindPage);
                string key = item[index];
                string dataKey = item[index2];

                if (dataKey != "" && dataKey != null && dataKey != "-")
                {
                    if (_logs["General"].Colls.ContainsKey(key))
                    {
                        if (_logs["General"].Colls[key].Items.ContainsKey(dataKey))
                        {
                            val = Convert.ToInt32(_logs["General"].Colls[key].Items[dataKey]);
                            val++;
                            _logs["General"].Colls[key].Items[dataKey] = val.ToString();
                        }
                        else
                            _logs["General"].Colls[key].Items.Add(dataKey, "1");
                    }
                    else
                        _logs["General"].Colls.Add(key, new WebLog(dataKey, "1"));
                }
            }
        }
        private static bool FindPage(string item)
        {
            if (item == "cs-uri-stem")
                return true;

            return false;
        }
        private static bool FindDate(string item)
        {
            if (item == "date")
                return true;

            return false;
        }
    }
}