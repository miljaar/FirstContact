using NSubstitute;

namespace FirstContact.Tests.ContactServiceTests;

public class DeleteContactTests
{
    [Fact]
    public void DeleteContact_Calls_Delete_And_Returns_True()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.Delete(1)
            .Returns(x => true);

        var contactService = new ContactService(substituteRepo);
        var result = contactService.DeleteContact(1);

        substituteRepo.Received().Delete(1);

        Assert.True(result);
    }

    [Fact]
    public void DeleteContact_Calls_Delete_And_Returns_False()
    {
        var substituteRepo = Substitute.For<IContactRepository>();
        substituteRepo.Delete(1)
            .Returns(x => false);

        var contactService = new ContactService(substituteRepo);
        var result = contactService.DeleteContact(1);

        substituteRepo.Received().Delete(1);

        Assert.False(result);
    }
}