using AmzErp.DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace AmzErp.Common.Attribute
{
    public class RoleAttribute: System.Attribute
    {
        public IEnumerable<UserType> UserType { get; }


        public RoleAttribute(params UserType[] userTypes)
        {
            this.UserType = userTypes;
        }
        public RoleAttribute()
        {
        }
    }
}
