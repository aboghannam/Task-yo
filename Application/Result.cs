using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Result : IRequest
    {
        public Result()
        {

        }
        internal Result(bool success, object payload = null, string message = default)
        {
            Success = success;
            Payload = payload;
            Message = message;
        }
        //internal Result(string[] Errors)
        //{
        //    Success = false;
        //    Message = Errors.ToString();
        //}

        #region Properties

        public bool Success { get; set; }
        public string[] Errors { get; set; } = new string[] { };

        public object Payload { get; set; }
        public string Message { get; set; }
        #endregion


    }
}
