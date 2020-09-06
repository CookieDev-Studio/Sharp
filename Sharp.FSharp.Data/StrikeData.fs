namespace Sharp.FSharp.Data

open Sharp.FSharp.Domain
open Npgsql.FSharp
open System

module StrikeData =
    let private parseStrike (read : RowReader) = 
        { id = read.int "id"
          guildId = GuildId (read.string "guild_id" |> UInt64.Parse) 
          userId = UserId (read.string "user_id" |> UInt64.Parse)
          modId = ModId (read.string "mod_id" |> UInt64.Parse)
          reason = read.string "reason"
          date = read.NpgsqlReader.GetOrdinal("date_time") |> read.NpgsqlReader.GetTimeStamp }

    let addStrike (GuildId guildId) (UserId userId) (ModId modId) reason dateTime =
        sprintf "SELECT add_strike('%i', '%i', '%i', '%s', '%A')"
            guildId userId modId reason dateTime
        |> Operations.executeNonQuery
          
    let getStrikes (GuildId guildId) (UserId userId) =
        sprintf "SELECT * FROM strike
        	     WHERE guild_id = '%i' 
        	     AND user_id = '%i'"
            guildId userId
        |> Operations.executeQuery parseStrike
         
    let getAllStrikes (GuildId guildId) =
        sprintf "SELECT * FROM strike
        	     WHERE guild_id = '%i'" 
            guildId
        |> Operations.executeQuery parseStrike

    let removeStrike(GuildId guildId) (strikeId : int) =
        sprintf "SELECT remove_strike('%i', '%i')"
            guildId strikeId
        |> Operations.executeNonQuery
            
    let removeAllStrikesFromUser (GuildId guildId) (UserId userId) =
        sprintf "SELECT remove_all_strikes('%i', '%i')"
            guildId userId
        |> Operations.executeNonQuery