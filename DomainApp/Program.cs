using System;

namespace DomainApp;

// ===== ВСЕ КЛАССЫ ВНУТРИ (временное решение) =====

public class PersonName
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public PersonName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public class ContactInfo
{
    public string Email { get; set; }
    public string Phone { get; set; }

    public ContactInfo(string email, string phone)
    {
        Email = email;
        Phone = phone;
    }
}

public class ApplicationNumber
{
    public string Value { get; set; }

    public ApplicationNumber(string value)
    {
        Value = value;
    }
}

public class ApplicationStatus
{
    public string Value { get; set; }

    public ApplicationStatus(string value)
    {
        Value = value;
    }
}

public class OfficerPosition
{
    public string Value { get; set; }

    public OfficerPosition(string value)
    {
        Value = value;
    }
}

public class Officer
{
    public Guid Id { get; set; }
    public PersonName Name { get; set; }
    public OfficerPosition Position { get; set; }

    public Officer(Guid id, PersonName name, OfficerPosition position)
    {
        Id = id;
        Name = name;
        Position = position;
    }
}

public class Applicant
{
    public Guid Id { get; set; }
    public PersonName Name { get; set; }
    public ContactInfo Contact { get; set; }

    public Applicant(Guid id, PersonName name, ContactInfo contact)
    {
        Id = id;
        Name = name;
        Contact = contact;
    }

    public VisaApplication CreateApplication(ApplicationNumber number)
    {
        return new VisaApplication(number, this);
    }
}

public class VisaApplication
{
    public ApplicationNumber Number { get; set; }
    public Applicant Applicant { get; set; }
    public ApplicationStatus Status { get; set; }
    public Officer Officer { get; set; }

    public VisaApplication(ApplicationNumber number, Applicant applicant)
    {
        Number = number;
        Applicant = applicant;
        Status = new ApplicationStatus("Создана");
        Officer = null;
    }

    public void FillForm()
    {
        if (Status.Value == "Создана")
            Status = new ApplicationStatus("Форма заполнена");
    }

    public void CheckDocuments()
    {
        if (Status.Value == "Форма заполнена")
            Status = new ApplicationStatus("Документы проверены");
    }

    public void ApproveVisa(Officer officer)
    {
        if (Status.Value == "Документы проверены")
        {
            Status = new ApplicationStatus("Виза одобрена");
            Officer = officer;
        }
    }
}

// ===== ГЛАВНАЯ ПРОГРАММА =====

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== СИСТЕМА ВЫДАЧИ ВИЗ ===\n");

        var name = new PersonName("Иван", "Петров");
        var contact = new ContactInfo("ivan@mail.com", "+79991234567");
        var applicant = new Applicant(Guid.NewGuid(), name, contact);

        Console.WriteLine($"Заявитель: {applicant.Name.FirstName} {applicant.Name.LastName}");
        Console.WriteLine($"Email: {applicant.Contact.Email}");
        Console.WriteLine($"Телефон: {applicant.Contact.Phone}\n");

        var number = new ApplicationNumber("VN123");
        var visaApp = applicant.CreateApplication(number);

        Console.WriteLine($"Номер заявки: {visaApp.Number.Value}");
        Console.WriteLine($"Статус: {visaApp.Status.Value}\n");

        Console.WriteLine("Процесс рассмотрения:");

        visaApp.FillForm();
        Console.WriteLine($"  - После заполнения формы: {visaApp.Status.Value}");

        visaApp.CheckDocuments();
        Console.WriteLine($"  - После проверки документов: {visaApp.Status.Value}");

        var officer = new Officer(Guid.NewGuid(), new PersonName("Петр", "Сидоров"), new OfficerPosition("Officer"));
        visaApp.ApproveVisa(officer);

        Console.WriteLine($"\n✅ ВИЗА ОДОБРЕНА!");
        Console.WriteLine($"Статус: {visaApp.Status.Value}");

        if (visaApp.Officer != null)
        {
            Console.WriteLine($"Одобрил: {visaApp.Officer.Name.FirstName} {visaApp.Officer.Name.LastName}");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}