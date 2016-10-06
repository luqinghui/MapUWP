using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace LUCWQ.MapAPI
{
    public class BDMapAPI
    {
        private static string ak = "9SMLNEG9oTwVfcd0ciGBGKvSuv9kWne2";
        private static string bdplaceapitemplate = "http://api.map.baidu.com/place/v2/suggestion?query={0}&region={1}&output=json&ak={2}";

        public async static void GetSuggestPlaceByName(string inputname, string region, ObservableCollection<Result> suggestplace)
        {
            string reqstr = string.Format(bdplaceapitemplate, inputname, region, ak);
            //string reqstrencode = WebUtility.UrlEncode(reqstr);
            var http = new HttpClient();
            var response = await http.GetAsync(reqstr);
            var resultstr = await response.Content.ReadAsStringAsync();
            var serializer = new DataContractJsonSerializer(typeof(ReturnResultByBD));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(resultstr));
            var data = (ReturnResultByBD)serializer.ReadObject(ms);
            suggestplace.Clear();
            data.result.ForEach(p => suggestplace.Add(p));     
        }
    }


    [DataContract]
    public class Location
    {
        [DataMember]
        public double lat { get; set; }
        [DataMember]
        public double lng { get; set; }

    }
    [DataContract]
    public class Result
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public string uid { get; set; }
        [DataMember]
        public string city { get; set; }
        [DataMember]
        public string district { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
    [DataContract]
    public class ReturnResultByBD
    {
        [DataMember]
        public int status { get; set; }
        [DataMember]
        public string message { get; set; }
        [DataMember]
        public List<Result> result { get; set; }
    }

}
