using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
           

            builder.RegisterType<FoodManager>().As<IFoodService>();
            builder.RegisterType<FoodDal>().As<IFoodDal>();

            builder.RegisterType<FoodCategoryManager>().As<IFoodCategoryService>();
            builder.RegisterType<FoodCategoryDal>().As<IFoodCategoryDal>();

            builder.RegisterType<FoodCommentManager>().As<IFoodCommentService>();
            builder.RegisterType<FoodCommentDal>().As<IFoodCommentDal>();

            builder.RegisterType<InfoManager>().As<IInfoService>();
            builder.RegisterType<InfoDal>().As<IInfoDal>();


        }
    }
}
