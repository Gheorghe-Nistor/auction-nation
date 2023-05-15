using Ardalis.GuardClauses;
using AutoMapper;
using Cegeka.Auction.Application.Common.Exceptions;
using Cegeka.Auction.Application.TodoLists.Commands;
using Cegeka.Auction.Application.Users.Commands;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.Infrastructure.Identity;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.TodoLists;

namespace Cegeka.Auction.Application.SubcutaneousTests.Users.Commands;

using static Testing;
public class UpdateUserPreferencesTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateUserEmail()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);
        var mappedUser = _mapper.Map<UserDto>(user);
        mappedUser.Email = "test@gmail.com";

        var command = new UpdateUserCommand(mappedUser);
        await SendAsync(command);

        var updatedUser = await FindAsync<ApplicationUser>(userId);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser, Is.EqualTo(user));
    }

    [Test]
    public async Task ShouldUpdateUserLanguage()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);

        if (user != null)
            user.LanguageId = 1;
        var mappedUser = _mapper.Map<UserDto>(user);

        var command = new UpdateUserCommand(mappedUser);
        await SendAsync(command);

        var updatedUser = await FindAsync<ApplicationUser>(userId);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser, Is.EqualTo(user));
    }
    [Test]
    public async Task ShouldUpdateUserCurrency()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);

        if (user != null)
            user.CurrencyId = 1;
        var mappedUser = _mapper.Map<UserDto>(user);

        var command = new UpdateUserCommand(mappedUser);
        await SendAsync(command);

        var updatedUser = await FindAsync<ApplicationUser>(userId);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser, Is.EqualTo(user));
    }
    [Test]
    public async Task ShouldUpdateUserTimeZone()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);

        if (user != null)
        {
            user.TimeZoneId = 1;
        }
        var mappedUser = _mapper.Map<UserDto>(user);

        var command = new UpdateUserCommand(mappedUser);
        await SendAsync(command);

        var updatedUser = await FindAsync<ApplicationUser>(userId);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser, Is.EqualTo(user));
    }
    [Test]
    public async Task ShouldUpdateDisplaySettings()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);

        if (user != null)
            user.DisplaySettingId = 1;
        var mappedUser = _mapper.Map<UserDto>(user);

        var command = new UpdateUserCommand(mappedUser);
        await SendAsync(command);

        var updatedUser = await FindAsync<ApplicationUser>(userId);

        Assert.That(updatedUser, Is.Not.Null);
        Assert.That(updatedUser, Is.EqualTo(user));
    }

    [Test]
    public async Task ShouldThrowError()
    {
        IMapper _mapper;
        MapperConfiguration configuration;
        configuration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ApplicationUser, UserDto>();
        });
        _mapper = new Mapper(configuration);

        var userId = await RunAsDefaultUserAsync();
        var user = await FindAsync<ApplicationUser>(userId);

        if (user != null)
            user.DisplaySettingId = 10;
        var mappedUser = _mapper.Map<UserDto>(user);
        mappedUser.Id = "random";

        var command = new UpdateUserCommand(mappedUser);


        Assert.ThrowsAsync<NotFoundException>(() => SendAsync(command));
    }

    [Test]
    public async Task ShouldThrowNullException()
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
        var user = await FindAsync<ApplicationUser>("randomId");
        var mappedUser = _mapper.Map<UserDto>(user);
        var command = new UpdateUserCommand(mappedUser);

        //Assert
        Assert.ThrowsAsync<NullReferenceException>(() => SendAsync(command));

    }


}

/*
 *  public string? Language { get; set; } = "";
    public string? Currency { get; set; } = "";
    public string? TimeZone { get; set; } = "";
    public string? DisplaySetting { get; set; } = "";
 * */
