using NSubstitute;

namespace FirstContact.Tests.ContactServiceTests;

public class AddContactTests
{
    [Fact]
    public void AddContact_Calls_RepoAdd_And_Returns_ID_And_Name()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        var contactRequest = new CreateContactRequest { Name = "Dimitri De Trimmerie" };

        substituteRepo.When(repo => repo.Add(Arg.Any<Contact>()))
            .Do(repo =>
            {
                var contactItem = (Contact)repo[0];
                Assert.Equal("Dimitri De Trimmerie", contactItem.Name);
                contactItem.Id = 1;
            });

        var contactService = new ContactService(substituteRepo);

        var result = contactService.AddContact(contactRequest);
        Assert.Equal(1, result.Id);
        Assert.Equal("Dimitri De Trimmerie", result.Name);
        substituteRepo.Received().Add(Arg.Any<Contact>());
    }
}