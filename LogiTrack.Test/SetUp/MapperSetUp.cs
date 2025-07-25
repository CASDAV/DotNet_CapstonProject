using AutoMapper;
using LogiTrack.Application.Mapper;
using Microsoft.Extensions.Logging;

namespace LogiTrack.Test.SetUp;

public static class MapperSetUp
{
    public static IMapper SetUp()
    {
        var loggerFactory = LoggerFactory.Create(builder => { });

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        }, loggerFactory);

        //config.AssertConfigurationIsValid();

        return config.CreateMapper();
    }

}
