using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iap.API.Common
{
    public class Result<T>
    {
        public bool IsSuccess {get;}
        public T? Value {get;}
        public string? Error {get;}
        public ResultType ResultType {get;}

        private Result(bool isSuccess, T? value,
            string? error, ResultType type)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
            ResultType = type;
        }

        // Success 
        public static Result<T> Success(T value) => 
            new(true, value, null, ResultType.Success);

        // Failures
        public static Result<T> NotFound(string error) => 
            new(false, default, error, ResultType.NotFound);

        public static Result<T> Conflict(string error) => 
            new(false, default, error, ResultType.Conflict);

        public static Result<T> ValidationError(string error) => 
            new(false, default, error, ResultType.ValidationError);

        public static Result<T> Unauthorized(string error) => 
            new(false, default, error, ResultType.Unauthorized);
        
        public static Result<T> Forbidden(string error) => 
            new(false, default, error, ResultType.Forbidden);
    }

    // Non generic version for operations that dont return data
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }
        public ResultType ResultType { get; }

        private Result(bool isSuccess, string? error, ResultType type)
        {
            IsSuccess = isSuccess;
            Error = error;
            ResultType = type;
        }

        public static Result Success() =>
            new(true, null, ResultType.Success);

        public static Result NotFound(string error) =>
            new(false, error, ResultType.NotFound);

        public static Result Conflict(string error) =>
            new(false, error, ResultType.Conflict);

        public static Result ValidationError(string error) =>
            new(false, error, ResultType.ValidationError);

        public static Result Unauthorized(string error) =>
            new(false, error, ResultType.Unauthorized);

        public static Result Forbidden(string error) =>
            new(false, error, ResultType.Forbidden);
    }
}