namespace Sharp.FSharp.Data

open Sharp.FSharp.Domain
open System

module StrikeData =
    let AddStrike (GuildId guildId) (UserId userId) (ModId modId) reason dateTime =
        sprintf "SELECT add_strike('%i', '%i', '%i', '%s', '%A')"
            guildId
            userId
            modId
            reason
            dateTime
        |> Operations.executeNonQuery
          
    let GetStrikes(GuildId guildId) (UserId userId) =
        sprintf "select * from strike
        	     where guild_id = '%i' 
        	     and user_id = '%i'"
            guildId
            userId
        |> Operations.executeQuery (fun read ->
            { id = read.int "id"
              guildId = GuildId (read.string "guild_id" |> UInt64.Parse) 
              userId = UserId (read.string "user_id" |> UInt64.Parse)
              modId = ModId (read.string "mod_id" |> UInt64.Parse)
              reason = read.string "reason"
              date = read.NpgsqlReader.GetOrdinal("date_time") |> read.NpgsqlReader.GetTimeStamp })
              
(*
RemoveAllStrikesFromUser(ulong guildId, ulong userId);
RemoveStrike(ulong guildId, int strikeId);
*)

    