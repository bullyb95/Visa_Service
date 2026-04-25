using VisaService.Domain;
using VisaService.Domain.Exceptions;
using VisaService.ValueObjects;

Console.WriteLine("Value Objects:");
var lastName = new LastName("Петров");
var firstName = new FirstName("Иван");
var middleName = new MiddleName("Сергеевич");
Console.WriteLine($"ФИО: {lastName} {firstName} {middleName}");

var email = new Email("ivan.petrov@mail.ru");
var phone = new Phone("+79991234567"); 
Console.WriteLine($"Контакты: {email}, {phone}"); 
 
var appNumber = new ApplicationNumber("VISA-2025-001");
Console.WriteLine($"Номер заявки: {appNumber}");

var position = new OfficerPosition("Старший визовый офицер");
Console.WriteLine($"Должность: {position}"); 

Console.WriteLine();
Console.WriteLine("Entities:");
 
var contactInfo = new ContactInfo("ivan.petrov@mail.ru", "+79991234567");
var applicant = new Applicant(
    Guid.NewGuid(),
    new LastName("Петров"),
    new FirstName("Иван"),
    new MiddleName("Сергеевич"),
    contactInfo);
Console.WriteLine($"Заявитель: {applicant}, id = {applicant.Id}");

var officer = new Officer(
    Guid.NewGuid(),
    new LastName("Сидоров"),
    new FirstName("Алексей"),
    new MiddleName("Петрович"),
    new OfficerPosition("Старший визовый офицер"));
Console.WriteLine($"Офицер: {officer}, должность: {officer.Position}");

Console.WriteLine();
Console.WriteLine("Бизнес-логика:");

var application = applicant.CreateApplication(new ApplicationNumber("VISA-2025-001"));
Console.WriteLine($"Заявка создана: {application.Number}");

application.FillForm();
Console.WriteLine($"Форма заполнена: {application.FormFilled}");

application.SubmitDocuments();
Console.WriteLine("Документы поданы");

application.AcceptApplication(officer);
Console.WriteLine($"Заявка принята офицером {officer}");

application.VerifyData(officer);
Console.WriteLine($"Данные проверены: {application.DocumentsChecked}");

application.IssueVisa(officer);
Console.WriteLine($"Виза выдана: {application.ResultChecked}");

Console.WriteLine($"Результат: {application.GetResult()}");

Console.WriteLine();
Console.WriteLine("Проверка исключений:");

try
{
    application.FillForm();
}
catch (InvalidApplicationStateException ex)
{
    Console.WriteLine($"InvalidApplicationStateException: {ex.Message}");
}

try
{
    var officer2 = new Officer(
        Guid.NewGuid(),
        new LastName("Козлов"),
        new FirstName("Дмитрий"),
        new MiddleName("Иванович"),
        new OfficerPosition("Визовый офицер"));
    var app2 = applicant.CreateApplication(new ApplicationNumber("VISA-2025-002"));
    app2.FillForm();
    app2.SubmitDocuments();
    app2.AcceptApplication(officer);
    app2.VerifyData(officer2);
}
catch (AnotherOfficerEditApplicationException ex)
{
    Console.WriteLine($"AnotherOfficerEditApplicationException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Готово. Нажмите любую клавишу...");
Console.ReadKey();
