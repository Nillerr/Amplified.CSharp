using System;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using static Amplified.CSharp.Maybe;

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
        void SetAccessToken(AccessToken token);
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
        
        public Task<AccessTokenResult> GetAccessToken()
        {
            var instant = DateTimeOffset.Now;
            return _tokenCookieManager.GetAccessToken()
                .Filter(accessToken => accessToken.Expires > instant)
                .OrAsync(RefreshedAccessToken)
                .Map(AccessTokenResult.From)
                .OrGet(AccessTokenResult.Empty);
        }

        private AsyncMaybe<AccessToken> RefreshedAccessToken()
        {
            var instant = DateTimeOffset.Now;
            return _tokenCookieManager.GetRefreshToken()
                .Filter(refreshToken => refreshToken.Expires > instant)
                .MapAsync(ObtainAccessToken)
                .Map(_tokenCookieManager.SetAccessToken)
                .FlatMap(_tokenCookieManager.GetAccessToken);
        }
        
        private Task<AccessToken> ObtainAccessToken(RefreshToken refreshToken)
        {
            // Refresh the access token
            return Task.FromResult(new AccessToken());
        }
    }
}
