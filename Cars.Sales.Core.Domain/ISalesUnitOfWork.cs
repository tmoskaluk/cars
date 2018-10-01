using Cars.Core.Base.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cars.Sales.Core.Domain
{
    public interface ISalesUnitOfWork
    {
        void Commit();
    }
}
