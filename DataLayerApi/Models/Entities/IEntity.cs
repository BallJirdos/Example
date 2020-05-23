using DataLayerApi.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Models.Entities
{
    public interface IEntity : IIdentity<int>
    {
    }
}
