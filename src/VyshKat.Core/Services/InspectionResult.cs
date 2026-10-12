namespace VyshKat.Core.Services;

/// Результат техосмотра
public sealed record InspectionResult(bool IsApproved, string Reason)
{
    public static InspectionResult Approved() => new(true, "Техосмотр пройден");

    public static InspectionResult Rejected(string reason) => new(false, reason);
}