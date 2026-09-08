using NSubstitute;

namespace FirstContact.Tests.ContactServiceTests;

public class GetAllTests
{
    [Fact]
    public void GetAll_Calls_Repo_GetAll_And_Returns_List()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.GetAll().Returns(x =>
        {
            List<Contact> contacts = [
                 new Contact("Markske") { Id = 1},
                new Contact("Xavier") { Id = 2},
                ];
            return contacts;
        });

        var contactService = new ContactService(substituteRepo);
        var result = contactService.GetAll();

        substituteRepo.Received().GetAll();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Markske", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Xavier", result[1].Name);
    }

    [Fact]
    public void GetAll_Returns_Empty_List_When_No_Data()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.GetAll().Returns(x =>
        {
            return [];
        });

        var contactService = new ContactService(substituteRepo);
        var result = contactService.GetAll();

        substituteRepo.Received().GetAll();

        Assert.Empty(result);
    }
}