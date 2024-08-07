﻿using Microsoft.AspNetCore.Http;
using PresentationCreatorAPI.Domain.Entites;
using PresentationCreatorAPI.Enums;
using System.ComponentModel.DataAnnotations;

namespace PresentationCreatorAPI.Application.DTOs;
public class AddPaymentDto
{
    public int Summa { get; set; }
    public string Caption { get; set; } = string.Empty;
    [Required]
    public IFormFile File { get; set; } = null!;

    public static implicit operator Payment(AddPaymentDto dto)
    {
        return new Payment
        {
            Summa = dto.Summa,
            Caption = dto.Caption,
            Status = PaymentStatus.Expected,
            FilePath = ""
        };
    }
}
