// This file is provided freely for use, modification, and distribution without restriction. 
// No specific license applies, and you are free to share, modify, and redistribute this file as needed.

namespace InteliblueVueJsHrManagement.Server.Models;

public class Error
{
    public Error(string message)
    {
        Message = message;
    }

    public string Message { get; }

    public static Error None => new(null);

    public static implicit operator Error(string message) => new(message);

    public static implicit operator string(Error error) => error.Message;
}
