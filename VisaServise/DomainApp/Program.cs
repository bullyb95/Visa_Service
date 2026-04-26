using System;
using System.Collections.Generic;

namespace VisaService;

// ========== КЛАССЫ ДАННЫХ ==========

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
    private List<VisaApplication> _applications = new List<VisaApplication>();

    public Applicant(Guid id, PersonName name, ContactInfo contact)
    {
        Id = id;
        Name = name;
        Contact = contact;
    }

    public VisaApplication CreateApplication(ApplicationNumber number)
    {
        VisaApplication application = new VisaApplication(number, this);
        _applications.Add(application);
        return application;
    }
}

public class VisaApplication
{
    public ApplicationNumber Number { get; set; }
    public Applicant Applicant { get; set; }
    public string Status { get; set; }
    public Officer Officer { get; set; }

    public VisaApplication(ApplicationNumber number, Applicant applicant)
    {
        Number = number;
        Applicant = applicant;
        Status = "Создана";
        Officer = null;
    }

    public void FillForm()
    {
        if (Status == "Создана")
        {
            Status = "Форма заполнена";
        }
    }

    public void CheckDocuments()
    {
        if (Status == "Форма заполнена")
        {
            Status = "Документы проверены";
        }
    }

    public void ApproveVisa(Officer officer)
    {
        if (Status == "Документы проверены")
        {
            Status = "Виза одобрена";
            Officer = officer;
        }
    }
}

// ========== ГЛАВНАЯ ПРОГРАММА ==========

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== СИСТЕМА ВЫДАЧИ ВИЗ ===\n");

        // 1. Создаем заявителя
        PersonName name = new PersonName("Иван", "Петров");
        ContactInfo contact = new ContactInfo("ivan@mail.com", "+79991234567");
        Applicant applicant = new Applicant(Guid.NewGuid(), name, contact);

        Console.WriteLine("Заявитель: " + applicant.Name.FirstName + " " + applicant.Name.LastName);
        Console.WriteLine("Email: " + applicant.Contact.Email);
        Console.WriteLine("Телефон: " + applicant.Contact.Phone + "\n");

        // 2. Создаем заявку
        ApplicationNumber number = new ApplicationNumber("VN123");
        VisaApplication visaApp = applicant.CreateApplication(number);

        Console.WriteLine("Номер заявки: " + visaApp.Number.Value);
        Console.WriteLine("Статус: " + visaApp.Status + "\n");

        // 3. Процесс рассмотрения
        Console.WriteLine("Процесс рассмотрения:");

        visaApp.FillForm();
        Console.WriteLine("  - После заполнения формы: " + visaApp.Status);

        visaApp.CheckDocuments();
        Console.WriteLine("  - После проверки документов: " + visaApp.Status);

        // 4. Создаем офицера и одобряем визу
        Officer officer = new Officer(Guid.NewGuid(), new PersonName("Петр", "Сидоров"), new OfficerPosition("Officer"));
        visaApp.ApproveVisa(officer);

        Console.WriteLine("\n✅ ВИЗА ОДОБРЕНА!");
        Console.WriteLine("Статус: " + visaApp.Status);

        if (visaApp.Officer != null)
        {
            Console.WriteLine("Одобрил: " + visaApp.Officer.Name.FirstName + " " + visaApp.Officer.Name.LastName);
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}