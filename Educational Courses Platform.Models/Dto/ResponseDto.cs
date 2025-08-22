using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Educational_Courses_Platform.Models.Dto
{
    public class ResponseDto<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public T? Data { get; set; }

        public ResponseDto(bool success, string? message = null, IEnumerable<string>? errors = null, T? data = default)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Data = data;
        }
    }
}
