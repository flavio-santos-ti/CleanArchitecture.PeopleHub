﻿namespace PeopleHub.Application.Dtos.UserAccount;

public class UpdateUserAccountDto
{
    public string Email { get; set; } = string.Empty;
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}
