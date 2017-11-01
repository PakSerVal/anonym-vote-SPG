using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace SPG.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        [HttpPost("get-canditates-list")]
        public string GetCandidatesList()
        {
            return "список кандитатов";
        }

        [HttpPost("send-vote")]
        public string SendVote([FromBody] Vote vote_from_client)
        { 

            string json = JsonConvert.SerializeObject(vote_from_client);
            UTF8Encoding encoding = new UTF8Encoding();

            var body = encoding.GetBytes(json);
            WebRequest request = WebRequest.Create("/*тут нужен url СХГ*/");//тут нужен url СХГ
            request.Method = "POST";//тип запроса 
            request.ContentType = "application/json";
            request.ContentLength = body.Length;
            Stream dataStream = request.GetRequestStream();//Получает поток, содержащий данные запроса путем вызова метода GetRequestStream.
            dataStream.Write(body, 0, body.Length);//Запишись данных в объект Stream.
            
            WebResponse response = request.GetResponse();//отправка данных,прием ответа
            dataStream = response.GetResponseStream();
            StreamReader stream_response = new StreamReader(dataStream);
            string responseFromServer = stream_response.ReadToEnd();

            stream_response.Close();//
            dataStream.Close();//Закройте поток запроса
            response.Close();//
            if (responseFromServer==null) { return "vote_confirmed"; }
            else return "vote_not_confirmed";



        }
    }

     public class Vote
    {
        public string cryptoVote { get; set; }
        public string Id_or_Name { get; set; }

    }
    
}
