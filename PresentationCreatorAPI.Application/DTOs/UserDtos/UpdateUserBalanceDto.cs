﻿namespace Application.DTOs.UserDtos;

public class UpdateUserBalanceDto
{
    public int Id { get; set; }
    public int Balance { get; set; }
    public bool IsAdd { get; set; }

    public static implicit operator User(UpdateUserBalanceDto dto)
    {
        return new User
        {
            Id = dto.Id,
            Balance = dto.Balance
        };
    }
}
