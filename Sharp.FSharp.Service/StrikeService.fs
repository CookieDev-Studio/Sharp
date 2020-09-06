namespace Sharp.FSharp.Service

open System
open NpgsqlTypes
open Sharp.FSharp.Data

module StrikeService =
    let addStrike guildId userId modId reason (dateTime : DateTime) =
        let result = StrikeData.AddStrike guildId userId modId reason (NpgsqlDateTime.ToNpgsqlDateTime dateTime)
        match result with
        |Ok _ -> ()
        |Error error -> Console.WriteLine error

    let getStrikes guildId userId =
        let result = StrikeData.GetStrikes guildId userId
        match result with
        |Ok result -> List.toArray result
        |Error error -> Console.WriteLine error; Array.empty 
