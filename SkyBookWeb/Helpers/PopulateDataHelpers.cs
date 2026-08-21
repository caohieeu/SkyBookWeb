using Microsoft.AspNetCore.Mvc.Rendering;

namespace SkyBookWeb.Helpers
{
    public class PopulateDataHelpers
    {
        public static SelectListItem[] PopulateRoleData()
        {
            return [
                    new SelectListItem { Text = Utilty.Constant.RoleCustomer, Value = Utilty.Constant.RoleCustomer },
                    new SelectListItem { Text = Utilty.Constant.RoleEmployee, Value = Utilty.Constant.RoleEmployee },
                    new SelectListItem { Text = Utilty.Constant.RoleAdmin, Value = Utilty.Constant.RoleAdmin }
                ];
        }
    }
}
