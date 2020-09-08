namespace Sharp.Service

open System
open NpgsqlTypes
open Sharp.Data

module StrikeService =

    let addStrike guildId userId modId reason (dateTime : DateTime) =
        StrikeData.addStrikeAsync guildId userId modId reason (NpgsqlDateTime.ToNpgsqlDateTime dateTime)
        |> Operations.handleResultUnitAsync

    let getStrikesAsync guildId userId =
        StrikeData.getStrikesAsync guildId userId
        |> Operations.handleResultListAsync

    let getAllStrikesAsync guildId =
        StrikeData.getAllStrikesAsync guildId 
        |> Operations.handleResultListAsync

    let removeStrike guildId strikeId =
        StrikeData.removeStrikeAsync guildId strikeId 
        |> Operations.handleResultUnitAsync

    let removeAllStrikesFromUser guildId userid =
        StrikeData.removeAllStrikesFromUserAsync guildId userid 
        |> Operations.handleResultUnitAsync