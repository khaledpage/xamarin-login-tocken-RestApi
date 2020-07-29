using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Kalksi.Models;
using Newtonsoft.Json;

namespace Kalksi.Services
{
    public class RestService
    {
        private HttpClient client;
        string grant_type = "password";

        public RestService()
        {
            /*
           Konstruktor für den REST-Service.
             > Der HTTP-Client wird initialisiert
             > Legen Sie die maximale Anzahl von Bytes für die gleichzeitige Verarbeitung fest
             > Client akzeptiert Anforderungen mit einem Header, der als [application / x-www-form-urlencoded] definiert ist.
               
            */

            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        public async Task<Token> Login(User u)
        {
            /*
            
           Anmeldemethode

            Diese Methode meldet den Benutzer bei der Anwendung an. Durch Herunterladen der vom Benutzer eingegebenen Benutzerdaten werden diese verschlüsselt
            seine Daten, und dem Benutzer wird ein Token zugewiesen. Dieses Token wird verwendet, um die Sitzung zu identifizieren, und hat seine Zeit
            Ablauf, nach dem der Benutzer die Anmeldeanforderung erneut senden muss. Die Methode ist asynchron. Das bedeutet den Rest
            Aufgaben können ausgeführt werden, während der Benutzer oder Client auf eine Antwort des Servers auf sein Token wartet. Gemäß
            ist keine Sperrfunktion.

            > Benutzerdaten werden gespeichert - Alle Anmeldedaten werden in einer Liste von Schlüssel-Wert-Paaren (Wörterbuch?) Gespeichert.
            > Berechtigungsstufe, Benutzername und Benutzercode hinzufügen (zusätzlichen Grant-Typ untersuchen)
            > Benutzerdaten werden über FormUrlEncodedContent (?) Verschlüsselt.
            > Der Client wartet nach der Anmeldung (PostResponseLogin) auf die Antwort des Servers, dh er erhält das Anmeldetoken
            > Ein Datum wird abgerufen, vordefinierte Sekunden werden zu diesem Datum hinzugefügt - dies ist der Zeitstempel des Ablaufs des Anmeldetokens
            */

            var postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("grant_type", grant_type));
            postData.Add(new KeyValuePair<string, string>("username", u.Username));
            postData.Add(new KeyValuePair<string, string>("grant_type", u.Password));

            var content = new FormUrlEncodedContent(postData);
            //var weburl = "www.test.com";

            // funktioniert nur, wenn ein echter Server zum Anrufen vorhanden ist!

            //var response = await PostResponseLogin<Token>(Constants.LoginUrl, content); 

            //DateTime dt = new DateTime();
            //dt = DateTime.Today;
            //response.expire_date = dt.AddSeconds(response.expire_in);

            //return response;

            Token token = new Token() { access_token = "test", expire_in = 30 };
            token.expire_date = DateTime.Now.AddMinutes(token.expire_in);

            return token;
        }

        //public async Task<Token> PostResponse<Token>(string weburl, FormUrlEncodedContent content)
        //{
        //    var response = await client.PostAsync(weburl, content);
        //    var jsonResult = response.Content.ReadAsStringAsync().Result;
        //    var token = JsonConvert.DeserializeObject<Token>(jsonResult);
        //    return token;
        //}

        public async Task<T> PostResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            /*

            Eine Methode zum Generieren eines Anmeldetokens

             > Warten auf die Anfrage des Clients und Senden einer Antwort mit der PostAsync-Methode
             > Sobald die Anforderung empfangen wurde, wird die Antwort als Zeichenfolge für die spätere Deserialisierung gelesen
             > Das Antwortobjekt wird zurückgegeben (warum die folgende Methode, wenn es nirgendwo verwendet wird?)

           */

            var response = await client.PostAsync(weburl, content);
            var jsonResult = response.Content.ReadAsStringAsync().Result;
            var responseObject = JsonConvert.DeserializeObject<T>(jsonResult);
            return responseObject;
        }

        public async Task<T> PostResponse<T>(string weburl, string jsonstring) where T : class 
        {
            /*

            Metoda za generiranje tokena 

           */

            Token Token = App.TotkenDatabase.GetToken();
            string ContentType = "application/json";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);

            try
            {
                var Result = await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, ContentType));
                if (Result.StatusCode == HttpStatusCode.OK)
                {
                    var JsonResult = Result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResponse;
                    }

                    catch { return null; }
                }
                
            }

            catch { return null; }

            return null;
            
        }

        public async Task<T> GetResponse<T>(string weburl) where T : class
        {
            //var Token = App.TotkenDatabase.GetToken();
                //TokenDatabase.GetToken();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            try
            {
                var Response = await client.GetAsync(weburl);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<T>(JsonResult);
                        return ContentResponse;
                    }

                    catch { return null; }
                }
            }

            catch { return null; }
            return null;                                
        }

        public async Task<List<T>> GetListOfResponses<T>(string weburl) where T : class
        {
            //var Token = App.TotkenDatabase.GetToken();
            //TokenDatabase.GetToken();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.access_token);
            try
            {
                var Response = await client.GetAsync(weburl);
                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    var JsonResult = Response.Content.ReadAsStringAsync().Result;

                    try
                    {
                        var ContentResponse = JsonConvert.DeserializeObject<List<T>>(JsonResult);
                        return ContentResponse;
                    }

                    catch { return null; }
                }
            }

            catch { return null; }
            return null;
        }



    }
}
