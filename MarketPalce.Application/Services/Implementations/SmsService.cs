using MarketPlace.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Implementations
{
    public class SmsService : ISmsService
    {
        private readonly string apiKey = "6F63563266655438434C6A64465A6811132225436D4E6B694D646D6948423889526F3031743545533148453A";
        public void SendSms(string mobile, string activationCode)
        {
#if !DEBUG
            var kavenegarApi = new Kavenegar.KavenegarApi(apiKey);
            kavenegarApi.VerifyLookup(mobile, activationCode, "MarketVerify"); 
#endif
        }


        public void SendUserPasswordSms(string mobile, string password)
        {
#if !DEBUG
            var kavenegarApi = new Kavenegar.KavenegarApi(apiKey);
            kavenegarApi.VerifyLookup(mobile, password, "MarketPassword");
#endif
        }
    }
}
