using AutoMapper;
using DataLayerApi.Extensions;
using DataLayerApi.Models.Dtos.V10.Core;
using DataLayerApi.Models.Entities.Core;

namespace DataLayerApi.Configuration.Profiles
{
    public class CoreProfile : Profile
    {
        //public CoreProfile(IRepository<EnumItemType> eiTypeRepository)
        public CoreProfile()

        {
            this.CreateMap<EnumItemModel, EnumItem>()
            //.ForMember(dest => dest.EnumItemTypeId, opts => opts.MapFrom(i => eiTypeRepository.GetByCode(i.EnumItemType.Rel).Id))
            .ForMember(dest => dest.EnumItemTypeId, opts => opts.Ignore())
            .ForMember(dest => dest.EnumItemType, opts => opts.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.EnumItemType, opts => opts.MapFrom(i => i.EnumItemType.ToEnumItemTypeLink()));

            this.CreateMap<EnumItemTypeModel, EnumItemType>()
            //.ForMember(dest => dest.EnumItems, opts => opts.MapFrom(i => eiRepository.Get(e => e.EnumItemType.Code == i.Code)))
            .ForMember(dest => dest.EnumItems, opts => opts.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.EnumItems, opts => opts.MapFrom(i => i.ToEnumItemApiCollection()))
            .ForMember(dest => dest.ParentCode, opts => opts.MapFrom(i => i.Parent.NormalizedName));
        }
    }
}
