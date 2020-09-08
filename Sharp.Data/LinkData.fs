namespace Sharp.Data

open Sharp.Domain
open Npgsql.FSharp
open System

module LinkData =

    let private parseLinkRolePair (read : RowReader ) =
        { guildId = read.string "guild_id" |> UInt64.Parse |> GuildId
          roleId = read.string "role_id" |> UInt64.Parse |> RoleId
          linkCode = read.string "code"
          uses = read.int "uses"}

    let addLinkRolePair (GuildId guildId) (RoleId roleId) linkCode uses =
        sprintf "SELECT add_link_role_pair('%i', '%i', '%s', %i)"
            guildId roleId linkCode uses
        |> Operations.executeNonQuery

    let updateUses linkCode uses =
        sprintf "SELECT update_link_uses('%s', %i)"
            linkCode uses
        |> Operations.executeNonQuery

    let getLinkRolePairs (GuildId guildId) =
        sprintf "SELECT * FROM link_role_pair 
                 WHERE guild_id = '%i'"
            guildId
        |> Operations.executeQuery parseLinkRolePair
