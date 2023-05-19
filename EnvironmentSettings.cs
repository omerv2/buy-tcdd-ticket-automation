using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Buy.Tcdd.Ticket
{
    public class EnvironmentSettings
    {
        public static void TryLoadDotEnv()
        {
            try
            {
                DotNetEnv.Env.Load();
            }
            catch (FileNotFoundException)
            {
                // do nothing we only use .env file in development
            }
        }
        public static EnvironmentSettings Bind(IConfiguration configurationManager)
        {
            var EnvironmentSettings = new EnvironmentSettings();
            configurationManager.Bind(EnvironmentSettings);

            return EnvironmentSettings;
        }
        public string NEREDEN { get; set; }
        public string NEREYE { get; set; }
        public string GIDIS_TARIH { get; set; }
        public string SAAT { get; set; }
        public string CEP_TEL { get; set; }
        public string EMAIL { get; set; }

        public string YOLCU_ISIM { get; set; }
        public string YOLCU_SOYISIM { get; set; }
        public string YOLCU_TC { get; set; }
        public string CINSIYET { get; set; }
        public string YOLCU_DOGUM_TARIHI { get; set; }


        public string KKART_ISIMSOYISIM { get; set; }
        public string KKART_NO { get; set; }
        public string KKART_CVC { get; set; }
        public string KKART_SKT_AY { get; set; }
        public string KKART_SKT_YIL { get; set; }

    }
}
