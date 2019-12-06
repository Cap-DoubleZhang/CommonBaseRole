using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Model;
using Model.DtoModel.BaseRole;
using Model.EntityModel.BaseRole;

namespace CommonBaseRole.AutoMapper
{
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<List<SystemRole>, List<SystemRoleDto>>();
        }
    }
}
