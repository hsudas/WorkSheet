using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkSheet.Models.Entity;

namespace WorkSheet.Services.Interface
{
    public interface ILoginService
    {
        User LoginUser(LoginModel loginmodel);

        List<Menu> GetMenu();

    }
}
