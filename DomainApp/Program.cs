using VisaService.Domain;
using VisaService.Domain.Entities;
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
    new LastName("Петров"),
    new FirstName("Иван"),
    new MiddleName("Сергеевич"),
    contactInfo);
Console.WriteLine($"Заявитель: {applicant}, id = {applicant.Id}");

var officer = new Officer(
    new LastName("Сидоров"),
    new FirstName("Алексей"),
    new MiddleName("Петрович"),
    new OfficerPosition("Старший визовый офицер"));
Console.WriteLine($"Офицер: {officer}, должность: {officer.Position}");

Console.WriteLine();
Console.WriteLine("Бизнес-логика:");

var application = applicant.CreateApplication(new ApplicationNumber("VISA-2025-001"));
Console.WriteLine($"Заявка создана: {application.Number}");

// Applicant: Fill out form
applicant.FillForm(application);
Console.WriteLine($"Форма заполнена: {application.FormFilled}");

// Applicant: Submit documents
applicant.SubmitDocuments(application);
Console.WriteLine("Документы поданы");

// Officer: Accept application
officer.AcceptApplication(application);
Console.WriteLine($"Заявка принята офицером {officer}");

// Officer: Verify data
officer.VerifyData(application);
Console.WriteLine($"Данные проверены: {application.DocumentsChecked}");

// Officer: Issue visa
officer.IssueVisa(application);
Console.WriteLine($"Виза выдана: {application.ResultChecked}");

// Applicant: Receive result
var result = applicant.ReceiveResult(application);
Console.WriteLine($"Результат для заявителя: {result}");

Console.WriteLine();
Console.WriteLine("Проверка исключений:");

try
{
    applicant.FillForm(application);
}
catch (InvalidApplicationStateException ex)
{
    Console.WriteLine($"InvalidApplicationStateException: {ex.Message}");
}

try
{
    var officer2 = new Officer(
        new LastName("Козлов"),
        new FirstName("Дмитрий"),
        new MiddleName("Иванович"),
        new OfficerPosition("Визовый офицер"));
    var app2 = applicant.CreateApplication(new ApplicationNumber("VISA-2025-002"));
    applicant.FillForm(app2);
    applicant.SubmitDocuments(app2);
    officer.AcceptApplication(app2);
    officer2.VerifyData(app2);
}
catch (AnotherOfficerEditApplicationException ex)
{
    Console.WriteLine($"AnotherOfficerEditApplicationException: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("Готово. Нажмите любую клавишу...");
Console.ReadKey();
