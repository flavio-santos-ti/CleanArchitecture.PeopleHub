using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PeopleHub.Application.Dtos.Log;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseAuditableUseCase : BaseUseCase
{
    protected readonly IAuditLogService _auditLogService;
    protected readonly string _contextName;

    protected BaseAuditableUseCase(
        IAuditLogService auditLogService,
        IHttpContextAccessor httpContextAccessor,
        IAuthenticatedUserAccountService authenticatedUserService,
        IContextProvider contextProvider)
        : base(httpContextAccessor, authenticatedUserService)
    {
        _auditLogService = auditLogService;
        _contextName = contextProvider.ContextName;
    }

    private async Task RegisterAuditLogAsync(string eventAction, string contextName, int httpStatusCode, object? eventData = null, string? userEmail = null)
    {
        var log = new AuditLogDto
        {
            UserEmail = !string.IsNullOrEmpty(userEmail) ? userEmail : GetAuthenticatedUserEmail(), // Usa userEmail se fornecido, senão pega do usuário autenticado
            Action = eventAction,
            ContextName = contextName,
            HttpStatusCode = httpStatusCode,
            EventData = eventData != null ? JsonConvert.SerializeObject(eventData) : null,
            UserIp = GetUserIpAddress()
        };

        await _auditLogService.RegisterLogAsync(log);
    }

    protected async Task<ApiResponseDto<T>> CreateSuccessWithAudit<T>(
        object eventValue,
        string message = "Success",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Response = eventValue
        };

        await RegisterAuditLogAsync("CREATE", _contextName, 201, eventData: logData);
        return ApiResponseDto<T>.Success(_contextName, message, 201, data);
    }

    protected async Task<ApiResponseDto<T>> CreateUploadSuccessWithAuditAsync<T>(
        string message = "Success")
    {
        var logData = new
        {
            Message = message
        };

        await RegisterAuditLogAsync("CREATE_UPLOAD", _contextName, 201, eventData: logData);
        return ApiResponseDto<T>.Success(_contextName, message, 201);
    }

    protected async Task<ApiResponseDto<T>> CreateLoginSuccessWithAudit<T>(
        object eventValue,
        string message = "Success",
        string userEmail = "",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Response = eventValue
        };

        await RegisterAuditLogAsync("CREATE", _contextName, 201, eventData: logData, userEmail);
        return ApiResponseDto<T>.Success(_contextName, message, 201, data);
    }

    protected async Task<ApiResponseDto<T>> DeleteSuccessWithAudit<T>(
    object eventValue,
    object oldValue,
    string message = "Success",
    T? data = default)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue,
            OldValue = oldValue
        };

        await RegisterAuditLogAsync("DELETE", _contextName, 200, eventData: logData);
        return ApiResponseDto<T>.Success(_contextName, message, 200, data);
    }

    protected async Task<ApiResponseDto<T>> AuditExceptionAsync<T>(
        string message = "Error",
        T? data = default)
    {
        await RegisterAuditLogAsync("ERROR", _contextName, 500, eventData: new { Error = message });
        return ApiResponseDto<T>.Fail(_contextName, message, 500);
    }

    protected async Task<ApiResponseDto<T>> AuditLoginExceptionAsync<T>(
        string message,
        string useEmail)
    {
        await RegisterAuditLogAsync("ERROR", _contextName, 500, eventData: new { Error = message });
        return ApiResponseDto<T>.Fail(_contextName, message, 500);
    }

    protected async Task<ApiResponseDto<T>> AuditValidationErrorAsync<T>(
        object eventValue,
        string message = "Validation Error",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue
        }; 

        await RegisterAuditLogAsync("ERROR", _contextName, 409, eventData: logData);

        return ApiResponseDto<T>.Fail(_contextName, message, 409);
    }

    protected async Task<ApiResponseDto<T>> AuditLoginValidationErrorAsync<T>(
        object eventValue,
        string message = "Validation Error",
        string userEmail = "",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue
        };

        await RegisterAuditLogAsync("ERROR", _contextName, 409, eventData: logData, userEmail);

        return ApiResponseDto<T>.Fail(_contextName, message, 409);
    }

    protected async Task<ApiResponseDto<T>> AuditNotFoundErrorAsync<T>(
        object eventValue,
        string message = "Validation Error",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue
        };

        await RegisterAuditLogAsync("NOT_FOUND", _contextName, 404, eventData: logData);

        return ApiResponseDto<T>.Fail(_contextName, message, 404);
    }

    protected async Task<ApiResponseDto<T>> UpdateSuccessWithAudit<T>(
        object eventValue,
        object oldValue,
        string message = "Success",
        T? data = default)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue,
            OldValue = oldValue,
        };

        await RegisterAuditLogAsync("UPDATE", _contextName, 200, eventData: logData);
        return ApiResponseDto<T>.Success(_contextName, message, 200, data);
    }

    protected async Task<ApiResponseDto<T>> LoginSuccessWithAudit<T>(
        string userEmail,
        T? data = default)
    {
        var message = "Login successful.";

        var logData = new
        {
            Token = data
        };

        await RegisterAuditLogAsync("LOGIN_SUCCESS", _contextName, 200, eventData: logData, userEmail);
        return ApiResponseDto<T>.Success(_contextName, message, 200, data);
    }

    protected async Task<ApiResponseDto<T>> AuditLoginValidationErrorAsync<T>(
        object eventValue,
        string message,
        string useEmail)
    {
        var logData = new
        {
            Message = message,
            Request = eventValue
        };

        await RegisterAuditLogAsync("ERROR", _contextName, 409, eventData: logData, useEmail);

        return ApiResponseDto<T>.Fail(_contextName, message, 409);
    }
}
