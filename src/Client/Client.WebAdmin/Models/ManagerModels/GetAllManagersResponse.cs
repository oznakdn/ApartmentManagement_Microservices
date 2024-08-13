﻿namespace AdminWebApp.Models.ManagerModels;

public class GetAllManagersResponse
{
    public string Id { get; set; }
    public string? SiteId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}