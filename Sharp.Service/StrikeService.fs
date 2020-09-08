namespace Sharp.Service

open System
open NpgsqlTypes
open Sharp.Data

module StrikeService =

    let addStrike guildId userId modId reason (dateTime : DateTime) =
        StrikeData.addStrike guildId userId modId reason (NpgsqlDateTime.ToNpgsqlDateTime dateTime) |> Operations.handleResultUnit

    let getStrikes guildId userId =
        StrikeData.getStrikes guildId userId |> Operations.handleResultList

    let getAllStrikes guildId =
        StrikeData.getAllStrikes guildId |> Operations.handleResultList

    let removeStrike guildId strikeId =
        StrikeData.removeStrike guildId strikeId |> Operations.handleResultUnit

    let removeAllStrikesFromUser guildId userid =
        StrikeData.removeAllStrikesFromUser guildId userid |> Operations.handleResultUnit