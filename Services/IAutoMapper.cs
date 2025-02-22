using AutoMapper;

public partial class MappingProfile : Profile
{
    public MappingProfile()
    {

        CreateMap<AddArtistDto, Artist>();
        CreateMap<UpdateArtistDto, Artist>();

        CreateMap<AddCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<AddDiscountDto, Discount>();
        CreateMap<UpdateDiscountDto, Discount>();

        CreateMap<AddGenreDto, Genre>();
        CreateMap<UpdateGenreDto, Genre>();

        CreateMap<AddOrderDto, Order>();
        CreateMap<UpdateOrderDto, Order>();

        CreateMap<AddOrderDetailDto, OrderDetail>();
        CreateMap<UpdateOrderDetailDto, OrderDetail>();

        CreateMap<AddPublisherDto, Publisher>();
        CreateMap<UpdatePublisherDto, Publisher>();

        CreateMap<AddPromotionDto, Promotion>();
        CreateMap<UpdatePromotionDto, Promotion>();

        CreateMap<ReservationDto, Reservation>()
            .ForMember(dest => dest.ReservedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.VinylRecord, opt => opt.Ignore()) 
            .ForMember(dest => dest.Customer, opt => opt.Ignore());

        CreateMap<VinylRecordDto, VinylRecord>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
            .ForMember(dest => dest.Tracks, opt => opt.MapFrom(src => src.Tracks))
            .ForMember(dest => dest.SalePrice, opt => opt.MapFrom(src => src.SalePrice))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.Stock))
            .ForMember(dest => dest.ArtistId, opt => opt.MapFrom(src => src.ArtistId))
            .ForMember(dest => dest.PublisherId, opt => opt.MapFrom(src => src.PublisherId))
            .ForMember(dest => dest.GenreId, opt => opt.MapFrom(src => src.GenreId))
            .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
            .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher))
            .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre));
    }
}
