namespace CourseMate.Models.Enums
{
public enum enumRole
{
    Admin = 1,
    Instructor = 2,
    Student = 3,
    Guest = 4
    }

    public enum enumMajor
    {
        ComputerScience = 1,
        InformationTechnology = 2,
        SoftwareEngineering = 3,
        DataScience = 4,
        CyberSecurity = 5,
        NetworkEngineering = 6,
        ArtificialIntelligence = 7,
        WebDevelopment = 8,
        MobileAppDevelopment = 9,
        CloudComputing = 10,
    }

    public enum enumYear
    {
        FirstYear = 1,
        SecondYear = 2,
        ThirdYear = 3,
        FourthYear = 4,
        FifthYear = 5,
    }

    public enum enumSemester
    {
        Fall = 1,
        Spring = 2,
        Summer = 3,
    }

    public enum enumStatus
    {
        Inactive = 0,
        Active = 1,
        Suspended = 2,
        Deleted = 3
    }

}
