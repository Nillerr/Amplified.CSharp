using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Amplified.CSharp.Extensions;
using static Amplified.CSharp.Maybe;
using static Amplified.CSharp.Units;

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

        public static void Main123(string[] args)
        {
            /*args.FirstOrNone()
                .Map(arg => Parse<int>(arg))
                .Map(intArg => Store(intArg).Return(intArg))
                .Match(
                    intArg => Unit(() => Console.WriteLine($"Stored: {intArg}")),
                    none => Unit(() => Console.WriteLine()) 
                );*/

            /*var none = None();
            var some = Some(1);

            if (some == none)
            {
                
            }*/

            Unit unit = Some(1).ToUnit().Return(Some(1))
                .Match(
                    some => Console.WriteLine("Hello Some"),
                    none => Console.WriteLine("Hello None")
                );

        }

        public static Unit Store(int value)
        {
            return Unit();
        }
        
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
                .FlatMapUnit(_tokenCookieManager.GetAccessToken);
        }
        
        private Task<AccessToken> ObtainAccessToken(RefreshToken refreshToken)
        {
            // Refresh the access token
            return Task.FromResult(new AccessToken());
        }
    }
}
