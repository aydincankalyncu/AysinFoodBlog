using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class InfoManager : IInfoService
    {
        private readonly IInfoDal _infoDal;
        public InfoManager(IInfoDal infoDal)
        {
            _infoDal = infoDal;
        }
        public void Add(Info info)
        {
            _infoDal.Add(info);
        }

        public void Delete(int infoId)
        {
            _infoDal.Delete(new Info { Id = infoId });
        }

        public List<Info> GetInfo()
        {
            return _infoDal.GetList().ToList();
        }

        public void Update(Info info)
        {
            _infoDal.Update(info);
        }
    }
}
