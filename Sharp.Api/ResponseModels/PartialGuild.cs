using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PartialGuild
{
    public ulong Id { get; set; }
    public string Name { get; set; }
    public string? Icon { get; set; }
    public bool Owner { get; set; }
    public int Permissions { get; set; }
    [JsonProperty(PropertyName = "permissions_new")]
    public string PermissionsNew { get; set; }
}