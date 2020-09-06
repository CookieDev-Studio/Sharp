namespace Sharp.FSharp.Data

open Sharp.FSharp.Domain

module StrikeData =
    let AddStrike (GuildId guildId) (UserId userId) (ModId modId) reason dateTime =
        sprintf "SELECT add_strike('%i', '%i', '%i', '%s', '%A')"
            guildId
            userId
            modId
            reason
            dateTime
        |> Operations.executeNonQuery
          
(*
Strike> GetStrikes(ulong guildId, ulong userId);
RemoveAllStrikesFromUser(ulong guildId, ulong userId);
RemoveStrike(ulong guildId, int strikeId);


    (fun read ->
        { id = read.string "id" |> Uint64.Parse
          guildId = read.string "guild_id" |> }) *)