﻿namespace PresentationCreatorAPI.Application.Interfaces;

public interface IRedisService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value);
    Task DeleteAsync(string key);
}
