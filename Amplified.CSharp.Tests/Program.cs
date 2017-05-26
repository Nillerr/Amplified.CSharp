using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;

namespace Amplified.CSharp
{
    public class AccessToken
    {
        public DateTimeOffset Expires { get; }
        public string Token { get; } 
    }

    public class RefreshToken
    {
        public DateTimeOffset Expires { get; }
        public string Token { get; }
    }
    
    public interface ITokenManager
    {
        Maybe<AccessToken> GetAccessToken();
        Maybe<RefreshToken> GetRefreshToken();
    }

    public class AccessTokenResult
    {
        public static AccessTokenResult Empty() => new AccessTokenResult(null); 
        
        public static AccessTokenResult From(AccessToken token)
        {
            return new AccessTokenResult(token.Token);
        }

        public AccessTokenResult(string token)
        {
            Token = token;
        }

        public string Token { get; }
    }
    
    public class Program
    {
        private ITokenManager _tokenCookieManager;
        
        public Task<AccessTokenResult> Main21323(string[] args)
        {
            var instant = DateTimeOffset.Now;
            return _tokenCookieManager.GetAccessToken()
                .Filter(accessToken => accessToken.Expires > instant)
                .OrAsync(
                    () => _tokenCookieManager.GetRefreshToken()
                        .Filter(refreshToken => refreshToken.Expires > instant)
                        .MapAsync(ObtainAccessToken)
                )
                .Map(AccessTokenResult.From)
                .OrGet(AccessTokenResult.Empty);
        }

        private Task<AccessToken> ObtainAccessToken(RefreshToken refreshToken)
        {
            // Refresh the access token
            return Task.FromResult(new AccessToken());
        }
    }
}
