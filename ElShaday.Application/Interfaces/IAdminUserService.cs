﻿using ElShaday.Application.DTOs.Requests;
using ElShaday.Application.DTOs.Responses;

namespace ElShaday.Application.Interfaces;

public interface IAdminUserService : ICrudService<AdminUserRequestDto, AdminUserResponseDto>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NickNameExistsAsync(string nickName);
    Task<bool> IdExistsAsync(int id);
    Task DeactivateAsync(int id);
    Task ActivateAsync(int id);
}