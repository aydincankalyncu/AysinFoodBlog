using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IInfoService
    {
        void Add(Info info);
        void Update(Info info);
        void Delete(int infoId);
        List<Info> GetInfo();
    }
}
