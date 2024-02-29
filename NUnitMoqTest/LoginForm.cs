using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitMoqTest
{
    internal class LoginForm
    {
        private readonly ILoginService loginService;

        public LoginForm(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        public bool ValidateUser(string username, string password)
        {
            return loginService.ValidateUser(username, password);
        }
    }

    public interface ILoginService
    {
        bool ValidateUser(string username, string password);
    }
}