using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Services.Interfaces
{
    public interface ISmsService
    {
        void SendSms(string mobile, string activationCode);
        void SendUserPasswordSms(string mobile, string password);
    }
}
