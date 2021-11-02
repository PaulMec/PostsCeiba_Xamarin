using System;
using System.Collections.Generic;
using System.Text;

namespace ManagementServices.Models
{
    public class RequestResponse<T>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public RequestResponse()
        {

        }
        /// <summary>
        /// Consctructor of response
        /// </summary>
        /// <param name="successful"></param>
        /// <param name="msg"></param>
        /// <param name="result"></param>
        public RequestResponse(bool successful, string msg, T result)
        {
            Message = msg;
            Successful = successful;
            Result = result;
        }
        /// <summary>
        /// Message related to response
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Response status
        /// </summary>
        public bool Successful { get; set; }
        /// <summary>
        /// delivered result
        /// </summary>
        public T Result { get; set; }
    }
}
