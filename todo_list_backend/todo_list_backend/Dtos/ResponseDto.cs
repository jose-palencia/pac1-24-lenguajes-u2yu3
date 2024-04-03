﻿using System.Text.Json.Serialization;

namespace todo_list_backend.Dtos
{
    public class ResponseDto<T>
    {
        public bool Status { get; set; }

        [JsonIgnore]
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
