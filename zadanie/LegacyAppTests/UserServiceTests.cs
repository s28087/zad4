using LegacyApp;

namespace LegacyAppTests;

public class UserServiceTests
{
    [Fact]

    // test dla adduser - niepodanie @, .
    public void AddUser_Should_Return_False_When_Email_Without_Email_At_And_Dot()
    {
        //Arrange
        string fName = "Jan";
        string lName = "Malewski";
        DateTime bDate = new DateTime(2000, 3, 20);
        int clientId = 2;
        string email = "malewskigmailpl";
        var service = new UserService();

        //Act
        bool res = service.AddUser(fName, lName, email, bDate, clientId);

        //Assert
        Assert.Equal(false, res);


    }

    [Fact]
    public void AddUser_Should_Return_False_When_Age_Under_21()
    {
        //Arrange
        string fName = "Jan";
        string lName = "Malewski";
        DateTime bDate = new DateTime(2010, 3, 20);
        int clientId = 2;
        string email = "malewski@gmail.pl";
        var service = new UserService();

        //Act
        bool res = service.AddUser(fName, lName, email, bDate, clientId);

        //Assert
        Assert.Equal(false, res);
    }

    [Fact]
    public void Add_User_Should_Return_False_When_No_First_Name()
    {
        //Arrange
        string fName = null;
        string lName = "Malewski";
        DateTime bDate = new DateTime(2000, 3, 20);
        int clientId = 2;
        string email = "malewski@gmail.pl";
        var service = new UserService();

        //Act
        bool res = service.AddUser(fName, lName, email, bDate, clientId);

        //Assert
        Assert.Equal(false, res);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Very_Important_Client()
    {
        //Arrange
        string fName = "Jan";
        string lName = "Malewski";
        DateTime bDate = new DateTime(2000, 3, 20);
        int clientId = 2;
        string email = "malewski@gmail.pl";
        var service = new UserService();

        //Act
        bool res = service.AddUser(fName, lName, email, bDate, clientId);

        //Assert
        Assert.Equal(true, res);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_When_Important_Client()
    {
        //Arrange
        string fName = "Jakub";
        string lName = "Smith";
        DateTime bDate = new DateTime(1990, 3, 20);
        int clientId = 3;
        string email = "smith@gmail.pl";
        var service = new UserService();

        //Act
        bool res = service.AddUser(fName, lName, email, bDate, clientId);

        //Assert
        Assert.Equal(true, res);
    }
    
[Fact]
public void AddUser_Should_Return_False_When_Credit_Limit_Under_500_And_HadCreditLimit()
{
    //Arrange
    string fName = "Adam";
    string lName = "Kowalski";
    DateTime bDate = new DateTime(2000, 3, 20);
    int clientId = 1;
    string email = "kowalski@wp.pl";
    var service = new UserService();

    //Act
    bool res = service.AddUser(fName, lName, email, bDate, clientId);

    //Assert
    Assert.Equal(false, res);
}


[Fact]
public void AddUser_Should_Return_True_When_Normal_Client()
{
    //Arrange
    string fName = "Kamil";
    string lName = "Kwiatkowski";
    DateTime bDate = new DateTime(1990, 3, 20);
    int clientId = 5;
    string email = "andrzejewicz@wp.pl";
    var service = new UserService();

    //Act
    bool res = service.AddUser(fName, lName, email, bDate, clientId);

    //Assert
    Assert.Equal(true, res);
}


}