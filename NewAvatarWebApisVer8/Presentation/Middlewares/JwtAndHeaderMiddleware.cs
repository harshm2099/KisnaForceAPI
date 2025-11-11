using Microsoft.IdentityModel.Tokens;
using NewAvatarWebApis.Core.Application.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;

namespace NewAvatarWebApis.Presentation.Middlewares
{
    public class JwtAndHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtAndHeaderMiddleware> _logger;

        public JwtAndHeaderMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<JwtAndHeaderMiddleware> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower();

            var allowedPaths = new[]
            {
                "/api/v1/login/login",
                "/api/v1/login/send-email-otp",
                "/api/v1/login/send-otp",
                "/api/v1/login/send-otp-me",
                "/api/v1/login/verify-email",
                "/api/v1/login/login-with-mobile-otp",
                "/api/v1/login/login-new-emob",
                "/api/v1/login/login-with-email-otp",
                "/api/v1/login/login-with-userid",
                "/api/v1/common/splash-screen-list"
            };

            // Check if path starts with swagger or matches allowed list
            if (allowedPaths.Contains(path, StringComparer.OrdinalIgnoreCase) ||
                path?.StartsWith("/swagger", StringComparison.OrdinalIgnoreCase) == true)
            {
                await _next(context);
                return;
            }

            var headers = context.Request.Headers;

            // Log headers (debug only)
            _logger.LogInformation("Incoming headers: {Headers}", string.Join(", ", headers.Select(h => $"{h.Key}={h.Value}")));

            var commonHeader = new CommonHeader
            {
                devicetype = headers["devicetype"].FirstOrDefault(),
                devicename = headers["devicename"].FirstOrDefault(),
                appversion = headers["appversion"].FirstOrDefault(),
                orgtype = headers["orgtype"].FirstOrDefault()
            };

            // Read custom Authentication header (instead of Authorization)
            string? token = null;
            if (headers.TryGetValue("X-Authentication", out var authValues))
                token = authValues.FirstOrDefault();

            // Normalize if prefixed (optional)
            if (!string.IsNullOrEmpty(token) && token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                token = token.Substring("Bearer ".Length).Trim();

            commonHeader.Authorization = token;

            if (string.IsNullOrEmpty(commonHeader.Authorization))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { error = "Authentication header not found" });
                return;
            }

            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];

                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(commonHeader.Authorization, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                context.User = new System.Security.Claims.ClaimsPrincipal(
                    new System.Security.Claims.ClaimsIdentity(jwtToken.Claims, "jwt")
                );

                context.Items["CommonHeader"] = commonHeader;

                await _next(context);
            }
            catch (SecurityTokenExpiredException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsJsonAsync(new { error = "Token expired" });
            }
            catch (SecurityTokenException ex)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsJsonAsync(new { error = $"Invalid token: {ex.Message}" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in JwtAndHeaderMiddleware");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = $"Unexpected error: {ex.Message}" });
            }
        }
    }
}
