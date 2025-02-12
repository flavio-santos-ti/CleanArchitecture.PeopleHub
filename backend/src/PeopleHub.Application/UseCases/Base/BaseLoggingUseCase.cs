using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PeopleHub.Application.Actions;
using PeopleHub.Application.Dtos.Log;
using PeopleHub.Application.Dtos.Response;
using PeopleHub.Application.Interfaces.Common;
using PeopleHub.Application.Interfaces.Log;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base;

public abstract class BaseLoggingUseCase : BaseUseCase
{
    protected readonly IAuditLogService _auditLogService;
    protected readonly string _contextName;

    protected BaseLoggingUseCase(
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
            UserEmail = !string.IsNullOrEmpty(userEmail) ? userEmail : GetAuthenticatedUserEmail(), 
            Action = eventAction,
            ContextName = contextName,
            HttpStatusCode = httpStatusCode,
            EventData = eventData != null ? JsonConvert.SerializeObject(eventData) : null,
            UserIp = GetUserIpAddress()
        };

        await _auditLogService.RegisterLogAsync(log);
    }

    protected async Task<ApiResponseDto<T>> ResponseAsync<T>(
        LogAction logAction,
        string message,
        object? eventValue = default,
        object? oldValue = default,
        T? data = default,
        string? userEmail = null)
    {
        object? logData = null;
        int httpStatusCode = 0;

        if (logAction == LogAction.CREATE)
        {
            httpStatusCode = 201;
            logData = new { Message = message, Response = eventValue };
        }
        else if (logAction == LogAction.CREATE_UPLOAD)
        {
            httpStatusCode = 201;
            logData = new { Message = message };
        }
        else if (logAction == LogAction.DELETE)
        {
            httpStatusCode = 200;
            logData = new { Message = message, Request = eventValue, OldValue = oldValue };
        }
        else if (logAction == LogAction.ERROR)
        {
            httpStatusCode = 500;
            logData = new { Error = message };
        }
        else if (logAction == LogAction.VALIDATION_ERROR)
        {
            httpStatusCode = 409;
            logData = new { Message = message, Response = eventValue };
        }
        else if (logAction == LogAction.NOT_FOUND)
        {
            httpStatusCode = 404;
            logData = new { Message = message, Response = eventValue };
        }
        else if (logAction == LogAction.UPDATE)
        {
            httpStatusCode = 200;
            logData = new { Message = message, Request = eventValue, OldValue = oldValue };
        }
        else if (logAction == LogAction.LOGIN_SUCCESS)
        {
            httpStatusCode = 200;
            logData = new { Token = data };
        }
        else if (logAction == LogAction.READ)
        {
            httpStatusCode = 200;
            logData = new { Message = message, Response = eventValue };
        };

        await RegisterAuditLogAsync(logAction.Value, _contextName, httpStatusCode, eventData: logData, userEmail);
        return ApiResponseDto<T>.Success(_contextName, message, httpStatusCode, data);
    }
}
