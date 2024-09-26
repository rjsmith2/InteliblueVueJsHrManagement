// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

namespace InteliblueVueJsHrManagement.Server.Models;

public class Result
{
    public bool IsSuccess { get; }
    public Error Error { get; }
    public Result(bool isSuccess, Error error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }



    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Success<T>(T data) => new(true, Error.None, data);

    public static Result<T> Failure<T>(Error error) => new(false, error, default);
}
