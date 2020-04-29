using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace APIAutomation
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string html;
            string url = "https://reqres.in/api/users/2";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            var resp = JsonConvert.DeserializeObject<SingleUserResponseObject>(html);
            Assert.IsTrue(resp.data.id.Equals(2));
        }

        [TestMethod]
        public void TestMethod2()
        {
            string html;

            var reqObject = new UsersRequestObject();
            reqObject.name = "morpheus";
            reqObject.job = "leader";

            string request = JsonConvert.SerializeObject(reqObject);
            string url = "https://reqres.in/api/users";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            httpWebRequest.ContentType = "application/json";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(request);
            }
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            var apiResponse = JsonConvert.DeserializeObject<UsersResponseObject>(html);
            Assert.IsTrue(apiResponse.name.Equals("morpheus") && apiResponse.job.Equals("leader"));
        }
    }

    public class SingleUserResponseObject
    {
        public Data data { get; set; }
        public Ad ad { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string avatar { get; set; }
    }

    public class Ad
    {
        public string company { get; set; }
        public string url { get; set; }
        public string text { get; set; }
    }

    public class UsersRequestObject
    {
        public string name { get; set; }
        public string job { get; set; }
    }


    public class UsersResponseObject
    {
        public string name { get; set; }
        public string job { get; set; }
        public string id { get; set; }
        public DateTime createdAt { get; set; }
    }


}
