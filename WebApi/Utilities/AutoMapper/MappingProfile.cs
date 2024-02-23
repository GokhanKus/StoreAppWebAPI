﻿using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate,Book>();  
        }
    }
}
//yararlı araclari, artik sadece konfigurasyon ayarlamalarini yaptigimiz webapi katmanindaki utilities klasorunde toplayalim.