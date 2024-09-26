// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.


namespace InteliblueVueJsHrManagement.Server.Models;

public class Result<T> : Result
{
    public T? Data { get; }

    public Result(bool isSuccess, Error error, T? data) : base(isSuccess, error)
    {
        Data = data;
    }
}
