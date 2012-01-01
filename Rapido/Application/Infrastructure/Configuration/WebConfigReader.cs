using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using System.Collections.Specialized;
using System.Data.Common;
using Rapido.Infrastructure.Data;

namespace Rapido.Application.Infrastructure.Configuration
{
    public class WebConfigReader
    {
        public ConnectionStringInfo GetConnectionStringInfo(string key)
        {
            return new ConnectionStringInfo(WebConfigurationManager.ConnectionStrings[key].ConnectionString.ToString()
                                            , WebConfigurationManager.ConnectionStrings[key].ProviderName.ToString());
        }

        public string GetConnectionString(string key)
        {
            //decrypts ConnectionStrings section if encrypted
            return WebConfigurationManager.ConnectionStrings[key].ConnectionString.ToString();
        }

        public string GetConnectionStringProviderName(string key)
        {
            //decrypts ConnectionStrings section if encrypted
            return WebConfigurationManager.ConnectionStrings[key].ProviderName.ToString();
        }

        public bool SettingExists(string key)
        {
            return (null == WebConfigurationManager.AppSettings[key]) ? false : true;
        }

        public string GetSetting(string key)
        {
            //Contract: ToString() is called to throw an exception if the key does not exist
            return WebConfigurationManager.AppSettings[key].ToString();
        }

        public List<string> GetSettings(string key)
        {
            List<string> settingsList = new List<string>();
            string setting = WebConfigurationManager.AppSettings[key].ToString();
            setting = setting.Replace(Environment.NewLine, "");
            setting = setting.Replace(" ", "");
            string[] settings = setting.Split(',');
            settingsList.AddRange(settings);
            return settingsList;
        }

        public bool GetBoolSetting(string key)
        {
            //Contract: parse method should throw an exception if the key does not exist
            string s = GetSetting(key);

            return Boolean.Parse(s);
        }

        public int GetIntSetting(string key)
        {
            //Contract: parse method should throw an exception if the key does not exist
            string s = GetSetting(key);

            return int.Parse(s);
        }



    }
}