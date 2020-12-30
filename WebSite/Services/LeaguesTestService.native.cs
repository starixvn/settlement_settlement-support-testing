using System.Configuration;
using System.IO;
using WebSite.Models;

namespace WebSite.Services
{
    public interface LeaguesTestService
    {
        string ReadSetting(string fileName);

        void ChangeLeaguesTest(LeaguesTestModel model);
    }

    public class LeaguesTestServiceImp : LeaguesTestService
    {
        public string ReadSetting(string fileName)
        {
            try
            {
                using (var streamReader = new StreamReader(fileName))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public void ChangeLeaguesTest(LeaguesTestModel model)
        {
            var fileNameOnUat = ConfigurationManager.AppSettings["LeaguesTestUAT"];
            var fileNameOnPro = ConfigurationManager.AppSettings["LeaguesTestPRO"];

            if (fileNameOnUat.Equals(fileNameOnPro))
            {
                model.IsClear = false;
                WriteOnUat(fileNameOnPro, model);
            }
            else
            {
                WriteOnUat(fileNameOnUat, model);

                WriteOnProduction(fileNameOnPro, model);
            }
        }

        private static void WriteOnProduction(string file, LeaguesTestModel model)
        {
            using (var tw = new StreamWriter(file, false))
            {
                tw.WriteLine($"ExcludeLeagueIds:{model.LeagueIds.Replace(" ", string.Empty)}");
                tw.WriteLine($"IncludeLeagueIds:");
            }
        }

        private static void WriteOnUat(string file, LeaguesTestModel model)
        {
            var leagueIds = model.IsClear ? "0" : model.LeagueIds.Replace(" ", string.Empty);
            using (var tw = new StreamWriter(file, false))
            {
                tw.WriteLine($"ExcludeLeagueIds:");
                tw.WriteLine($"IncludeLeagueIds:{leagueIds}");
            }
        }
    }
}