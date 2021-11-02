using Newtonsoft.Json;
using ServicesCeiba.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace  ServicesCeiba
{
    public class HttpRequestHelper
    {
        /// <summary>
        /// webservice Url
        /// </summary>
        public string EndPointUrl { get; set; }

        /// <summary>
        /// Key known only by the client.
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Client Id for OAuth
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Capture Requests.
        /// </summary>
        bool captureRequests = false;
        /// <summary>
        /// Constructor, initializes default endpoint value
        /// </summary>
        public HttpRequestHelper()
        {
            this.EndPointUrl = ""; //https://jsonplaceholder.typicode.com
            this.captureRequests = true;
            this.ConsumerSecret = null;
            this.ConsumerKey = null;
        }

        /// <summary>
        /// Constructor, inicializa el valor del endpoint del servicio
        /// </summary>
        /// <param name="urlServicio">url del servicio web</param>
        public HttpRequestHelper(string urlServicio)
        {
            this.EndPointUrl = urlServicio;
            this.ConsumerSecret = null;
            this.ConsumerKey = null;
        }
        /// <summary>
        /// Tests the connection to the endpoint
        /// </summary>
        /// <returns>Returns object response with boolean response</returns>
        public async Task<RequestResponse<bool>> TestConnection()
        {
            RequestResponse<bool> response;

            try
            {
                HttpWebRequest iNetRequest = (HttpWebRequest)WebRequest.Create(EndPointUrl);

                iNetRequest.Timeout = 1500;

                WebResponse iNetResponse = iNetRequest.GetResponse();

                // Console.WriteLine ("...connection established..." + iNetRequest.ToString ());
                iNetResponse.Close();

                response = new RequestResponse<bool>(true, "Conexión establecida", true);

            }
            catch (WebException ex)
            {
                response = new RequestResponse<bool>(false, "Error de conexión", false);
            }

            return response;
        }

        /// <summary>
        /// Reponse with HttpClient Implementation
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public async Task<RequestResponse<T>> Post<T>(string url, Object body)
        {
            try
            {
                string serviceUrl = $"{EndPointUrl}{url}";

                // Set Dummy Certification in HttpClientHandler for HttpClient
                HttpClientHandler handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
                HttpClient httpClient = new HttpClient(handler);

                HttpResponseMessage response = new HttpResponseMessage();
                RequestResponse<T> result = new RequestResponse<T>();

                // Serialize the Body Content
                string dataJson = JsonConvert.SerializeObject(body,
                              new JsonSerializerSettings()
                              {
                                  DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                              });

                StringContent content = new StringContent(dataJson, Encoding.UTF8, "application/json");

                // Invoke Post Method until get Response 
                response = await httpClient.PostAsync(serviceUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                // Try to Deserialize the Response Object
                try
                {
                    result = JsonConvert.DeserializeObject<RequestResponse<T>>(responseString);

                    // Result is not Successful and Message is Undefined
                    if (!response.IsSuccessStatusCode && string.IsNullOrEmpty(result.Message))
                    {
                        string messageError;

                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.BadRequest:
                                messageError = "BadRequest";
                                break;

                            default:
                                messageError = "Resultado inesperado.";
                                break;
                        }

                        result = new RequestResponse<T>(false, messageError, default(T));
                    }
                }
                catch (Exception ex)
                {
                    result = new RequestResponse<T>(false, "Error deserializando.", default(T));
                }

                return result;
            }
            catch (Exception ex)
            {
                return new RequestResponse<T>(false, "Excepción no controlada.", default(T));
            }
        }

        /// <summary>
        /// Send Http Get Request
        /// </summary>
        /// <typeparam name="T">type to be returned</typeparam>
        /// <param name="url">service method</param>
        /// <returns>Request response with object response, result boolean and message</returns>
        public async Task<T> Get<T>(string url)
        {
            url = string.Format("{0}{1}", EndPointUrl, url);

            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            httpReq.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            httpReq.Method = "GET";
            httpReq.ContentType = "application/json";
            T response;
            try
            {
                var request = await httpReq.GetResponseAsync();
                using (StreamReader reader = new StreamReader(request.GetResponseStream()))
                {
                    try
                    {
                        var content = reader.ReadToEnd();
                        var responseBody = JsonConvert.DeserializeObject<T>(content);
                        response = responseBody;
                    }
                    catch (Exception ex)
                    {
                        return default(T);
                    }
                }
            }
            catch (WebException ex)
            {
                string message = "";
                switch (ex.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                        message = "Error de conexión";
                        break;
                    case WebExceptionStatus.ConnectionClosed:
                        message = "Error: Se cerró la conexión con el servidor";
                        break;
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                        message = "Error: Limite de mensaje excedido";
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        message = "Error: No se encontro el recurso";
                        break;
                    case WebExceptionStatus.ProtocolError:
                        message = "Error: no se encontró aplicación, revise el EndPoint";
                        break;
                    case WebExceptionStatus.ProxyNameResolutionFailure:
                        message = "Error: No se encontró el proxy";
                        break;
                    case WebExceptionStatus.Timeout:
                        message = "Error: Limite de tiempo para recibir respuesta";
                        break;
                    case WebExceptionStatus.UnknownError:
                        message = "Error desconocido";
                        break;
                    default:
                        message = "Error de conexión";
                        break;
                }
                return default(T);
            }

            return response;
        }

        /// <summary>
        /// Send Http Get Request with body
        /// </summary>
        /// <typeparam name="T">type to be returned</typeparam>
        /// <param name="url">service method</param>
        /// <param name="body">>Object with request body</param>
        /// <returns>Request response with object response, result boolean and message</returns>
        public async Task<RequestResponse<T>> Get<T>(string url, object body)
        {
            url = string.Format("{0}{1}", EndPointUrl, url);

            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            httpReq.Method = "GET";
            httpReq.ContentType = "application/json";
            RequestResponse<T> response;
            try
            {
                var requestStream = await httpReq.GetRequestStreamAsync().ConfigureAwait(false);
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(body,
                             new Newtonsoft.Json.JsonSerializerSettings()
                             {
                                 DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
                             });
                StreamWriter w = new StreamWriter(requestStream);
                w.Write(data);
                w.Flush();
                requestStream.Close();
                var request = await httpReq.GetResponseAsync();

                using (StreamReader reader = new StreamReader(request.GetResponseStream()))
                {
                    try
                    {
                        var content = reader.ReadToEnd();
                        var responseBody = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestResponse<T>>(content);
                        response = responseBody;
                    }
                    catch (Exception ex)
                    {
                        response = new RequestResponse<T>(false, "Error deserializando", default(T));
                    }
                }
            }
            catch (WebException ex)
            {
                string message = "";
                switch (ex.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                        message = "Error de conexión";
                        break;
                    case WebExceptionStatus.ConnectionClosed:
                        message = "Error: Se cerró la conexión con el servidor";
                        break;
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                        message = "Error: Limite de mensaje excedido";
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        message = "Error: No se encontro el recurso";
                        break;
                    case WebExceptionStatus.ProtocolError:
                        message = "Error: no se encontró aplicación, revise el EndPoint";
                        break;
                    case WebExceptionStatus.ProxyNameResolutionFailure:
                        message = "Error: No se encontró el proxy";
                        break;
                    case WebExceptionStatus.Timeout:
                        message = "Error: Limite de tiempo para recibir respuesta";
                        break;
                    case WebExceptionStatus.UnknownError:
                        message = "Error desconocido";
                        break;
                    default:
                        message = "Error de conexión";
                        break;
                }
                response = new RequestResponse<T>(false, message, default(T));
            }

            return response;
        }

        /// <summary>
        /// Send a Http POST Request
        /// </summary>
        /// <typeparam name="T">Type to be returned </typeparam>
        /// <param name="url">service method</param>
        /// <param name="cuerpo">Object with request body</param>
        /// <returns>Request response object with object response, result boolean and message</returns>
        public async Task<RequestResponse<T>> Put<T>(string url, Object body)
        {
            url = string.Format("{0}{1}", EndPointUrl, url);

            var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            httpReq.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            httpReq.Method = "PUT";
            httpReq.ContentType = "application/json";
            RequestResponse<T> response;
            try
            {
                var requestStream = await httpReq.GetRequestStreamAsync().ConfigureAwait(false);
                string data = Newtonsoft.Json.JsonConvert.SerializeObject(body,
                              new Newtonsoft.Json.JsonSerializerSettings()
                              {
                                  DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat
                              });

                StreamWriter w = new StreamWriter(requestStream);
                w.Write(data);
                w.Flush();
                requestStream.Close();
                var request = await httpReq.GetResponseAsync();

                using (StreamReader reader = new StreamReader(request.GetResponseStream()))
                {
                    try
                    {
                        var content = reader.ReadToEnd();
                        var responseBody = Newtonsoft.Json.JsonConvert.DeserializeObject<RequestResponse<T>>(content, new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat,
                            DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTimeOffset,
                            //  DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc
                        });
                        response = responseBody;

                    }
                    catch (Exception ex)
                    {
                        response = new RequestResponse<T>(false, "Error deserializando", default(T));
                        //respuesta = new RespuestaFW<T>(false, Language.Etiqueta_ErrorDeserializando, default(T));
                    }
                }
            }
            catch (WebException ex)
            {
                string message = "";
                switch (ex.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                        message = "Error de conexión";
                        break;
                    case WebExceptionStatus.ConnectionClosed:
                        message = "Error: Se cerró la conexión con el servidor";
                        break;
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                        message = "Error: Limite de mensaje excedido";
                        break;
                    case WebExceptionStatus.NameResolutionFailure:
                        message = "Error: No se encontro el recurso";
                        break;
                    case WebExceptionStatus.ProtocolError:
                        message = "Error: no se encontró aplicación, revise el EndPoint";
                        break;
                    case WebExceptionStatus.ProxyNameResolutionFailure:
                        message = "Error: No se encontró el proxy";
                        break;
                    case WebExceptionStatus.Timeout:
                        message = "Error: Limite de tiempo para recibir respuesta";
                        break;
                    case WebExceptionStatus.UnknownError:
                        message = "Error desconocido";
                        break;
                    default:
                        message = "Error de conexión";
                        break;
                }
                response = new RequestResponse<T>(false, message, default(T));
            }
            return response;
        }

    }
}
