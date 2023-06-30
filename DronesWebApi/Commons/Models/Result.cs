using System;
using System.Collections.Generic;
using System.Linq;

namespace DronesWebApi.Commons.Models
{
    public class Result<T> where T : class
    {
        internal Result(bool succeeded, IDictionary<string, string[]> errors, T value = default)
        {
            Succeeded = succeeded;
            Errors = errors;
            Value = value;
        }

        public bool Succeeded { get; set; }

        public IDictionary<string, string[]> Errors { get; set; }

        public T Value { get; set; }
    }

    public static class Result
    {
        public static Result<Object> Failure(IDictionary<string, string[]> errors)
        {
            return new Result<Object>(succeeded: false, errors);
        }

        public static Result<T> Success<T>(T value) where T : class 
        {
            return new Result<T>(succeeded: true, errors: new Dictionary<string, string[]>(), value);
        }
    }
}
