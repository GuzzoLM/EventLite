using AutoMapper;
using EventLite.Streams;

namespace EventLite.MongoDB.DTO
{
    internal class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Commit, CommitDTO>().ReverseMap();
            CreateMap<EventStream, EventStreamDTO>().ReverseMap();
            CreateMap<Snapshot<object>, SnapshotDTO>().ReverseMap();
        }
    }
}