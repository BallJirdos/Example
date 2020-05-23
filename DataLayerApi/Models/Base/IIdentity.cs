using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayerApi.Models.Base
{
    public interface IIdentity<E>
    {
        E Id { get; set; }
    }
}
