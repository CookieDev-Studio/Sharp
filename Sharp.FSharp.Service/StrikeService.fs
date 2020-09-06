namespace Sharp.FSharp.Service

open System
open NpgsqlTypes
open Sharp.FSharp.Data

module StrikeService =
    let addStrike guildId userId modId reason (dateTime : DateTime) =
        let result = StrikeData.addStrike guildId userId modId reason (NpgsqlDateTime.ToNpgsqlDateTime dateTime)
        match result with
        |Ok _ -> ()
        |Error error -> Console.WriteLine error

    let getStrikes guildId userId =
        let result = StrikeData.getStrikes guildId userId
        match result with
        |Ok result -> List.toArray result
        |Error error -> Console.WriteLine error; Array.empty 

    let getAllStrikes guildId =
        let result = StrikeData.getAllStrikes guildId
        match result with
        |Ok result -> List.toArray result
        |Error error -> Console.WriteLine error; Array.empty

    let removeStrike guildId strikeId =
           let result = StrikeData.removeStrike guildId strikeId
           match result with
           |Ok _ -> ()
           |Error error ->  Console.WriteLine error; ()

    let removeAllStrikes guildId userid =
        let result = StrikeData.removeAllStrikesFromUser guildId userid
        match result with
        |Ok _ -> ()
        |Error error ->  Console.WriteLine error; ()