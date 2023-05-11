using Ardalis.GuardClauses;
using AutoMapper;
using Cegeka.Auction.Application.TodoLists.Queries;
using Cegeka.Auction.Application.Users.Queries;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.Domain.ValueObjects;
using Cegeka.Auction.Infrastructure.Identity;
using Cegeka.Auction.WebUI.Shared.AccessControl;

namespace Cegeka.Auction.Application.SubcutaneousTests.Users.Queries;

using static Testing;

public class GetUserTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnUser()
    {
        //Arrange
        await RunAsDefaultUserAsync();
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        ApplicationUser userTest = new ApplicationUser
        {
            UserName = "testName",
            Email = "testEmail",
        };

        //Act
        await AddAsync(userTest);
        var query = new GetUserQuery(userTest.Id);
        var result = await SendAsync(query);
        var resultMapped = _mapper.Map<UserDto>(userTest);

        //Assert
        Assert.That(result, Is.Not.Null);
        //    Assert.That(result.User,Is.EqualTo(resultMapped));
    }

    [Test]
    public async Task ShouldThrowException()
    {
        await RunAsDefaultUserAsync();
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        //Act
        var query = new GetUserQuery("randomString");

        //Assert
        Assert.ThrowsAsync<NotFoundException>(() => SendAsync(query));

    }
}
