using NSubstitute;

namespace FirstContact.Tests.ContactServiceTests;

public class UpdateContactTests
{
    [Fact]
    public void UpdateContact_Calls_GetById_And_Commit_For_Normal_Update()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        var returnContact = new Contact("Markske Vertongen") { Id = 1 };

        substituteRepo.GetById(1).Returns(returnContact);

        var contactService = new ContactService(substituteRepo);
        var result = contactService.UpdateContact(1,
            new UpdateContactRequest { Name = "Xavier Waterslagers" });

        substituteRepo.Received().Commit();

        Assert.True(result);
        Assert.Equal("Xavier Waterslagers", returnContact.Name);
        Assert.Equal(1, returnContact.Id);
    }

    [Fact]
    public void UpdateContact_Calls_GetById_And_NOT_Commit_When_No_Contact_Found()
    {
        var substituteRepo = Substitute.For<IContactRepository>();

        substituteRepo.GetById(1)
            .ReturnsForAnyArgs(x => null);

        var contactService = new ContactService(substituteRepo);
        var result = contactService.UpdateContact(1,
            new UpdateContactRequest { Name = "Xavier Waterslagers" });

        substituteRepo.DidNotReceive().Commit();

        Assert.False(result);
    }
}