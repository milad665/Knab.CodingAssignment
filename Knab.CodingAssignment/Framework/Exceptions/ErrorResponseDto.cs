using System.Runtime.Serialization;
using System.Text.Json;

namespace Knab.CodingAssignment.Framework.Exceptions
{
    [DataContract]
    public class ErrorResponseDto
    {
        protected ErrorResponseDto()
        { }

        public ErrorResponseDto(
            int code,
            string message)
        {
            Code = code;
            Message = message;

            ErrorType = "Logic Error";
        }

        [DataMember]
        public int Code { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public string ErrorType { get; set; }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}