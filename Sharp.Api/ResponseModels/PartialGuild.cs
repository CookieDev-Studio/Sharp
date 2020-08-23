using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PartialGuild
{
    [JsonProperty(PropertyName = "id")]
    public ulong Id { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "icon")]
    public string? Icon { get; set; }
    [JsonProperty(PropertyName = "owner")]
    public bool Owner { get; set; }
    [JsonProperty(PropertyName = "permissions")]
    public int Permissions { get; set; }
    [JsonProperty(PropertyName = "permissions_new")]
    public string PermissionsNew { get; set; }
}