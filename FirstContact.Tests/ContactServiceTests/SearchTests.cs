using NSubstitute;

namespace FirstContact.Tests.ContactServiceTests;

public class SearchTests
{
    [Fact]
    public void Search_Call_Returns_List_Of_Contacts()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.Search(Arg.Any<string>())
            .ReturnsForAnyArgs(x =>
        {
            Assert.Equal("Mark", x[0]);
            List<Contact> contacts = [
                new Contact("Markske Vertongen") { Id = 1},
                new Contact("Mark Dinges") { Id = 3},
                ];
            return contacts;
        });

        var contactService = new ContactService(substituteRepo);
        var result = contactService.Search("Mark");

        substituteRepo.Received().Search(Arg.Any<string>());

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Markske Vertongen", result[0].Name);
        Assert.Equal(3, result[1].Id);
        Assert.Equal("Mark Dinges", result[1].Name);
    }

    [Fact]
    public void Search_Call_Returns_Empty_List_For_No_Match()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.Search(Arg.Any<string>())
            .ReturnsForAnyArgs(x =>
        {
            Assert.Equal("Mark", x[0]);
            return [];
        });

        var contactService = new ContactService(substituteRepo);
        var result = contactService.Search("Mark");

        substituteRepo.Received().Search(Arg.Any<string>());

        Assert.Empty(result);
    }
}