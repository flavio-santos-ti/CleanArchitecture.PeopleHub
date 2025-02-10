using Microsoft.AspNetCore.Http;
using PeopleHub.Application.Interfaces.UserAccount;

namespace PeopleHub.Application.UseCases.Base
{
    public abstract class BaseUseCase
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IAuthenticatedUserAccountService _authenticatedUserService;

        protected BaseUseCase(IHttpContextAccessor httpContextAccessor, IAuthenticatedUserAccountService authenticatedUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _authenticatedUserService = authenticatedUserService;
        }

        protected string GetAuthenticatedUserEmail()
        {
            return _authenticatedUserService.GetAuthenticatedUserEmail() ?? "Trying a New User";
        }

        protected string GetUserIpAddress()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                return "Unknown IP";
            }

            // Check if there is a 'X-Forwarded-For' header(in case it is behind a proxy / reverse proxy).
            var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedHeader))
            {
                return forwardedHeader.Split(',')[0].Trim(); // Gets the first IP if there is a list.
            }

            //  Gets the IP directly from the connection.
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress == null)
                return "Unknown IP";

            // If the address is ::1 (loopback IPv6), try to get another valid IP (IPv6 or IPv4).
            if (remoteIpAddress.ToString() == "::1")
            {
                var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up);

                foreach (var netInterface in networkInterfaces)
                {
                    var ipProps = netInterface.GetIPProperties();
                    foreach (var addr in ipProps.UnicastAddresses)
                    {
                        if (addr.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 &&
                            !addr.Address.IsIPv6LinkLocal && !addr.Address.IsIPv6SiteLocal)
                        {
                            return addr.Address.ToString(); // Returns a real IPv6.
                        }
                    }
                }
            }

            // If no valid IPv6 is found, returns the connection IP (which can be either IPv4 or IPv6).
            return remoteIpAddress.ToString();
        }
    }
}
