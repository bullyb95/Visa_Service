namespace Domain.ValueObject.Exceptions;

public class AnotherOfficerEditApplicationException(string officerId, string applicationId, string? ownerOfficerId)
    : InvalidOperationException($"Officer {officerId} cannot edit application {applicationId} owned by officer {ownerOfficerId ?? "unknown"}")
{
    public string OfficerId => officerId;
    public string ApplicationId => applicationId;
    public string? OwnerOfficerId => ownerOfficerId;
}