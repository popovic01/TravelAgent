using AutoMapper;
using TravelAgent.DTO.Offer;
using TravelAgent.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravelAgent.Mappings
{
    public class OfferProfile : Profile
    {
        public OfferProfile()
        {
            CreateMap<Offer, OfferReviewDTO>()
                .ForMember(dest => dest.OfferTypeId, opt => opt.MapFrom(src => src.OfferType.Id))
                .ForMember(dest => dest.TransportationTypeId, opt => opt.MapFrom(src => src.TransportationType.Id))
                .ReverseMap();
            CreateMap<Offer, OfferDTO>().ReverseMap();
        }
    }
}
