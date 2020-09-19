﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Services.Identity.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(AutoMapper.Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}
